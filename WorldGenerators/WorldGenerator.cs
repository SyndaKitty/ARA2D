namespace ARA2D.WorldGenerators
{
    public interface WorldGenerator
    {
        Chunk GenerateChunk(ChunkCoords coords);
    }
}
