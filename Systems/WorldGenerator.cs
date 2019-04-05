using System.Xml.Serialization;
using ARA2D.Components;
using Nez;

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
            short[,] tiles = new short[Chunk.Size,Chunk.Size];
            for (int y = 0; y < Chunk.Size; y++)
            {
                for (int x = 0; x < Chunk.Size; x++)
                {
                    tiles[x, y] = 0;
                }
            }

            var chunk = new Chunk(coords, tiles);
            Events.ChunkGenerated(coords, chunk);
            world.SetChunk(coords, chunk);
        }
    }
}
