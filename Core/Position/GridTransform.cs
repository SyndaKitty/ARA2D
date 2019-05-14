using Core.Position;
using System.Numerics;

namespace Core
{
    public class GridTransform
    {
        public TileCoords Coords;

        public int Width { get; }
        public int Height { get; }
        
        public Matrix4x4 Matrix; // Calculated

        public GridTransform(TileCoords coords, int width, int height)
        {
            Coords = coords;

            Width = width;
            Height = height;
        }
    }
}
