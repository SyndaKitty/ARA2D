using System.Collections.Generic;
using ARA2D.Core;
using MoonSharp.Interpreter;
using Nez;

namespace ARA2D.Commands.Systems
{
    public class ActionResultsWriter : ProcessingSystem
    {
        IComponentProvider componentProvider;

        public ActionResultsWriter(IComponentProvider componentProvider)
        {
            this.componentProvider = componentProvider;
        }

        public override void process()
        {
            List<CommandAction> actions = componentProvider.GetComponent<CommandActions>().Actions;

            foreach (CommandAction action in actions)
            {
                var res = action.GetResult(componentProvider);
                action.Script.Globals["res"] = res ? DynValue.True : DynValue.False;
            }

            actions.Clear();
        }
    }
}
