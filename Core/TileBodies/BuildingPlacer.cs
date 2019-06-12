using Core.Archetypes;
using Core.Buildings;
using DefaultEcs;
using DefaultEcs.System;

namespace Core.TileBodies
{
    public class BuildingPlacer : AEntitySystem<FrameContext>
    {
        public BuildingPlacer(Factory factory) : base(factory.BuildingPlacementSet)
        {
        }

        protected override void Update(FrameContext state, in Entity entity)
        {
            var placement = entity.Get<BodyPlacement>();
            if (!placement.Success || placement.Type == PlacementType.Check) return;
            state.Factory.CreateBuilding(placement, entity.Get<Building>());
        }
    }
}
