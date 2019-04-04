using ARA2D.Systems;
using System.Collections.Generic;

namespace ARA2D
{
    public class World
    {
        WorldGenerator generator;
        Dictionary<ChunkCoords, Chunk> loadedChunks;

        public World(WorldGenerator generator)
        {
            this.generator = generator;
            loadedChunks = new Dictionary<ChunkCoords, Chunk>();
        }

        public void GenerateChunk(ChunkCoords coords)
        {
            if (loadedChunks.ContainsKey(coords)) return; 
            generator.GenerateChunk(coords); 
        }

        public void UnloadChunk(ChunkCoords coords)
        {
            loadedChunks.Remove(coords);
            Events.ChunkRemoved(coords);
        }

        public bool IsChunkLoaded(ChunkCoords coords)
        {
            return loadedChunks.ContainsKey(coords);
        }

        public void SetChunk(ChunkCoords coords, Chunk chunk)
        {
            loadedChunks[coords] = chunk;
        }

        public Chunk this[ChunkCoords coords] => loadedChunks[coords];
    }
}
