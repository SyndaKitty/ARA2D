using Core.Position;
using DefaultEcs.System;

namespace Core
{
    public class FrameLogic : AEntitySystem<FrameContext>
    {
        readonly ISystem<FrameContext> wrappedSystems;

        public FrameLogic() : base(Engine.World)
        {
            wrappedSystems = new SequentialSystem<FrameContext>(
                new GridMatrixUpdate()
            );
        }

        protected override void PreUpdate(FrameContext state)
        {
            wrappedSystems.Update(state);
        }
    }
}