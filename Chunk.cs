namespace ARA2D
{
    public class Chunk
    {
        public const int Bits = 5; // 2^5 = 32
        public const int Size = 1 << Bits;

        public readonly ChunkCoords Coords;
        public Tile[,] tiles;

        public Chunk(ChunkCoords coords)
        {
            Coords = coords;
        }
    }
}
