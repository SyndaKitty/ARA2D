using System;
using Core.Tiles;
using DefaultEcs.System;

namespace Core.Showcase
{
    public class TileRandomizer : AComponentSystem<TickContext, Chunk>
    {
        Random random;

        public TileRandomizer() : base(Engine.World)
        {
            random = new Random((int)TimeSpan.FromDays((DateTime.Now - DateTime.MinValue).Days).TotalMilliseconds);
        }

        protected override void Update(TickContext state, Span<Chunk> components)
        {
            //if (random.NextDouble() > 1f / 60) return;
            foreach (var chunk in components)
            {
                int i = (int)(random.NextDouble() * Chunk.Size * Chunk.Size);
                chunk.Tiles[i] = (short) (random.NextDouble() * 16);
                chunk.TilesChanged = true;
            }
        }
    }
}
