using System.Collections.Generic;
using Nez;
using Coroutine = MoonSharp.Interpreter.Coroutine;

namespace ARA2D.Commands.Components
{
    public class CommandScript : Component
    {
        public List<CommandCall> CommandCalls;
        public bool Running => Status == ScriptStatus.Running;
        public int CurrentLine;
        public Coroutine Coroutine;

        public ScriptStatus Status;
        public string StatusDescription;

        public CommandScript(List<CommandCall> commandCalls)
        {
            CommandCalls = commandCalls;
        }
    }

    public enum ScriptStatus
    {
        //NotStarted,
        Running,
        CommandNotFound,
        Error,
        Done
    }
}
