using ARA2D.Core;

namespace ARA2D.Commands
{
    public class WaitAction : CommandAction
    {
        public void Start(IComponentProvider componentProvider)
        {
            
        }

        public bool GetResult(IComponentProvider componentProvider)
        {
            return true;
        }
    }
}
