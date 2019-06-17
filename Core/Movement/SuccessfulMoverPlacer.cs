using System;
using System.Numerics;
using Core.Archetypes;
using Core.TileBodies;
using DefaultEcs;
using DefaultEcs.System;

namespace Core.Movement
{
    public class SuccessfulMoverPlacer : AEntitySystem<TickContext>
    {
        public SuccessfulMoverPlacer(Factory factory) : base(factory.MovementResultsSet)
        {
        }

        protected override void Update(TickContext state, ReadOnlySpan<Entity> entities)
        {
            foreach (var entity in entities)
            {
                var movement = entity.Get<MovementResults>();
                if (!movement.Success) continue;
                var tileBody = entity.Get<TileBody>();
                var gridTransform = entity.Get<GridTransform>();
                state.Factory.GetChunkBodies(movement.To).Bodies[movement.To.Index] = tileBody.ID;
                gridTransform.Coords = movement.To;

                //if (!entity.Has<Transform>())
                {
                    entity.Set(new Transform(movement.From.ToVector2()));
                }
            }
        }
    }
}
