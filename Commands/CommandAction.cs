using ARA2D.Core;

namespace ARA2D.Commands
{
    public interface CommandAction
    {
        void Start(IComponentProvider componentProvider);
        bool GetResult(IComponentProvider componentProvider);
    }
}
