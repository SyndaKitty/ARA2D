using ARA2D.Chunks;

namespace ARA2D.WorldGeneration
{
    public interface WorldGenerator
    {
        TileChunk GenerateChunk(ChunkCoords coords);
    }
}
