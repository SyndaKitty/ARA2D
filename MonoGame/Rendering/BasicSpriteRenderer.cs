using System;
using Core;
using Core.Rendering;
using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Rendering
{
    public class BasicSpriteRenderer : AEntitySystem<FrameContext>
    {
        readonly SpriteBatch spriteBatch;

        public BasicSpriteRenderer(SpriteBatch spriteBatch) : base(Engine.World.GetEntities().With(typeof(Sprite), typeof(Transform)).Build())
        {
            this.spriteBatch = spriteBatch;
        }

        protected override void Update(FrameContext state, ReadOnlySpan<Entity> entities)
        {
            // TODO: Handle multiple cameras
            var cameraEntity = state.Factory.CameraSet.GetEntities()[0];
            var cameraTransform = cameraEntity.Get<Transform>();

            Vector2 position;
            foreach (var entity in entities)
            {
                var transform = entity.Get<Transform>();
                var sprite = entity.Get<Sprite>();
                position.X = (transform.X - cameraTransform.Position.X) * cameraTransform.ScaleX;
                position.Y = (transform.Y - cameraTransform.Position.Y) * cameraTransform.ScaleY;
                spriteBatch.Draw(sprite.Texture, position, Color.White);
            }
        }
    }
}
