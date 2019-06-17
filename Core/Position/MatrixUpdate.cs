using System;
using System.Numerics;
using Core.Archetypes;
using DefaultEcs;
using DefaultEcs.System;

namespace Core.Position
{
    public class MatrixUpdate : AEntitySystem<FrameContext>
    {
        public MatrixUpdate(Factory factory) : base(factory.PureTransformSet)
        {
        }

        protected override void Update(FrameContext state, ReadOnlySpan<Entity> entities)
        {
            foreach (var entity in entities)
            {
                var transform = entity.Get<Transform>();
                if (!transform.Dirty) continue;

                // Recalculate matrix
                transform.Matrix = Matrix4x4.CreateTranslation(transform.X, transform.Y, 0);
            }
        }
    }
}
