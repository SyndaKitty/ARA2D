using DefaultEcs;
using DefaultEcs.System;

namespace Core
{
    public class MoveSystem : AComponentSystem<TickContext, Transform>
    {
        public MoveSystem() : base(Engine.World)
        {
        }

        protected override void Update(TickContext state, ref Transform component)
        {
            component.X++;
        }
    }
}