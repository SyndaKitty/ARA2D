using ARA2D.Commands.Components;
using Nez;

namespace ARA2D.Commands.Systems
{
    public class CommandScriptRunner : EntityProcessingSystem
    {
        public CommandScriptRunner() : base(new Matcher().all(typeof(CommandScript)))
        {
        }

        public override void process(Entity entity)
        {
        }
    }
}
