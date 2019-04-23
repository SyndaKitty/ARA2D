
using System.Collections.Generic;
using Nez;

namespace ARA2D.Commands
{
    public class CommandActions : Component
    {
        public List<CommandAction> Actions = new List<CommandAction>();

        public int Add(CommandActionType type)
        {
            switch (type)
            {
                case CommandActionType.Wait:
                    Actions.Add(new WaitAction());
                    break;
                case CommandActionType.Move:
                    Actions.Add(new MoveAction());
                    break;
                case CommandActionType.Back:
                    Actions.Add(new MoveAction());
                    break;
                case CommandActionType.Right:
                    Actions.Add(new TurnAction());
                    break;
                case CommandActionType.Left:
                    Actions.Add(new TurnAction());
                    break;
            }

            return Actions.Count - 1;
        }

        public CommandAction this[int i] => Actions[i];
    }
}
