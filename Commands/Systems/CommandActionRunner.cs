using ARA2D.Core;

namespace ARA2D.Commands
{
    public class CommandActionRunner
    {
        IComponentProvider componentProvider;

        public CommandActionRunner(IComponentProvider componentProvider)
        {
            this.componentProvider = componentProvider;
        }
    }

    public enum CommandActionType
    {
        Wait = -1,
        Move,
        Back,
        Right,
        Left
    }
}
