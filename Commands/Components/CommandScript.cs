using System.Collections.Generic;
using Nez;

namespace ARA2D.Commands.Components
{
    public class CommandScript : Component
    {
        public List<CommandCall> CommandCalls;

        public CommandScript(List<CommandCall> commandCalls)
        {
            CommandCalls = commandCalls;
        }
    }

    public struct CommandCall
    {
        public string CommandName;
        public string Arguments;

        public CommandCall(string commandName, string arguments)
        {
            CommandName = commandName;
            Arguments = arguments;
        }
    }
}
