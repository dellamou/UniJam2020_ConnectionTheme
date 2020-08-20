using UnityEngine;

namespace FoW
{
    [System.Serializable]
    public abstract class FogOfWarDrawer
    {
        protected FogOfWarMap _map;

        public void Initialise(FogOfWarMap map)
        {
            _map = map;
            OnInitialise();
        }

        protected virtual void OnInitialise() { }
        public virtual void OnDestroy() { }
        public abstract void Clear(byte value);
        public abstract bool Fade(byte[] currentvalues, byte[] totalvalues, float partialfogamount, int inamount, int outamount);
        public abstract void GetValues(byte[] outvalues);
        public abstract void SetValues(byte[] values);
        public abstract void Draw(FogOfWarShape shape, bool ismultithreaded);

        protected struct DrawInfo
        {
            public Vector2 fogCenterPos;
            public Vector2Int fogEyePos;
            public Vector2 fogForward;
            public float forwardAngle;
            public int xMin;
            public int xMax;
            public int yMin;
            public int yMax;

            public DrawInfo(FogOfWarMap map, FogOfWarShape shape)
            {
                // convert size to fog space
                Vector2 radius = shape.CalculateRadius() * map.pixelSize;
                fogForward = shape.foward;
                Vector2 relativeoffset;
                if (shape.absoluteOffset)
                {
                    forwardAngle = 0;
                    relativeoffset = shape.offset;
                }
                else
                {
                    forwardAngle = FogOfWarUtils.ClockwiseAngle(Vector2.up, fogForward) * Mathf.Deg2Rad;
                    float sin = Mathf.Sin(-forwardAngle);
                    float cos = Mathf.Cos(-forwardAngle);
                    relativeoffset = new Vector2(shape.offset.x * cos - shape.offset.y * sin, shape.offset.x * sin + shape.offset.y * cos);
                }

                fogCenterPos = FogOfWarConversion.WorldToFog(FogOfWarConversion.WorldToFogPlane(shape.eyePosition, map.plane) + relativeoffset, map.offset, map.resolution, map.size);
                fogEyePos = FogOfWarConversion.WorldToFog(shape.eyePosition, map.plane, map.offset, map.resolution, map.size).ToInt();

                // find ranges
                if (shape.visibleCells == null)
                {
                    xMin = Mathf.Max(0, Mathf.RoundToInt(fogCenterPos.x - radius.x));
                    xMax = Mathf.Min(map.resolution.x - 1, Mathf.RoundToInt(fogCenterPos.x + radius.x));
                    yMin = Mathf.Max(0, Mathf.RoundToInt(fogCenterPos.y - radius.y));
                    yMax = Mathf.Min(map.resolution.y - 1, Mathf.RoundToInt(fogCenterPos.y + radius.y));
                }
                else
                {
                    fogCenterPos = FogOfWarConversion.SnapToNearestFogPixel(fogCenterPos);
                    fogEyePos = FogOfWarConversion.SnapToNearestFogPixel(FogOfWarConversion.WorldToFog(shape.eyePosition, map.offset, map.resolution, map.size)).ToInt();

                    Vector2Int pos = fogCenterPos.ToInt();
                    Vector2Int rad = radius.ToInt();
                    xMin = Mathf.Max(0, Mathf.RoundToInt(pos.x - rad.x));
                    xMax = Mathf.Min(map.resolution.x - 1, Mathf.RoundToInt(pos.x + rad.x));
                    yMin = Mathf.Max(0, Mathf.RoundToInt(pos.y - rad.y));
                    yMax = Mathf.Min(map.resolution.y - 1, Mathf.RoundToInt(pos.y + rad.y));
                }
            }
        }
    }
}
