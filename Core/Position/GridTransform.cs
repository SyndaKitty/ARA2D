using Core.Position;
using Microsoft.Xna.Framework;

namespace Core
{
    public class GridTransform
    {
        public TileCoords Coords;

        public int Width { get; }
        public int Height { get; }
        
        public Matrix Matrix; // Calculated

        public GridTransform(TileCoords coords, int width, int height)
        {
            Coords = coords;

            Width = width;
            Height = height;
        }
    }
}
