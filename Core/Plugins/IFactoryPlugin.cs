using DefaultEcs;

namespace Core.Plugins
{
    public interface IFactoryPlugin
    {
        void Chunk(Entity entity);
        void Building(Entity entity);
    }
}
