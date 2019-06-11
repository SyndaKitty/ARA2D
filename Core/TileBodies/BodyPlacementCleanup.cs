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
            var placement = entity.Get<BodyPlacement>();
            if (entity.Has<Building>())
            {
                var building = entity.Get<Building>();
            }
            entity.Dispose();
        }
    }
}
