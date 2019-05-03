namespace Core.PluginSystems
{
	public interface ITimeService
	{
		float DeltaTime { get; }
		TickMode TickMode { get; }
		bool ForceTick { get; }
	}

	public enum TickMode
	{
		// The engine will only tick if the ForceTick flag is set
		Manual,
		// The engine will calculate ticks based on DeltaTime
		Automatic
	}
}
