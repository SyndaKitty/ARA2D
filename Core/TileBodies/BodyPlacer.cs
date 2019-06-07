using Core.Archetypes;
using DefaultEcs.System;

namespace Core.TileBodies
{
    public class BodyPlacer : AEntitySystem<FrameContext>
    {
        public BodyPlacer(Factory factory) : base(factory.BodyPlacementSet)
        {

        }
    }
}