using Core.Archetypes;
using Core.Plugins;
using DefaultEcs;

namespace Core
{
    public class FrameContext
    {
        public readonly Entity GlobalEntity;
        public readonly Factory Factory;
        public readonly IInputService Input;

        public float Dt;

        public FrameContext(Factory factory, Entity globalEntity, IInputService input)
        {
            GlobalEntity = globalEntity;
            Factory = factory;
            Input = input;
        }
    }
}
