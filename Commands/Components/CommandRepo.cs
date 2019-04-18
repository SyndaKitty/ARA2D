using System.Collections.Generic;
using MoonSharp.Interpreter;
using Nez;

namespace ARA2D.Commands.Components
{
    public class CommandRepo : Component
    {
        public Dictionary<string, DynValue> Commands;

        public CommandRepo()
        {
            Commands = new Dictionary<string, DynValue>();
        }
    }
}
