using System;
using Core.Tiles;
using DefaultEcs.System;

namespace Core.Showcase
{
    public class TileRandomizer : AComponentSystem<FrameContext, Chunk>
    {
        Random random;

        public TileRandomizer() : base(Engine.World)
        {
            random = new Random((int)TimeSpan.FromDays((DateTime.Now - DateTime.MinValue).Days).TotalMilliseconds);
        }

        protected override void Update(FrameContext state, Span<Chunk> components)
        {
            //if (random.NextDouble() > 1f / 60) return;
            foreach (var chunk in components)
            {
                for (int tile = 0; tile < 10; tile++)
                {
                    int i = (int)(random.NextDouble() * Chunk.Size * Chunk.Size);
                    chunk.Tiles[i] = (short) (random.NextDouble() * 5);
                    chunk.TilesChanged = true;
                }
            }
        }
    }
}
