using System;
using System.Numerics;
using Core.Archetypes;
using DefaultEcs;
using DefaultEcs.System;

namespace Core.Rendering
{
    public class ViewMatrixUpdate : AEntitySystem<FrameContext>
    {
        public ViewMatrixUpdate(Factory factory) : base(factory.CameraSet)
        {
        }

        protected override void Update(FrameContext state, ReadOnlySpan<Entity> entities)
        {
            foreach (var entity in entities)
            {
                var transform = entity.Get<Transform>();
                if (!transform.Dirty) continue;

                transform.Matrix = Matrix4x4.CreateTranslation(-transform.X, -transform.Y, 0) 
                    * Matrix4x4.CreateScale(transform.ScaleX, transform.ScaleY, 0);
            }
        }
    }
}
