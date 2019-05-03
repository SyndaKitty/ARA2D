using DefaultEcs;
using DefaultEcs.System;

namespace Core
{
	public class GameLogic : AEntitySystem<LogicContext>
	{
		readonly ISystem<LogicContext> wrappedSystems;

		public GameLogic(World world) : base(world)
		{
			wrappedSystems = new SequentialSystem<LogicContext>
			(
				// TODO: Game logic systems go here	
				new MoveSystem(world)
			);
		}

		protected override void PreUpdate(LogicContext state)
		{
			// TODO: Be a little bit smarter about this. 
			// We could spread tick logic throughout several frames instead.
			for (int i = 0; i < state.TicksPassed; i++)
			{
				wrappedSystems?.Update(state);
			}
		}

		protected override void PostUpdate(LogicContext state)
		{
			state.TicksPassed = 0;
		}
	}
}