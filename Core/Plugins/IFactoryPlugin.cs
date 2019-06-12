using DefaultEcs;

namespace Core.Plugins
{
    public interface IFactoryPlugin
    {
        void Chunk(Entity entity);
        void Building(Entity entity);
        void Global(Entity entity);
        void Camera(Entity entity);
        void BuildingPlacement(Entity entity);
        void CheckBodyPlacement(Entity entity);
        void ChunkBodies(Entity entity);
        void BuildingPlacementGhost(Entity entity);
    }
}
