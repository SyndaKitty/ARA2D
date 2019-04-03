using System.Collections.Generic;

namespace ARA2D
{
    public class World
    {
        public WorldScene worldScene;
        WorldGenerator generator;
        Dictionary<ChunkCoords, Chunk> chunks;

        public World(WorldGenerator generator)
        {
            this.generator = generator;
            chunks = new Dictionary<ChunkCoords, Chunk>();
        }

        public void GenerateChunk(ChunkCoords coords)
        {
            var chunk = chunks[coords] = generator.GenerateChunk(coords);
            Events.ChunkGenerated(coords, chunk);
        }

        public void UnloadChunk(ChunkCoords coords)
        {
            chunks.Remove(coords);
        }

        public Chunk this[ChunkCoords coords] => chunks[coords];
    }
}
