namespace Core.PluginSystems
{
	public interface ITimeService
	{
		TickInfo GetTickInfo();
	}

	public class TickInfo
	{
		// The number of ticks that passed since the last update
		public int TicksPassed;

		// The amount of progress through the current tick
		public float PercentProgress;

		public TickInfo(int ticksPassed, float percentProgress)
		{
			TicksPassed = ticksPassed;
			PercentProgress = percentProgress;
		}
	}
}
