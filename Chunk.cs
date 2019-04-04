namespace ARA2D
{
    public class Chunk
    {
        public const int Bits = 3; // 2^3 = 8
        public const int Size = 1 << Bits;

        public readonly ChunkCoords Coords;
        public Tile[,] tiles;

        public Chunk(ChunkCoords coords)
        {
            Coords = coords;
        }
    }
}
