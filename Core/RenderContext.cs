using DefaultEcs;

namespace Core
{
    public class RenderContext
    {
        public readonly Entity GlobalEntity;

        public RenderContext(Entity globalEntity)
        {
            GlobalEntity = globalEntity;
        }
    }
}
