using System;
using Core;
using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame
{
    public class RenderSystem : AEntitySystem<TimeInfo>
    {
        SpriteBatch spriteBatch;

        // TODO: Reference sprite component to draw
        Texture2D texture;

        public RenderSystem(SpriteBatch spriteBatch, Texture2D defaultSprite) : base(Engine.World.GetEntities().With<GridTransform>().Build())
        {
            this.spriteBatch = spriteBatch;
            texture = defaultSprite;
        }

        protected override void PreUpdate(TimeInfo state)
        {
         
            spriteBatch.Begin();
        }

        protected override void Update(TimeInfo state, ReadOnlySpan<Entity> entities)
        {
            foreach (var entity in entities)
            {
                GridTransform grid = entity.Get<GridTransform>();
                Rectangle rect = new Rectangle(grid.X * 16, grid.Y * 16, grid.Width * 16, grid.Height * 16);

                spriteBatch.Draw(texture, rect, Color.White);
            }
        }

        protected override void PostUpdate(TimeInfo state)
        {
            spriteBatch.End();
        }
    }
}
