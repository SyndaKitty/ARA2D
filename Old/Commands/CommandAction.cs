using ARA2D.Core;
using MoonSharp.Interpreter;

namespace ARA2D.Commands
{
    public abstract class CommandAction
    {
        public Script Script { get; }

        protected CommandAction(Script script)
        {
            Script = script;
        }

        public abstract void Start(IComponentProvider componentProvider);
        public abstract bool GetResult(IComponentProvider componentProvider);
    }
}
