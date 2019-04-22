using System.Collections.Generic;
using ARA2D.Chunks;
using ARA2D.Core;
using ARA2D.WorldGeneration;
using Nez;

namespace ARA2D.Systems
{
    public class World : ProcessingSystem
    {
        // TODO: True ECS refactor
        readonly WorldGenerator generator;
        readonly Dictionary<ChunkCoords, TileChunk> loadedChunks;

        public World(WorldGenerator generator)
        {
            this.generator = generator;
            loadedChunks = new Dictionary<ChunkCoords, TileChunk>();
            Events.OnPassiveTileChunkRequest += (coords) => GenerateChunk(coords);
        }

        #region Chunks
        public TileChunk GenerateChunk(ChunkCoords coords)
        {
            if (loadedChunks.ContainsKey(coords)) return loadedChunks[coords];
            var chunk = generator.GenerateChunk(coords);
            Events.TriggerTileChunkGenerated(coords, chunk);
            return loadedChunks[coords] = chunk;
        }

        public void UnloadChunk(ChunkCoords coords)
        {
            if (!loadedChunks.ContainsKey(coords)) return;
            loadedChunks.Remove(coords);
            Events.TriggerTileChunkRemoved(coords);
        }

        public bool IsChunkLoaded(ChunkCoords coords)
        {
            return loadedChunks.ContainsKey(coords);
        }

        public TileChunk RequiredChunk(ChunkCoords coords)
        {
            if (!loadedChunks.ContainsKey(coords)) return GenerateChunk(coords);
            return loadedChunks[coords];
        }

        public TileChunk this[ChunkCoords coords] => loadedChunks[coords];
        #endregion Chunks

        public override void process()
        {
        }
    }
}
