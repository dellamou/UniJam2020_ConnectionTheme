using UnityEngine;

namespace FoW
{
    public class FogOfWarDrawerSoftware : FogOfWarDrawer
    {
        byte[] _values;
        FilterMode _filterMode;
        protected bool _isMultithreaded { get; private set; }

        public override void GetValues(byte[] outvalues)
        {
            System.Array.Copy(_values, outvalues, _values.Length);
        }

        public override void SetValues(byte[] values)
        {
            System.Array.Copy(values, _values, _values.Length);
        }

        protected override void OnInitialise()
        {
            if (_values == null || _values.Length != _map.pixelCount)
                _values = new byte[_map.pixelCount];
            
            _filterMode = _map.filterMode;
        }

        public override void Clear(byte value)
        {
            for (int i = 0; i < _values.Length; ++i)
                _values[i] = value;
        }

        public override bool Fade(byte[] currentvalues, byte[] totalvalues, float partialfogamount, int inamount, int outamount)
        {
            // partial fog needs to be inversed
            partialfogamount = 1 - partialfogamount;
            int partialfog = (int)(partialfogamount * (1 << 8));

            bool haschanged = false;
            for (int i = 0; i < currentvalues.Length; ++i)
            {
                // if nothing has changed, don't do anything
                if (currentvalues[i] == totalvalues[i])
                    continue;

                // decrease fog
                if (currentvalues[i] < totalvalues[i])
                    totalvalues[i] = (byte)Mathf.Max(totalvalues[i] - inamount, currentvalues[i]);
                else
                {
                    // increase fog
                    int target = (currentvalues[i] * partialfog) >> 8;
                    if (totalvalues[i] < target)
                        totalvalues[i] = (byte)Mathf.Min(totalvalues[i] + outamount, target);
                }

                haschanged = true;
            }

            return haschanged;
        }

        bool LineOfSightCanSee(FogOfWarShape shape, Vector2 offset, float fogradius)
        {
            if (shape.lineOfSight == null)
                return true;
            
            float idx = FogOfWarUtils.ClockwiseAngle(Vector2.up, offset) * shape.lineOfSight.Length * (1.0f /  360.0f);
            if (idx < 0)
                idx += shape.lineOfSight.Length;

            // sampling
            float value;
            if (_map.filterMode == FilterMode.Point)
                value = shape.lineOfSight[Mathf.RoundToInt(idx) % shape.lineOfSight.Length];
            else
            {
                int idxlow = Mathf.FloorToInt(idx);
                int idxhigh = (idxlow + 1) % shape.lineOfSight.Length;
                value = Mathf.LerpUnclamped(shape.lineOfSight[idxlow], shape.lineOfSight[idxhigh], idx % 1);
            }

            float dist = value * fogradius;
            return offset.sqrMagnitude < dist * dist;
        }

        bool LineOfSightCanSeeCell(FogOfWarShape shape, Vector2Int offset)
        {
            if (shape.visibleCells == null)
                return true;

            // offset so it is relative to the center
            int halfwidth = shape.visibleCellsWidth >> 1;

            offset.x += halfwidth;
            if (offset.x < 0 || offset.x >= shape.visibleCellsWidth)
                return true;

            offset.y += halfwidth;
            if (offset.y < 0 || offset.y >= shape.visibleCellsWidth)
                return true;

            return shape.visibleCells[offset.y * shape.visibleCellsWidth + offset.x];
        }

        byte SampleTexture(Texture2D texture, float u, float v, float brightness)
        {
            // GetPixel() and GetPixelBilinear() are not supported on other threads!
            if (_map.multithreaded)
                return 0;

            float value = 0;
            if (_filterMode == FilterMode.Point)
                value = 1 - texture.GetPixel(Mathf.FloorToInt(u * texture.width), Mathf.FloorToInt(v * texture.height)).a;
            else
                value = 1 - texture.GetPixelBilinear(u, v).a;
            value = 1 - (1 - value) * brightness;
            return (byte)(value * 255);
        }

        void Unfog(int x, int y, byte v)
        {
            int index = y * _map.resolution.x + x;
            if (_values[index] > v)
                _values[index] = v;
        }

        public override void Draw(FogOfWarShape shape, bool ismultithreaded)
        {
            _isMultithreaded = ismultithreaded;

            if (shape is FogOfWarShapeCircle circle)
                DrawCircle(circle);
            else if (shape is FogOfWarShapeBox box)
            {
                if (box.rotateToForward)
                    DrawRotatedBox(box);
                else
                    DrawAxisAlignedBox(box);
            }
        }

        void DrawCircle(FogOfWarShapeCircle shape)
        {
            int fogradius = Mathf.RoundToInt(shape.radius * _map.pixelSize);
            int fogradiussqr = fogradius * fogradius;
            DrawInfo info = new DrawInfo(_map, shape);
            float lineofsightradius = shape.CalculateMaxLineOfSightDistance() * _map.pixelSize;

            // view angle stuff
            float dotangle = 1 - shape.angle / 90;
            bool usedotangle = dotangle > -0.99f;

            for (int y = info.yMin; y <= info.yMax; ++y)
            {
                for (int x = info.xMin; x <= info.xMax; ++x)
                {
                    // is pixel within circle radius
                    Vector2 centeroffset = new Vector2(x, y) - info.fogCenterPos;
                    if (shape.visibleCells == null && centeroffset.sqrMagnitude >= fogradiussqr)
                        continue;

                    // check if in view angle
                    if (usedotangle && Vector2.Dot(centeroffset.normalized, info.fogForward) <= dotangle)
                        continue;

                    // can see pixel
                    Vector2Int offset = new Vector2Int(x, y) - info.fogEyePos;
                    if (!LineOfSightCanSee(shape, offset, lineofsightradius))
                        continue;

                    if (!LineOfSightCanSeeCell(shape, offset))
                        continue;

                    Unfog(x, y, shape.GetFalloff(centeroffset.magnitude / lineofsightradius));
                }
            }
        }

        void DrawAxisAlignedBox(FogOfWarShapeBox shape)
        {
            // convert size to fog space
            DrawInfo info = new DrawInfo(_map, shape);
            float lineofsightradius = shape.CalculateMaxLineOfSightDistance() * _map.pixelSize + 0.01f;

            byte brightness = shape.maxBrightness;
            bool drawtexture = shape.hasTexture && !_isMultithreaded;
            for (int y = info.yMin; y <= info.yMax; ++y)
            {
                for (int x = info.xMin; x <= info.xMax; ++x)
                {
                    // can see pixel
                    Vector2Int offset = new Vector2Int(x, y) - info.fogEyePos;
                    if (!LineOfSightCanSee(shape, offset, lineofsightradius))
                        continue;

                    if (!LineOfSightCanSeeCell(shape, offset))
                        continue;

                    // unfog
                    if (drawtexture)
                    {
                        float u = Mathf.InverseLerp(info.xMin, info.xMax, x);
                        float v = Mathf.InverseLerp(info.yMin, info.yMax, y);
                        Unfog(x, y, SampleTexture(shape.texture, u, v, shape.brightness));
                    }
                    else
                        Unfog(x, y, brightness);
                }
            }
        }

        void DrawRotatedBox(FogOfWarShapeBox shape)
        {
            // convert size to fog space
            DrawInfo info = new DrawInfo(_map, shape);
            float lineofsightradius = shape.CalculateMaxLineOfSightDistance() * _map.pixelSize;

            // rotation stuff
            Vector2 sizemul = shape.size * 0.5f * _map.pixelSize;
            Vector2 invfogsize = new Vector2(1.0f / (shape.size.x * _map.pixelSize), 1.0f / (shape.size.y * _map.pixelSize));
            float sin = Mathf.Sin(info.forwardAngle);
            float cos = Mathf.Cos(info.forwardAngle);

            byte brightness = shape.maxBrightness;
            bool drawtexture = shape.hasTexture && !_isMultithreaded;
            for (int y = info.yMin; y < info.yMax; ++y)
            {
                float yy = y - info.fogCenterPos.y;

                for (int x = info.xMin; x < info.xMax; ++x)
                {
                    float xx = x - info.fogCenterPos.x;

                    // get rotated uvs
                    float u = xx * cos - yy * sin;
                    if (u < -sizemul.x || u >= sizemul.x)
                        continue;
                    float v = yy * cos + xx * sin;
                    if (v < -sizemul.y || v >= sizemul.y)
                        continue;

                    // can see pixel
                    Vector2Int offset = new Vector2Int(x, y) - info.fogEyePos;
                    if (!LineOfSightCanSee(shape, offset, lineofsightradius))
                        continue;

                    if (!LineOfSightCanSeeCell(shape, offset))
                        continue;

                    // unfog
                    if (drawtexture)
                        Unfog(x, y, SampleTexture(shape.texture, 0.5f + u * invfogsize.x, 0.5f + v * invfogsize.y, shape.brightness));
                    else
                        Unfog(x, y, brightness);
                }
            }
        }
    }
}
