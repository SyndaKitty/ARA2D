using Core.PluginSystems;
using DefaultEcs.System;

namespace Core.Plugins
{
    public class EnginePlugins
    {
        public readonly ISystem<RenderContext> Render;
        public readonly ISystem<LogicContext> PreLogic;
        public readonly ISystem<LogicContext> PostLogic;
        public readonly ITimeService Time;

        public EnginePlugins(ISystem<RenderContext> render, ITimeService time, ISystem<LogicContext> preLogic = null, ISystem<LogicContext> postLogic = null)
        {
            Render = render;
            PreLogic = preLogic;
            PostLogic = postLogic;
            Time = time;
        }
    }
}
