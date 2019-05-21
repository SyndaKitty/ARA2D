using DefaultEcs.System;

namespace Core
{
    public class FrameLogic : AEntitySystem<FrameContext>
    {
        public FrameLogic() : base(Engine.World)
        {
        }
    }
}