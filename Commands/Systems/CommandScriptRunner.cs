using ARA2D.Commands.Components;
using ARA2D.ComponentProvider;
using MoonSharp.Interpreter;
using Nez;

namespace ARA2D.Commands.Systems
{
    public class CommandScriptRunner : EntityProcessingSystem
    {
        readonly IComponentProvider componentProvider;

        public CommandScriptRunner(IComponentProvider componentProvider) : base(new Matcher().all(typeof(CommandScript)))
        {
            this.componentProvider = componentProvider;
        }

        public override void process(Entity entity)
        {
            var commandScript = entity.getComponent<CommandScript>();
            if (!commandScript.Running) return;
            if (commandScript.CommandCalls.Count <= commandScript.CurrentLine)
            {
                commandScript.Status = ScriptStatus.Done;
            }

            var nextCall = commandScript.CommandCalls[commandScript.CurrentLine];
            RunCommand(commandScript, nextCall);
        }

        public void RunCommand(CommandScript script, CommandCall command)
        {
            var repo = componentProvider.GetComponent<CommandRepo>();
            // Check if the command exists
            if (!repo.Commands.ContainsKey(command.Name))
            {
                script.Status = ScriptStatus.CommandNotFound;
                script.StatusDescription = $"Unable to find command \"{command.Name}\"";
                return;
            }

            var args = DynValue.NewString(command.Arguments);
            repo.Commands[command.Name].Call(args);
        }
    }
}
