using Core.PluginSystems;
using DefaultEcs.System;

namespace Core.Plugins
{
    public class EnginePlugins
    {
		public readonly ISystem<FrameContext> Render;
		public readonly ITimeService Time;
        public readonly IFactoryPlugin Factory;

        public EnginePlugins(ISystem<FrameContext> render, ITimeService time, IFactoryPlugin factory = null)
        {
			Render = render;
			Time = time;
            Factory = factory;
        }
    }
}
