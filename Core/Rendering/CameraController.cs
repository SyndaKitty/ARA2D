using System;
using System.Numerics;
using DefaultEcs;
using DefaultEcs.System;

namespace Core.Rendering
{
    public class CameraController : AEntitySystem<FrameContext>
    {
        float t;

        public CameraController() : base(Engine.World.GetEntities().With(typeof(Camera), typeof(Transform)).Build())
        {
        }

        protected override void Update(FrameContext state, ReadOnlySpan<Entity> entities)
        {
            var global = state.GlobalEntity;
            t += state.Dt;

            foreach (var entity in entities)
            {
                var transform = entity.Get<Transform>();
                float r = 16;
                transform.Position = new Vector2((float)Math.Cos(t) * r, (float)Math.Sin(t) * r);
                Console.WriteLine(transform.Position);
            }
        }
    }
}