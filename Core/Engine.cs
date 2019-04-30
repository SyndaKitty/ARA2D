using Core.PluginSystems;
using DefaultEcs;
using DefaultEcs.System;

namespace Core
{
    public class Engine
    {
		public readonly static World World = new World();

		// Systems that need to run every frame
		readonly SequentialSystem<TimeInfo> frameSystems;
		
		// Systems that need to run once per tick
		readonly SequentialSystem<TimeInfo> tickSystems;

		readonly ITimeService timeService;

        public Engine(Plugins.Plugins plugins)
        {
			frameSystems = new SequentialSystem<TimeInfo>
				(
					plugins.Render
				);

			tickSystems = new SequentialSystem<TimeInfo>();

			timeService = plugins.Time;

			Initialize();
        }

		void Initialize()
		{
			var entity = World.CreateEntity();
			entity.Set(new GridTransform(1, 2, 3, 4));
		}

		public void Update()
		{
			TickInfo tick = timeService.GetTickInfo();

			// TODO: Perhaps we could batch together per-frame and per-tick systems?
			TimeInfo frameTime = new TimeInfo(false, tick.PercentProgress);
			frameSystems.Update(frameTime);

			// TODO: Change this to spread tick logic over several frames, instead of ticking frame
			// We would need to keep track of systems that have been run already
			TimeInfo tickTime = new TimeInfo(true, 0);
			for (int i = 0; i < tick.TicksPassed; i++)
			{
				tickSystems.Update(tickTime);
			}
		}
    }
}
