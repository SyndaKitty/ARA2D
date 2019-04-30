namespace Core
{
	public class TimeInfo
	{
		// Whether or not a tick passed 
		// ... (or if the TimeInfo is pissed)
		public bool Ticked;

		public float PercentProgress;

		public TimeInfo(bool ticked, float progress)
		{
			Ticked = ticked;
			PercentProgress = progress;
		}
	}
}
