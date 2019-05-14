using Core.Plugins;
using Core.PluginSystems;
using Core.Position;
using Core.Rendering;
using DefaultEcs;
using DefaultEcs.System;

namespace Core
{
    public class Engine
    {
		public readonly static World World = new World();
		public const float TickLength = .3f;

		readonly ISystem<RenderContext> render;
		readonly ISystem<LogicContext> logic;

		readonly ITimeService timeService;

		readonly RenderContext renderContext = new RenderContext();
		readonly LogicContext logicContext = new LogicContext();

		float accumulatedTime;

        public Engine(EnginePlugins plugins)
        {
			render = plugins.Render;
			// SequentialSystem handles null systems
			logic = new SequentialSystem<LogicContext>(plugins.PreLogic, new GameLogic(World), plugins.PostLogic);
			
			timeService = plugins.Time;

			Initialize();
        }

		public void Render()
		{
			render.Update(renderContext);
		}

		public void Update()
		{
			UpdateLogicContext();	
			logic.Update(logicContext);
		}

		void Initialize()
		{
			var entity = World.CreateEntity();
            TileCoords coords = new TileCoords(0, 1, 0, 2);
			entity.Set(new GridTransform(coords, 3, 3));
            entity.Set(new SpriteLoad("Sprites/Test"));
		}

		void UpdateLogicContext()
		{
			if (timeService.TickMode == TickMode.Automatic)
			{
				accumulatedTime += timeService.DeltaTime;
				logicContext.TicksPassed = (int)(accumulatedTime / TickLength);
				accumulatedTime -= TickLength * logicContext.TicksPassed;
			}
			else if (timeService.TickMode == TickMode.Manual)
			{
				logicContext.TicksPassed = timeService.ForceTick ? 1 : 0;
			}
		}
    }
}
