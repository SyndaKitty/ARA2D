using ARA2D.Components;
using Nez;
using System;

namespace ARA2D.Systems
{
    public class WorldGenerator : EntityProcessingSystem
    {
        // TODO: Think of a way to remove this circular dependency
        public World world;

        public WorldGenerator() : base(new Matcher().all(typeof(PassiveChunkGenerate)))
        {
        }

        public override void process(Entity entity)
        {
            GenerateChunk(entity.getComponent<PassiveChunkGenerate>().Coords);
            entity.destroy();
        }

        public void GenerateChunk(ChunkCoords coords)
        {
            if (world.IsChunkLoaded(coords))
            {
                return;
            }

            // TODO: Generate chunks with Perlin/Simplex noise
            var chunk = new Chunk(coords);
            Events.ChunkGenerated(coords, chunk);
            world.SetChunk(coords, chunk);
        }
    }
}
