using DefaultEcs;
using DefaultEcs.System;

namespace Core
{
    public class MoveSystem : AComponentSystem<TickContext, GridTransform>
    {
        public MoveSystem(World world) : base(world)
        {
        }

        protected override void Update(TickContext state, ref GridTransform component)
        {
            component.Coords.LocalX++;
        }
    }
}