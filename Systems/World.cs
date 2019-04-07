using System.Collections.Generic;
using ARA2D.Components;
using ARA2D.WorldGenerators;
using Nez;

namespace ARA2D.Systems
{
    public class World : EntityProcessingSystem
    {
        public TileEntitySystem TileEntitySystem;

        readonly WorldGenerator generator;
        readonly Dictionary<ChunkCoords, Chunk> loadedChunks;


        public World(WorldGenerator generator, TileEntitySystem tileEntitySystem) : base(new Matcher().all(typeof(PassiveChunkGenerate)))
        {
            this.generator = generator;
            loadedChunks = new Dictionary<ChunkCoords, Chunk>();
            TileEntitySystem = tileEntitySystem;
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
            if (!loadedChunks.ContainsKey(coords)) return;
            TileEntitySystem.ChunkUnloaded(coords); // TODO: Maybe do this with events?
            loadedChunks.Remove(coords);
            Events.ChunkRemoved(coords);
        }

        public bool IsChunkLoaded(ChunkCoords coords)
        {
            return loadedChunks.ContainsKey(coords);
        }

        public Chunk RequiredChunk(ChunkCoords coords)
        {
            if (!loadedChunks.ContainsKey(coords)) return GenerateChunk(coords);
            return loadedChunks[coords];
        }

        public Chunk this[ChunkCoords coords] => loadedChunks[coords];
        #endregion Chunks


        public override void process(Entity entity)
        {
            var request = entity.getComponent<PassiveChunkGenerate>();
            GenerateChunk(request.Coords);
            entity.destroy();
        }
    }
}
