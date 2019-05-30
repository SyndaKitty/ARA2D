using System;
using Core.Position;
using DefaultEcs;
using DefaultEcs.System;

namespace Core.WorldGeneration
{
    public class ChunkLoadProcessor : AEntitySystem<TickContext>
    {
        readonly WorldGenerator worldGenerator;

        public ChunkLoadProcessor() : base(Engine.World.GetEntities().With<Global>().Build())
        {
            worldGenerator = new WorldGenerator();
        }

        protected override void Update(TickContext state, ReadOnlySpan<Entity> entities)
        {
            var global = entities[0];
            var requests = global.Get<ChunkLoadRequests>().Requests;
            var cache = global.Get<ChunkCache>();
            foreach (TileCoords requestCoords in requests)
            {
                if (cache.ChunkLookup.ContainsKey(requestCoords)) continue;
                var chunk = worldGenerator.GenerateChunk(requestCoords);
                state.Factory.CreateChunk(requestCoords, chunk);
            }
            requests.Clear();
        }
    }
}
