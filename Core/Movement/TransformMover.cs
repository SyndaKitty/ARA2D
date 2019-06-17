using System;
using Core.Archetypes;
using DefaultEcs;
using DefaultEcs.System;

namespace Core.Movement
{
    public class TransformMover : AEntitySystem<FrameContext>
    {
        public TransformMover(Factory factory) : base(factory.MovementResultsSet)
        {
        }

        protected override void Update(FrameContext state, ReadOnlySpan<Entity> entities)
        {
            foreach (var entity in entities)
            {
                var movement = entity.Get<MovementResults>();
                if (!movement.Success) continue;
                var transform = entity.Get<Transform>();
                var from = movement.From.ToVector2();
                var to = movement.To.ToVector2();
                var current = from + (to - from) * state.TickProgress;
                transform.X = current.X;
                transform.Y = current.Y;
            }
        }
    }
}
