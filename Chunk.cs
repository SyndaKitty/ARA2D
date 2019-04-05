namespace ARA2D
{
    public class Chunk
    {
        public const int Bits = 2; // 2^2 = 4
        //public const int Bits = 3; // 2^3 = 8
        //public const int Bits = 4; // 2^4 = 16 
        //public const int Bits = 5; // 2^5 = 32
        public const int Size = 1 << Bits;

        public readonly ChunkCoords Coords;
        public int[,] TileEntityIDs; // TODO think about better ways to structure this
        public short[,] BaseTiles;
        
        public Chunk(ChunkCoords coords, short[,] baseTiles, int[,] tileEntityIDs = null)
        {
            BaseTiles = baseTiles;
            Coords = coords;
            TileEntityIDs = tileEntityIDs ?? new int[Size,Size];
        }
    }
}
