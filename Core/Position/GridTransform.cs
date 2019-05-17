using Core.Position;
using System.Numerics;

namespace Core
{
    public class GridTransform
    {
        public TileCoords Coords;
        
        public Matrix4x4 Matrix; // Calculated

        public GridTransform(TileCoords coords)
        {
            Coords = coords;
        }
    }
}
