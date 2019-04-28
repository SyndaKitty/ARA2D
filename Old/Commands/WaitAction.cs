using ARA2D.Core;
using MoonSharp.Interpreter;

namespace ARA2D.Commands
{
    public class WaitAction : CommandAction
    {
        public WaitAction(Script script) : base(script)
        {
        }

        public override void Start(IComponentProvider componentProvider)
        {
            
        }

        public override bool GetResult(IComponentProvider componentProvider)
        {
            return true;
        }
    }
}
