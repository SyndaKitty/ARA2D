using Core.Archetypes;
using Core.Plugins;
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
        public readonly IInputService Input;

        public TickContext(Factory factory, Entity globalEntity, IInputService input)
        {
            Factory = factory;
            GlobalEntity = globalEntity;
            Input = input;
        }
    }
}
