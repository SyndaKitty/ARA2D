using ARA2D.Core;
using ARA2D.Movement;
using MoonSharp.Interpreter;

namespace ARA2D.Commands
{
    public class MoveAction : CommandAction
    {
        MoveRequester moveRequester;

        public MoveAction(Script script, MoveRequester moveRequester) : base(script)
        {
            this.moveRequester = moveRequester;
        }

        public override void Start(IComponentProvider componentProvider)
        {
            // TODO
        }

        public override bool GetResult(IComponentProvider componentProvider)
        {
            // TODO
            return true;
        }
    }
}
