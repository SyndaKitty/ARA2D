using System;

namespace ARA2D.Chunks
{
    public class TileChunk
    {
        //public const int Bits = 2; // 2^2 = 4
        //public const int Bits = 3; // 2^3 = 8
        //public const int Bits = 4; // 2^4 = 16 
        public const int Bits = 5; // 2^5 = 32
        //public const int Bits = 6; // 2^6=64
        public const int Size = 1 << Bits;

        public static int LocalBitMask;
        static TileChunk ()
        {
            LocalBitMask = (int)Math.Pow(2, Bits) - 1;
        }

        public readonly ChunkCoords Coords;
        public short[,] BaseTiles;

        public TileChunk(ChunkCoords coords, short[,] baseTiles)
        {
            BaseTiles = baseTiles;
            Coords = coords;
        }
    }
}
