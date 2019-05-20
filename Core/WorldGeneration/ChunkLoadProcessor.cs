using System;
using Core.Position;
using DefaultEcs;
using DefaultEcs.System;

namespace Core.WorldGeneration
{
    public class LoadRequestProcessor : AEntitySystem<LogicContext>
    {
        public LoadRequestProcessor() : base(Engine.World.GetEntities().With<Global>().Build())
        {
        }

        protected override void Update(LogicContext state, ReadOnlySpan<Entity> entities)
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
