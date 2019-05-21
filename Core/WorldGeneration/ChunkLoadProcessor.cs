using System;
using Core.Position;
using DefaultEcs;
using DefaultEcs.System;

namespace Core.WorldGeneration
{
    public class ChunkLoadProcessor : AEntitySystem<TickContext>
    {
        public ChunkLoadProcessor() : base(Engine.World.GetEntities().With<Global>().Build())
        {
        }

        protected override void Update(TickContext state, ReadOnlySpan<Entity> entities)
        {
            var global = entities[0];
            var requests = global.Get<ChunkLoadRequests>();
            foreach (TileCoords request in requests.Requests)
            {
                // TODO: Generate chunk
            }
        }
    }
}
