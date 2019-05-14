using DefaultEcs;
using DefaultEcs.System;

namespace Core
{
	public class MoveSystem : AComponentSystem<LogicContext, GridTransform>
	{
		public MoveSystem(World world) : base(world)
		{
		}

		protected override void Update(LogicContext state, ref GridTransform component)
		{
			component.Coords.LocalX++;
		}
	}
}