using UnityEngine;
using System.Collections.Generic;
#if UNITY_2018_1_OR_NEWER
using Unity.Collections;
#endif

namespace FoW
{
    public enum FogOfWarShapeType
    {
        Circle,
        Box
    }

#if UNITY_2018_1_OR_NEWER
    class FogOfWarUnitRaycasts
    {
        public NativeArray<RaycastCommand> raycasts;
        public NativeArray<RaycastHit> hits;
    }
#endif

    [AddComponentMenu("FogOfWar/FogOfWarUnit")]
    public class FogOfWarUnit : MonoBehaviour
    {
        public int team = 0;
        [Range(0, 1)]
        public float brightness = 1;

        [Header("Shape")]
        public FogOfWarShapeType shapeType = FogOfWarShapeType.Circle;
        public bool absoluteOffset = false;
        public Vector2 offset = Vector2.zero;

        [Tooltip("For Box and Texture")]
        public Vector2 boxSize = new Vector2(5, 5);
        [Tooltip("For Circle")]
        public float circleRadius = 5;
        [Tooltip("For Circle"), Range(0.0f, 1.0f)]
        public float innerRadius = 1;
        [Tooltip("For Circle"), Range(0.0f, 180.0f)]
        public float angle = 180;
        [Tooltip("For Texture")]
        public Texture2D texture;
        [Tooltip("For Texture")]
        public bool rotateToForward = false;

        [Header("Line of Sight")]
        public LayerMask lineOfSightMask = 0;
        public int lineOfSightRaycastCount = 100;
        public float lineOfSightPenetration = 0;
#if UNITY_2018_1_OR_NEWER
        public bool multithreading = false;
        FogOfWarUnitRaycasts _multithreadRaycasts = null;
#endif
        public bool cellBased = false;
        public bool snapToPixelCenter = false;

        float[] _distances = null;
        bool[] _visibleCells = null;

        Transform _transform;
        FogOfWarShape _cachedShape = null;

        static List<FogOfWarUnit> _registeredUnits = new List<FogOfWarUnit>();
        public static List<FogOfWarUnit> registeredUnits { get { return _registeredUnits; } }

        void Awake()
        {
            _transform = transform;
        }

        void OnEnable()
        {
            registeredUnits.Add(this);
        }

        void CleanupMultithreadRaycasts()
        {
#if UNITY_2018_1_OR_NEWER
            if (_multithreadRaycasts == null)
                return;

            _multithreadRaycasts.raycasts.Dispose();
            _multithreadRaycasts.hits.Dispose();
            _multithreadRaycasts = null;
#endif
        }

        void OnDisable()
        {
            registeredUnits.Remove(this);
            CleanupMultithreadRaycasts();
        }

        static bool CalculateLineOfSight2D(Vector2 eye, float radius, float penetration, LayerMask layermask, float[] distances)
        {
            bool hashit = false;
            float angle = 360.0f / distances.Length;
            RaycastHit2D hit;

            for (int i = 0; i < distances.Length; ++i)
            {
                Vector2 dir = Quaternion.AngleAxis(angle * i, Vector3.back) * Vector2.up;
                hit = Physics2D.Raycast(eye, dir, radius, layermask);
                if (hit.collider != null)
                {
                    distances[i] = (hit.distance + penetration) / radius;
                    if (distances[i] < 1)
                        hashit = true;
                    else
                        distances[i] = 1;
                }
                else
                    distances[i] = 1;
            }

            return hashit;
        }

        bool CalculateLineOfSight3D(Vector3 eye, float radius, float penetration, LayerMask layermask, Vector3 up, Vector3 forward)
        {
            bool hashit = false;
            float angle = 360.0f / _distances.Length;

#if UNITY_2018_1_OR_NEWER
            if (multithreading)
            {
                // make sure native arrays are ready
                if (_multithreadRaycasts == null)
                {
                    _multithreadRaycasts = new FogOfWarUnitRaycasts()
                    {
                        raycasts = new NativeArray<RaycastCommand>(lineOfSightRaycastCount, Allocator.Persistent),
                        hits = new NativeArray<RaycastHit>(lineOfSightRaycastCount, Allocator.Persistent)
                    };
                }
                else if (_multithreadRaycasts.raycasts.Length != lineOfSightRaycastCount)
                {
                    _multithreadRaycasts.raycasts = new NativeArray<RaycastCommand>(lineOfSightRaycastCount, Allocator.Persistent);
                    _multithreadRaycasts.hits = new NativeArray<RaycastHit>(lineOfSightRaycastCount, Allocator.Persistent);
                }
                
                // prepare raycasts
                for (int i = 0; i < _distances.Length; ++i)
                {
                    Vector3 dir = Quaternion.AngleAxis(angle * i, up) * forward;
                    _multithreadRaycasts.raycasts[i] = new RaycastCommand(eye, dir, radius, layermask);
                }

                // perform raycasts
                RaycastCommand.ScheduleBatch(_multithreadRaycasts.raycasts, _multithreadRaycasts.hits, 1).Complete();

                // copy results
                for (int i = 0; i < _distances.Length; ++i)
                {
                    if (_multithreadRaycasts.hits[i].collider != null)
                    {
                        _distances[i] = (_multithreadRaycasts.hits[i].distance + penetration) / radius;
                        if (_distances[i] < 1)
                            hashit = true;
                        else
                            _distances[i] = 1;
                    }
                    else
                        _distances[i] = 1;
                }
            }
            else
#endif
            {
                CleanupMultithreadRaycasts();
                for (int i = 0; i < _distances.Length; ++i)
                {
                    Vector3 dir = Quaternion.AngleAxis(angle * i, up) * forward;
                    if (Physics.Raycast(eye, dir, out RaycastHit hit, radius, layermask))
                    {
                        _distances[i] = (hit.distance + penetration) / radius;
                        if (_distances[i] < 1)
                            hashit = true;
                        else
                            _distances[i] = 1;
                    }
                    else
                        _distances[i] = 1;
                }
            }

            return hashit;
        }

        float[] CalculateLineOfSight(FogOfWarPhysics physicsmode, Vector3 eyepos, FogOfWarPlane plane, float distance)
        {
            if (lineOfSightMask == 0)
                return null;

            if (_distances == null || _distances.Length != lineOfSightRaycastCount)
                _distances = new float[lineOfSightRaycastCount];

            if (physicsmode == FogOfWarPhysics.Physics2D)
            {
                if (CalculateLineOfSight2D(eyepos, distance, lineOfSightPenetration, lineOfSightMask, _distances))
                    return _distances;
            }
            else if (physicsmode == FogOfWarPhysics.Physics3D)
            {
                if (plane == FogOfWarPlane.XZ)
                {
                    if (CalculateLineOfSight3D(eyepos, distance, lineOfSightPenetration, lineOfSightMask, Vector3.up, Vector3.forward))
                        return _distances;
                }
                else if (plane == FogOfWarPlane.XY)
                {
                    if (CalculateLineOfSight3D(eyepos, distance, lineOfSightPenetration, lineOfSightMask, Vector3.back, Vector3.up))
                        return _distances;
                }
            }

            return null;
        }

        static float Sign(float v)
        {
            if (Mathf.Approximately(v, 0))
                return 0;
            return v > 0 ? 1 : -1;
        }
        
        bool[] CalculateLineOfSightCells(FogOfWarTeam fow, FogOfWarPhysics physicsmode, Vector3 eyepos, float distance, out int visiblecellswidth)
        {
            visiblecellswidth = 0;
            if (physicsmode == FogOfWarPhysics.Physics3D)
            {
                Debug.LogWarning("Physics3D is not supported with cells!", this);
                return null;
            }

            int rad = Mathf.RoundToInt(distance * fow.mapResolution.x / fow.mapSize);
            visiblecellswidth = rad + rad + 1;
            if (_visibleCells == null || _visibleCells.Length != visiblecellswidth * visiblecellswidth)
                _visibleCells = new bool[visiblecellswidth * visiblecellswidth];

            Vector2 cellsize = new Vector2(fow.mapSize / fow.mapResolution.x, fow.mapSize / fow.mapResolution.y) * 1.1f; // do 1.1 to bring it away from the collider a bit so the raycast won't hit it
            Vector2 playerworldpos = FogOfWarConversion.WorldToFogPlane(eyepos, fow.plane);
            for (int y = -rad; y <= rad; ++y)
            {
                for (int x = -rad; x <= rad; ++x)
                {
                    Vector2Int offset = new Vector2Int(x, y);

                    // find the nearest point in the cell to the player and raycast to that point
                    Vector2 fogoffset = offset - new Vector2(Sign(offset.x) * cellsize.x, Sign(offset.y) * cellsize.y) * 0.5f;
                    Vector2 worldoffset = FogOfWarConversion.FogToWorldSize(fogoffset, fow.mapResolution, fow.mapSize);
                    Vector2 worldpos = playerworldpos + worldoffset;
#if UNITY_EDITOR
                    Debug.DrawLine(playerworldpos, worldpos);
#endif

                    int idx = (y + rad) * visiblecellswidth + x + rad;

                    // if it is out of range
                    if (worldoffset.magnitude > distance)
                        _visibleCells[idx] = false;
                    else
                    {
                        _visibleCells[idx] = true;
                        RaycastHit2D hit = Physics2D.Raycast(playerworldpos, worldoffset.normalized, Mathf.Max(worldoffset.magnitude - lineOfSightPenetration, 0.00001f), lineOfSightMask);
                        _visibleCells[idx] = hit.collider == null;
                    }
                }
            }

            return _visibleCells;
        }

        void FillShape(FogOfWarTeam fow, FogOfWarShape shape)
        {
            if (snapToPixelCenter)
            {
                // snap to nearest fog pixel
                Vector3 worldpos = _transform.position;
                Vector2 fogworldpos = FogOfWarConversion.WorldToFogPlane(worldpos, fow.plane);
                fogworldpos = FogOfWarConversion.SnapWorldPositionToNearestFogPixel(fow, fogworldpos);
                shape.eyePosition = FogOfWarConversion.FogPlaneToWorld(fogworldpos.x, fogworldpos.y, worldpos.y, fow.plane);
            }
            else
                shape.eyePosition = _transform.position;
            shape.brightness = brightness;
            shape.foward = FogOfWarConversion.TransformFogPlaneForward(_transform, fow.plane);
            shape.absoluteOffset = absoluteOffset;
            shape.offset = offset;
            shape.radius = circleRadius;
            shape.size = boxSize;
        }

        T GetShapeFromCache<T>() where T : FogOfWarShape, new()
        {
            if (_cachedShape == null || !(_cachedShape is T))
                _cachedShape = new T();
            return (T)_cachedShape;

        }

        FogOfWarShape CreateShape(FogOfWarTeam fow)
        {
            if (shapeType == FogOfWarShapeType.Circle)
            {
                FogOfWarShapeCircle shape = GetShapeFromCache<FogOfWarShapeCircle>();
                FillShape(fow, shape);
                shape.innerRadius = innerRadius;
                shape.angle = angle;
                return shape;
            }
            else if (shapeType == FogOfWarShapeType.Box)
            {
                FogOfWarShapeBox shape = GetShapeFromCache<FogOfWarShapeBox>();
                shape.texture = texture;
                shape.hasTexture = texture != null;
                shape.rotateToForward = rotateToForward;
                FillShape(fow, shape);
                return shape;
            }
            return null;
        }

        public FogOfWarShape GetShape(FogOfWarTeam fow, FogOfWarPhysics physics, FogOfWarPlane plane)
        {
            FogOfWarShape shape = CreateShape(fow);
            if (shape == null)
                return null;

            if (cellBased)
            {
                shape.lineOfSight = null;
                shape.visibleCells = CalculateLineOfSightCells(fow, physics, shape.eyePosition, shape.CalculateMaxLineOfSightDistance(), out shape.visibleCellsWidth);
            }
            else
            {
                shape.lineOfSight = CalculateLineOfSight(physics, shape.eyePosition, plane, shape.CalculateMaxLineOfSightDistance());
                shape.visibleCells = null;
                shape.visibleCellsWidth = 0;
            }
            return shape;
        }

        void DrayRaycastGizmos()
        {
            if (lineOfSightMask == 0 || _cachedShape == null || !Application.isPlaying || _distances == null || _distances.Length < 1)
                return;

            FogOfWarTeam fow = FogOfWarTeam.GetTeam(team);
            if (fow == null)
                return;

            Vector3 worldpos = transform.position;
            Vector3 fogorigin = FogOfWarConversion.WorldToFogPlane3(worldpos, fow.plane);
            float radius = _cachedShape.CalculateMaxLineOfSightDistance();
            float angle = 360.0f / _distances.Length;
            for (int i = 0; i < _distances.Length; ++i)
            {
                Vector3 dir = Quaternion.AngleAxis(angle * i, Vector3.back) * Vector3.up;
                Vector3 pos = FogOfWarConversion.FogPlaneToWorld(fogorigin + dir * radius, fow.plane);
                Gizmos.DrawLine(worldpos, pos);
            }
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Vector3 pos;
            if (absoluteOffset)
                pos = transform.position + new Vector3(offset.x, offset.y, offset.y);
            else
                pos = transform.position + transform.forward * offset.y + transform.right * offset.x;
            if (shapeType == FogOfWarShapeType.Circle)
                Gizmos.DrawWireSphere(pos, circleRadius);
            else if (shapeType == FogOfWarShapeType.Box)
                Gizmos.DrawWireCube(pos, new Vector3(boxSize.x, boxSize.y, boxSize.y));

            DrayRaycastGizmos();
        }
    }
}
