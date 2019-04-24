using System.Collections.Generic;
using ARA2D.Core;
using Nez;

namespace ARA2D.Commands
{
    public class ActionRunner : ProcessingSystem
    {
        readonly IComponentProvider componentProvider;

        public ActionRunner(IComponentProvider componentProvider)
        {
            this.componentProvider = componentProvider;
        }

        public override void process()
        {
            List<CommandAction> actions = componentProvider.GetComponent<CommandActions>().Actions;
            foreach (CommandAction action in actions)
            {
                action.Start(componentProvider);
            }
        }
    }
}
