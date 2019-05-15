using System;
using Core;
using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Rendering
{
    public class BasicSpriteRenderer : AEntitySystem<RenderContext>
    {
        readonly SpriteBatch spriteBatch;

        public BasicSpriteRenderer(SpriteBatch spriteBatch) : base(Engine.World.GetEntities().With(typeof(Sprite), typeof(Transform)).Build())
        {
            this.spriteBatch = spriteBatch;
        }

        protected override void Update(RenderContext state, ReadOnlySpan<Entity> entities)
        {
            Vector2 position;
            foreach (var entity in entities)
            {
                var transform = entity.Get<Transform>();
                var sprite = entity.Get<Sprite>();
                position.X = transform.X;
                position.Y = transform.Y;
                spriteBatch.Draw(sprite.Texture, position, Color.White);
            }
        }
    }
}
