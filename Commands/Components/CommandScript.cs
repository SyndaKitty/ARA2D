using System.Collections.Generic;
using MoonSharp.Interpreter;
using Nez;
using Coroutine = MoonSharp.Interpreter.Coroutine;

namespace ARA2D.Commands
{
    public class CommandScript : Component
    {
        public List<CommandCall> CommandCalls;
        public bool Running => Status == ScriptStatus.Running;
        public int CurrentLine;
        public Coroutine Coroutine;
        public bool ReceivedYield;
        public int MoveResultIndex = -1;

        public Script Lua;
        public ScriptStatus Status;
        public string StatusDescription;

        public CommandScript(Script lua, List<CommandCall> commandCalls)
        {
            CommandCalls = commandCalls;
            Lua = lua;
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
