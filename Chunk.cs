using System;
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
        public short[,] BaseTiles;
        public static int LocalBitMask;

        public readonly List<int> ContainedTileEntityIDs;

        static Chunk ()
        {
            LocalBitMask = (int)Math.Pow(2, Bits) - 1;
        }

        public Chunk(ChunkCoords coords, short[,] baseTiles)
        {
            BaseTiles = baseTiles;
            Coords = coords;
        }
    }
}
