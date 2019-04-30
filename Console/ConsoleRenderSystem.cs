using System;
using System.Collections.Generic;
using Core;
using DefaultEcs;
using DefaultEcs.System;

namespace Console
{
	public class ConsoleRenderSystem : AEntitySystem<TimeInfo>
	{
		public ConsoleRenderSystem(World world) : base(world.GetEntities().With<GridTransform>().Build())
		{
		}

		protected override void Update(TimeInfo state, ReadOnlySpan<Entity> entities)
		{
			HashSet<Tuple<int, int>> GridSet = new HashSet<Tuple<int, int>>();
			foreach (var entity in entities)
			{
				GridTransform grid = entity.Get<GridTransform>();
				for (int y = grid.Y; y < grid.Y + grid.Height; y++)
				{
					for (int x = grid.X; x < grid.X + grid.Width; x++)
					{
						GridSet.Add(new Tuple<int, int>(x, y));
					}
				}
			}

			System.Console.SetCursorPosition(0, 0);
			for (int y = 0; y < 10; y++)
			{
				for (int x = 0; x < 10; x++)
				{
					if (GridSet.Contains(new Tuple<int, int>(x, y)))
					{
						System.Console.Write("X ");
					}
					else
					{
						System.Console.Write("  ");
					}
				}
				System.Console.WriteLine();
			}
		}
	}
}
