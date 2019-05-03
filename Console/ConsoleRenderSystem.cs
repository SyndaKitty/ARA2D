using System;
using System.Collections.Generic;
using System.Text;
using Core;
using DefaultEcs;
using DefaultEcs.System;

namespace Console
{
	public class ConsoleRenderSystem : AEntitySystem<RenderContext>
	{
		public const int Width = 10;
		public const int Height = 10;

		StringBuilder line = new StringBuilder(Width * 2);

		public ConsoleRenderSystem(World world) : base(world.GetEntities().With<GridTransform>().Build())
		{
			System.Console.CursorVisible = false;
		}

		protected override void Update(RenderContext state, ReadOnlySpan<Entity> entities)
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
						line.Append("X ");
					}
					else
					{
						line.Append("  ");
					}
				}
				System.Console.WriteLine(line.ToString());
				line.Clear();
			}
		}
	}
}
