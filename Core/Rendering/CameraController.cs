using System;
using Core.Archetypes;
using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework.Input;

namespace Core.Rendering
{
    public class CameraController : AEntitySystem<FrameContext>
    {
        const float Speed = 20;
        public CameraController(Factory factory) : base(factory.CameraSet)
        {
        }

        protected override void Update(FrameContext state, ReadOnlySpan<Entity> entities)
        {
            var global = state.GlobalEntity;

            float dx = 0;
            float dy = 0;

            dx += state.Input.KeyboardState.IsKeyDown(Keys.A) ? -Speed : 0;
            dx += state.Input.KeyboardState.IsKeyDown(Keys.D) ? Speed : 0;
            dy += state.Input.KeyboardState.IsKeyDown(Keys.W) ? -Speed : 0;
            dy += state.Input.KeyboardState.IsKeyDown(Keys.S) ? Speed : 0;

            foreach (var entity in entities)
            {
                var transform = entity.Get<Transform>();
                transform.X += dx * state.Dt;
                transform.Y += dy * state.Dt;
            }
        }
    }
}