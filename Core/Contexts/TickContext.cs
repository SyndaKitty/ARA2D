using Core.Archetypes;
using DefaultEcs;

namespace Core
{
    public class TickContext
    {
        public int TickNumber { get; set; }
        public float TickProgress { get; set; }
        public int TicksPassed { get; set; }

        public readonly Entity GlobalEntity;
        public readonly Factory Factory;

        public TickContext(Factory factory, Entity globalEntity)
        {
            Factory = factory;
            GlobalEntity = globalEntity;
        }
    }
}
