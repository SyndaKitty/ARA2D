using System;
using System.Numerics;
using DefaultEcs;
using DefaultEcs.System;

namespace Core.Rendering
{
    public class ViewMatrixUpdate : AEntitySystem<FrameContext>
    {
        public ViewMatrixUpdate() : base(Engine.World.GetEntities().With(typeof(Camera), typeof(Transform)).Build())
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
