using Core.Archetypes;
using DefaultEcs;

namespace Core
{
    public class FrameContext
    {
        public readonly Entity GlobalEntity;
        public readonly Factory Factory;

        public FrameContext(Factory factory, Entity globalEntity)
        {
            GlobalEntity = globalEntity;
            Factory = factory;
        }
    }
}
