using Core.Position;
using System.Numerics;

namespace Core
{
    public class GridTransform
    {
        TileCoords coords;

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

        public GridTransform(TileCoords coords)
        {
            Coords = coords;
        }
    }
}
