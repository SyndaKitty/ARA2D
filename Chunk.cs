using System.Collections.Generic;
using ARA2D.Systems;

namespace ARA2D
{
    public class Chunk
    {
        //public const int Bits = 0; // 2^0 = 1
        //public const int Bits = 1; // 2^1 = 2
        //public const int Bits = 2; // 2^2 = 4
        public const int Bits = 3; // 2^3 = 8
        //public const int Bits = 4; // 2^4 = 16 
        //public const int Bits = 5; // 2^5 = 32
        public const int Size = 1 << Bits;

        public readonly ChunkCoords Coords;
        public int[,] TileEntityIDs; // TODO think about better ways to structure this
        public short[,] BaseTiles;

        public readonly List<int> ContainedTileEntityIDs;

        public Chunk(ChunkCoords coords, short[,] baseTiles, int[,] tileEntityIDs = null, List<int> tileEntities = null)
        {
            BaseTiles = baseTiles;
            Coords = coords;
            TileEntityIDs = tileEntityIDs ?? new int[Size,Size];
            ContainedTileEntityIDs = tileEntities ?? new List<int>();
        }
    }
}
