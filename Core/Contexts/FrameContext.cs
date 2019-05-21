using DefaultEcs;

namespace Core
{
    public class FrameContext
    {
        public readonly Entity GlobalEntity;

        public FrameContext(Entity globalEntity)
        {
            GlobalEntity = globalEntity;
        }
    }
}
