namespace ARA2D
{
    public class Chunk
    {
        //public const int Bits = 3; // 2^3 = 8
        //public const int Bits = 4; // 2^4 = 16
        public const int Bits = 5; // 2^5 = 32
        public const int Size = 1 << Bits;

        public readonly ChunkCoords Coords;
        public short[,] BaseTiles;

        public Chunk(ChunkCoords coords, short[,] baseTiles)
        {
            BaseTiles = baseTiles;
            Coords = coords;
        }
    }
}
