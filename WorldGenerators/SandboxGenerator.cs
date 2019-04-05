namespace ARA2D.WorldGenerators
{
    public class SandboxGenerator : WorldGenerator
    {
        public Chunk GenerateChunk(ChunkCoords coords)
        {
            short[,] tiles = new short[Chunk.Size,Chunk.Size];
            for (int y = 0; y < Chunk.Size; y++)
            {
                for (int x = 0; x < Chunk.Size; x++)
                {
                    tiles[x, y] = 0;
                }
            }
            return new Chunk(coords, tiles);
        }
    }
}
