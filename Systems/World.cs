using System.Collections.Generic;
using ARA2D.Components;
using ARA2D.WorldGenerators;
using Nez;

namespace ARA2D.Systems
{
    public class World : EntityProcessingSystem
    {
        readonly WorldGenerator generator;
        readonly Dictionary<ChunkCoords, Chunk> loadedChunks;
        readonly Dictionary<int, TileEntity> tileEntities;

        public World(WorldGenerator generator) : base(new Matcher().all(typeof(PassiveChunkGenerate)))
        {
            this.generator = generator;
            loadedChunks = new Dictionary<ChunkCoords, Chunk>();
            tileEntities = new Dictionary<int, TileEntity>();
        }

        #region Chunks
        public Chunk GenerateChunk(ChunkCoords coords)
        {
            if (loadedChunks.ContainsKey(coords)) return loadedChunks[coords];
            var chunk = generator.GenerateChunk(coords);
            Events.ChunkGenerated(coords, chunk);
            return loadedChunks[coords] = chunk;
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
        #endregion Chunks

        #region TileEntities
        public bool IsTileEntityLoaded(int id)
        {
            return tileEntities.ContainsKey(id);
        }

        public void SetTileEntity(int id, TileEntity entity)
        {
            tileEntities[id] = entity;
        }
        #endregion TileEntities

        public Chunk this[ChunkCoords coords] => loadedChunks[coords];

        public override void process(Entity entity)
        {
            var request = entity.getComponent<PassiveChunkGenerate>();
            GenerateChunk(request.Coords);
            entity.destroy();
        }
    }
}
