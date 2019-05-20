using DefaultEcs;

namespace Core
{
    public class LogicContext
    {
        public int Tick { get; set; }
        public float TickProgress { get; set; }
        public int TicksPassed { get; set; }

        public readonly Entity GlobalEntity;

        public LogicContext(Entity globalEntity)
        {
            GlobalEntity = globalEntity;
        }
    }
}
