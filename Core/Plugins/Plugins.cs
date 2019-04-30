using Core.PluginSystems;
using DefaultEcs.System;

namespace Core.Plugins
{
	// TODO: Change name of namespace or class, shouldn't be the same
    public class Plugins
    {
		public readonly AEntitySystem<TimeInfo> Render;
		public readonly ITimeService Time;

        public Plugins(AEntitySystem<TimeInfo> render, ITimeService time)
        {
			Render = render;
			Time = time;
        }
    }
}
