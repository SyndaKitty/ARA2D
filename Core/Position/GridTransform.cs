using Core.Position;
using System.Numerics;

namespace Core
{
    public class GridTransform
    {
        TileCoords coords;
        public int Width;
        public int Height;

        public TileCoords Coords
        {
            get => coords;
            set
            {
                coords = value;
                Dirty = true;
            }
        }

        public Matrix4x4 Matrix; // Calculated
        public bool Dirty;

        public GridTransform(TileCoords coords, int width, int height)
        {
            Coords = coords;
            Width = width;
            Height = height;
        }
    }
}
