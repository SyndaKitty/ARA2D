using Core.Archetypes;
using Core.Plugins;
using Core.PluginSystems;
using Core.Position;
using Core.WorldGeneration;
using DefaultEcs;
using DefaultEcs.System;

namespace Core
{
    public class Engine
    {
		public static readonly World World = new World();
		public const float TickLength = .3f;

        readonly ISystem<FrameContext> frameSystems;
        readonly ISystem<TickContext> tickSystems;

        readonly ITimeService timeService;
        readonly Factory factory;

        FrameContext frameContext;
        TickContext tickContext;
        
		float accumulatedTime;

        public Engine(EnginePlugins plugins)
        {
			// SequentialSystem handles null systems
			frameSystems = new SequentialSystem<FrameContext>(new FrameLogic(), plugins.Render);
			tickSystems = new SequentialSystem<TickContext>(new TickLogic());
			
			timeService = plugins.Time;
            factory = new Factory(plugins.Factory);

            Initialize();
        }

        public void Render()
        {
            frameSystems.Update(frameContext);
        }

        public void Update()
        {
            UpdateTickContext();	
            tickSystems.Update(tickContext);
        }

		void Initialize()
        {
            var globalEntity = factory.CreateGlobal();
            factory.CreateBuilding(0, 6, 0, 6);
            var coords = new TileCoords(0, 0, 0, 0);
            var worldGenerator = new WorldGenerator();
            factory.CreateChunk(coords, worldGenerator.GenerateChunk(coords));

            frameContext = new FrameContext(factory, globalEntity);
            tickContext = new TickContext(factory, globalEntity);
        }

        void UpdateTickContext()
        {
            if (timeService.TickMode == TickMode.Automatic)
            {
                accumulatedTime += timeService.DeltaTime;
                tickContext.TicksPassed = (int)(accumulatedTime / TickLength);
                accumulatedTime -= TickLength * tickContext.TicksPassed;
            }
            else if (timeService.TickMode == TickMode.Manual)
            {
                tickContext.TicksPassed = timeService.ForceTick ? 1 : 0;
            }
        }
    }
}
