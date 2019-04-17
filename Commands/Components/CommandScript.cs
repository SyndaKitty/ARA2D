using System.Collections.Generic;
using Nez;

namespace ARA2D.Commands.Components
{
    public class CommandScript : Component
    {
        public List<CommandCall> CommandCalls;
        public bool Running;
        public int CurrentLine;

        public CommandScript(List<CommandCall> commandCalls)
        {
            CommandCalls = commandCalls;
        }
    }
}
