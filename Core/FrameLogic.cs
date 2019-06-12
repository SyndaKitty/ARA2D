using Core.Archetypes;
using Core.Input;
using Core.Position;
using Core.Rendering;
using Core.Showcase;
using Core.TileBodies;
using DefaultEcs.System;

namespace Core
{
    public class FrameLogic : AEntitySystem<FrameContext>
    {
        readonly ISystem<FrameContext> wrappedSystems;

        public FrameLogic(Factory factory) : base(Engine.World)
        {
            wrappedSystems = new SequentialSystem<FrameContext>(
                //new TileRandomizer(),
                new CameraController(factory),
                new ViewMatrixUpdate(factory),
                new InputBuildingPlacer(),
                new BodyPlacer(factory),
                new BuildingPlacer(factory),
                new BuildingGhostColor(factory),
                new BodyPlacementCleanup(),
                new GridMatrixUpdate()
            );
        }

        protected override void PreUpdate(FrameContext state)
        {
            wrappedSystems.Update(state);
        }
    }
}