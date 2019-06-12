using Core.Archetypes;
using DefaultEcs;
using DefaultEcs.System;

namespace Core.TileBodies
{
    public class BuildingGhostColor : AEntitySystem<FrameContext>
    {
        public BuildingGhostColor(Factory factory) : base(factory.BuildingGhostSet)
        {
        }

        protected override void Update(FrameContext state, in Entity entity)
        {
            var placement = entity.Get<BodyPlacement>();
            var ghost = entity.Get<BuildingGhost>();
            ghost.Color = placement.Success ? BuildingGhost.Valid : BuildingGhost.Invalid;
        }
    }
}
