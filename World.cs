using ARA2D.Systems;
using System.Collections.Generic;

namespace ARA2D
{
    public class World
    {
        readonly WorldGenerator generator;
        readonly Dictionary<ChunkCoords, Chunk> loadedChunks;
        readonly Dictionary<int, TileEntity> tileEntities;

        public World(WorldGenerator generator)
        {
            this.generator = generator;
            loadedChunks = new Dictionary<ChunkCoords, Chunk>();
            tileEntities = new Dictionary<int, TileEntity>();
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

        public bool IsTileEntityLoaded(int id)
        {
            return tileEntities.ContainsKey(id);
        }

        public void SetTileEntity(int id, TileEntity entity)
        {
            tileEntities[id] = entity;
        }

        public Chunk this[ChunkCoords coords] => loadedChunks[coords];
    }
}
