using Core.Archetypes;
using Core.Buildings;
using DefaultEcs;
using DefaultEcs.System;

namespace Core.TileBodies
{
    public class BodyPlacementCleanup : AEntitySystem<FrameContext>
    {
        public BodyPlacementCleanup() : base(Engine.World.GetEntities().With<BodyPlacement>().Without<BuildingGhost>().Build())
        {
        }

        protected override void Update(FrameContext state, in Entity entity)
        {
            entity.Dispose();
        }
    }
}