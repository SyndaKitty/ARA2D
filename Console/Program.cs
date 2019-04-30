using Core;
using Core.Plugins;

namespace Console
{
	public class Program
	{
		static void Main(string[] args)
		{
			ConsoleRenderSystem render = new ConsoleRenderSystem(Engine.World);
			ConsoleTimeService time = new ConsoleTimeService();

			Plugins plugins = new Plugins(render, time);
			Engine engine = new Engine(plugins);

			while (true)
			{
				engine.Update();
			}
		}
	}
}
