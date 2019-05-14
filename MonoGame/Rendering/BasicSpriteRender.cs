using System;
using Core;
using Core.Rendering;
using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Rendering
{
    public class BasicSpriteRender : AEntitySystem<RenderContext>
    {
        readonly SpriteBatch spriteBatch;

        public BasicSpriteRender(SpriteBatch spriteBatch) : base(Engine.World.GetEntities().With(typeof(Sprite), typeof(Transform)).Build())
        {
            this.spriteBatch = spriteBatch;
        }

        protected override void Update(RenderContext state, ReadOnlySpan<Entity> entities)
        {
            foreach (var entity in entities)
            {
                var transform = entity.Get<Transform>();
                var sprite = entity.Get<Sprite>();

                spriteBatch.Draw(sprite.Texture, transform.Position, Color.White);
            }
        }
    }
}
