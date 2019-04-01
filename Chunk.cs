namespace ARA2D
{
    public class Chunk
    {
        public readonly ChunkCoords Coords;
        public Tile[,] tiles;

        public Chunk(ChunkCoords coords)
        {
            Coords = coords;
        }
    }
}
