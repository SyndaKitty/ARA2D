
using System.Collections.Generic;
using ARA2D.Movement;
using MoonSharp.Interpreter;
using Nez;

namespace ARA2D.Commands
{
    public class CommandActions : Component
    {
        public List<CommandAction> Actions = new List<CommandAction>();

        public int Add(Script script, CommandActionType type, MoveRequester moveRequester)
        {
            switch (type)
            {
                case CommandActionType.Wait:
                    Actions.Add(new WaitAction(script));
                    break;
                case CommandActionType.Move:
                    Actions.Add(new MoveAction(script, moveRequester));
                    break;
                case CommandActionType.Back:
                    Actions.Add(new MoveAction(script, moveRequester));
                    break;
                case CommandActionType.Right:
                    Actions.Add(new TurnAction(script));
                    break;
                case CommandActionType.Left:
                    Actions.Add(new TurnAction(script));
                    break;
            }

            return Actions.Count - 1;
        }

        public CommandAction this[int i] => Actions[i];
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
