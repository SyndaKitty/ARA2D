using Microsoft.Xna.Framework;
using Nez;

namespace ARA2D
{
    public class Chunk
    {
        public const int Size = 32;
        public readonly ChunkCoords Coords;
        public Tile[,] tiles;

        public Chunk(ChunkCoords coords)
        {
            Coords = coords;
        }
    }
}
