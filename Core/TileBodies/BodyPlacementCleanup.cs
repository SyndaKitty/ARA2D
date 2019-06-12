using Core.Archetypes;
using Core.Buildings;
using DefaultEcs;
using DefaultEcs.System;

namespace Core.TileBodies
{
    public class BodyPlacementCleanup : AEntitySystem<FrameContext>
    {
        public BodyPlacementCleanup(Factory factory) : base(factory.BodyPlacementSet)
        {
        }

        protected override void Update(FrameContext state, in Entity entity)
        {
            entity.Dispose();
        }
    }
}