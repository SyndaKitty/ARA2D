using Core.PluginSystems;

namespace MonoGame
{
    public class TimeService : ITimeService
    {
        public TickInfo GetTickInfo()
        {
            // TODO: Implement
            return new TickInfo(1, 1);
        }
    }
}
