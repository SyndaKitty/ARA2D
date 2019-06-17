using Core.Archetypes;
using DefaultEcs;
using DefaultEcs.System;

namespace Core.Movement
{
    public class ComputerMovement : AEntitySystem<TickContext>
    {
        public ComputerMovement(Factory factory) : base(factory.ComputerSet)
        {
        }

        protected override void Update(TickContext state, in Entity entity)
        {
            state.Factory.RequestMovement(Direction.Right, entity);
        }
    }
}
