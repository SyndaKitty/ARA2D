using Core.PluginSystems;

namespace Console
{
    public class ConsoleTimeService : ITimeService
    {
        public float DeltaTime { get; } = 20f / 1000;

        public TickMode TickMode { get; set; } = TickMode.Manual;

        public bool ForceTick { get; set; }
    }
}
