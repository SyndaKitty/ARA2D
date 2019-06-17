using System;
using Core.Archetypes;
using DefaultEcs;
using DefaultEcs.System;

namespace Core.Movement
{
    public class SuccessfulMoveRemover : AEntitySystem<TickContext>
    {
        public SuccessfulMoveRemover(Factory factory) : base(factory.MovementResultsSet)
        {
        }

        protected override void Update(TickContext state, ReadOnlySpan<Entity> entities)
        {
            foreach (var entity in entities)
            {
                var movement = entity.Get<MovementResults>();
                if (!movement.Success) continue;
                state.Factory.GetChunkBodies(movement.From).Bodies[movement.From.Index] = -1;
            }
        }
    }
}
