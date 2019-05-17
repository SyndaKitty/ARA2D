using Core;
using DefaultEcs;
using DefaultEcs.System;

namespace Console
{
    public class ConsoleCommandRunner : AEntitySystem<LogicContext>
    {
        ConsoleTimeService time;

        public ConsoleCommandRunner(ConsoleTimeService time) : base(Engine.World.GetEntities().With<ConsoleCommand>().Build())
        {
            this.time = time;
        }

        protected override void Update(LogicContext state, in Entity entity)
        {
            var command = entity.Get<ConsoleCommand>();
            entity.Remove<ConsoleCommand>();

            var txt = command.Text.ToLower();

            if (txt == "")
            {
                time.ForceTick = true;
            }
            else if (txt == "auto")
            {
                time.TickMode = Core.PluginSystems.TickMode.Automatic;
            }
            else if (txt == "manual")
            {
                time.TickMode = Core.PluginSystems.TickMode.Manual;
            }
        }
    }
}