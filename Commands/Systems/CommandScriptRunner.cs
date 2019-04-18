using System;
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

            // TODO: Take this test code out
            //commandScript.Status = ScriptStatus.Running;

            if (!commandScript.Running) return;
            if (commandScript.CommandCalls.Count <= commandScript.CurrentLine)
            {
                commandScript.Status = ScriptStatus.Done;
            }

            var nextCall = commandScript.CommandCalls[commandScript.CurrentLine];

            if (commandScript.Coroutine == null)
            {
                CreateCoroutine(commandScript, nextCall);
            }
            RunCoroutine(commandScript);
            if (commandScript.Coroutine == null && ++commandScript.CurrentLine >= commandScript.CommandCalls.Count)
            {
                commandScript.Status = ScriptStatus.Done;
            }
        }

        public void CreateCoroutine(CommandScript script, CommandCall command)
        {
            Console.WriteLine($"Creating coroutine for {command.Name}");
            var repo = componentProvider.GetComponent<CommandRepository>();

            if (!repo.Commands.ContainsKey(command.Name))
            {
                script.Status = ScriptStatus.CommandNotFound;
                script.StatusDescription = $"Unable to find command \"{command.Name}\"";
                return;
            }

            DynValue args;
            if (string.IsNullOrEmpty(command.Arguments))
            {
                args = DynValue.Nil;
            }
            else
            {
                args = DynValue.NewString(command.Arguments);
            }
            repo.Script.Globals["args"] = args;
            script.Coroutine = repo.Script.CreateCoroutine(repo.Commands[command.Name]).Coroutine;
        }

        public void RunCoroutine(CommandScript script)
        {
            DynValue result = script.Coroutine.Resume();
            if (result.Type == DataType.Void)
            {
                script.Coroutine = null;
            }
            else
            {
                // TODO: Do something with yielded value
                Console.WriteLine($"Yielded: {result.Number}");
            }
        }
    }
}
