using Core.Position;
using Microsoft.Xna.Framework;

namespace Core
{
    public class GridTransform
    {
        public TileCoords Coords;
        
        public Matrix Matrix; // Calculated

        public GridTransform(TileCoords coords)
        {
            Coords = coords;
        }
    }
}
