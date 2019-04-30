using Core.PluginSystems;

namespace Console
{
	public class ConsoleTimeService : ITimeService
	{
		public TickInfo GetTickInfo()
		{
			System.Console.ReadKey();
			return new TickInfo(1, 0);
		}
	}
}
