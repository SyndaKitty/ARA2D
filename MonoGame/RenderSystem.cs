using System;
using Core;
using Core.Rendering;
using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame
{
    public class RenderSystem : AEntitySystem<RenderContext>
    {
        SpriteBatch spriteBatch;

        public RenderSystem(SpriteBatch spriteBatch) : base(Engine.World.GetEntities().With<GridTransform>().With<Sprite>().Build())
        {
            this.spriteBatch = spriteBatch;
        }

        protected override void PreUpdate(RenderContext state)
        {
            spriteBatch.Begin();
        }

        protected override void Update(RenderContext state, ReadOnlySpan<Entity> entities)
        {
            foreach (var entity in entities)
            {
                GridTransform grid = entity.Get<GridTransform>();
                Sprite sprite = entity.Get<Sprite>();

                // TODO: Manage scale/transformation with camera matrix
                // Scale up rectangle
                Rectangle rect = new Rectangle(grid.X * 16, grid.Y * 16, grid.Width * 16, grid.Height * 16);

                spriteBatch.Draw(sprite.Texture, rect, Color.White);
            }
        }

        protected override void PostUpdate(RenderContext state)
        {
            spriteBatch.End();
        }
    }
}
