using Core.PluginSystems;

namespace MonoGame
{
    public class TimeService : ITimeService
    {
        public float DeltaTime { get; set; }

        public TickMode TickMode => TickMode.Automatic;

        public bool ForceTick => false;
    }
}
