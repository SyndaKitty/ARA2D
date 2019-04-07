namespace ARA2D.WorldGenerators
{
    public interface WorldGenerator
    {
        TileChunk GenerateChunk(ChunkCoords coords);
    }
}
