using System;
using Core;
using Core.Rendering;
using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Rendering
{
    public class GridRenderer : AEntitySystem<RenderContext>
    {
        readonly SpriteBatch spriteBatch;

        public GridRenderer(SpriteBatch spriteBatch) : base(Engine.World.GetEntities().With(typeof(GridTransform), typeof(Sprite)).Without<Transform>().Build())
        {
            this.spriteBatch = spriteBatch;
        }

        protected override void Update(RenderContext state, ReadOnlySpan<Entity> entities)
        {
            Vector2 position;
            foreach (var entity in entities)
            {
                GridTransform transform = entity.Get<GridTransform>();
                Sprite sprite = entity.Get<Sprite>();

                position.X = transform.Matrix.Translation.X;
                position.Y = transform.Matrix.Translation.Y;
                spriteBatch.Draw(sprite.Texture, position, Color.White);
            }
        }
    }
}
