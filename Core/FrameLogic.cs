using Core.Position;
using Core.Rendering;
using DefaultEcs.System;

namespace Core
{
    public class FrameLogic : AEntitySystem<FrameContext>
    {
        readonly ISystem<FrameContext> wrappedSystems;

        public FrameLogic() : base(Engine.World)
        {
            wrappedSystems = new SequentialSystem<FrameContext>(
                new GridMatrixUpdate(),
                new ViewMatrixUpdate(),
                new CameraController()
            );
        }

        protected override void PreUpdate(FrameContext state)
        {
            wrappedSystems.Update(state);
        }
    }
}