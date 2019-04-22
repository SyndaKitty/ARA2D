using Nez;

namespace ARA2D.Core
{
    public interface IComponentProvider
    {
        T GetComponent<T>() where T : Component;
    }
}
