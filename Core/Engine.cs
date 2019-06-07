using System.Numerics;
using Core.Archetypes;
using Core.Plugins;
using Core.PluginSystems;
using Core.Position;
using Core.TileBodies;
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
        readonly IInputService inputService;
        readonly Factory factory;

        FrameContext frameContext;
        TickContext tickContext;
        
		float accumulatedTime;

        public Engine(EnginePlugins plugins)
        {
            inputService = plugins.Input;
			timeService = plugins.Time;
            factory = new Factory(plugins.Factory);
			
            // SequentialSystem handles null systems
			frameSystems = new SequentialSystem<FrameContext>(new FrameLogic(factory), plugins.Render);
			tickSystems = new SequentialSystem<TickContext>(new TickLogic(factory));

            Initialize();
        }

        public void Render()
        {
            frameContext.Dt = timeService.DeltaTime;
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

            // Building
            factory.PlaceBuilding(PlacementType.Place, new TileCoords(0, 6, 0, 6), 4, 4);

            // Chunk
            var coords = new TileCoords(0, 0, 0, 0);
            var worldGenerator = new WorldGenerator();
            factory.CreateChunk(coords, worldGenerator.GenerateChunk(coords));

            // Camera
            factory.CreateCamera(Vector2.Zero);

            frameContext = new FrameContext(factory, globalEntity, inputService);
            tickContext = new TickContext(factory, globalEntity, inputService);
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
