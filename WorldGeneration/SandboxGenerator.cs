using ARA2D.Chunks;

namespace ARA2D.WorldGeneration
{
    public class SandboxGenerator : WorldGenerator
    {
        public TileChunk GenerateChunk(ChunkCoords coords)
        {
            short[,] tiles = new short[TileChunk.Size,TileChunk.Size];
            for (int y = 0; y < TileChunk.Size; y++)
            {
                for (int x = 0; x < TileChunk.Size; x++)
                {
                    tiles[x, y] = 0;
                }
            }
            return new TileChunk(coords, tiles);
        }
    }
}
