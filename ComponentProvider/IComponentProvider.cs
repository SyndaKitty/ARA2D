using Nez;

namespace ARA2D.ComponentProvider
{
    public interface IComponentProvider
    {
        T GetComponent<T>() where T : Component;
    }
}
