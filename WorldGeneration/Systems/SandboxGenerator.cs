using ARA2D.Chunks;
using ARA2D.Core;
using ARA2D.WorldGeneration.Components;
using Nez;

namespace ARA2D.WorldGeneration
{
    public class SandboxGenerator : EntityProcessingSystem
    {
        readonly IComponentProvider componentProvider;

        public SandboxGenerator(IComponentProvider componentProvider) : base(new Matcher().all(typeof(ChunkGenerationRequest)))
        {
            this.componentProvider = componentProvider;
        }

        public override void process(Entity entity)
        {
            var grid = componentProvider.GetComponent<Grid>();
            var coords = entity.getComponent<ChunkGenerationRequest>().Coords;
            
            if (!grid.TileChunks.ContainsKey(coords))
            {
                var tileChunk = GenerateTileChunk(coords);
                grid.TileChunks.Add(coords, tileChunk);
                // TODO: Replace this
                Events.TriggerTileChunkGenerated(coords, tileChunk);
            }

            if (!grid.TileEntityChunks.ContainsKey(coords))
            {
                grid.TileEntityChunks.Add(coords, GenerateTileEntityChunk(coords));
            }

            entity.removeComponent<ChunkGenerationRequest>();
            entity.destroy();
        }

        public TileChunk GenerateTileChunk(ChunkCoords coords)
        {
            short[,] tiles = new short[TileChunk.Size,TileChunk.Size];
            for (int y = 0; y < TileChunk.Size; y++)
            {
                for (int x = 0; x < TileChunk.Size; x++)
                {
                    tiles[x, y] = 0;
                }
            }
            return new TileChunk(coords, tiles);
        }

        public TileEntityChunk GenerateTileEntityChunk(ChunkCoords coords)
        {
            return new TileEntityChunk(coords);
        }
    }
}
