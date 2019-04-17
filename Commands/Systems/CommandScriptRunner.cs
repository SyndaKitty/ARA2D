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
            var commandScript = entity.getComponent<CommandScript>();
            if (!commandScript.Running) return;
            if (commandScript.CommandCalls.Count <= commandScript.CurrentLine) return;

            var nextCall = commandScript.CommandCalls[commandScript.CurrentLine];
            RunCommand(nextCall);
        }

        public static void RunCommand(CommandCall command)
        {

        }
    }
}
