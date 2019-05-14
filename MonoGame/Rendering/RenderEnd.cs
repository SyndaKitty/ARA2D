using Core;
using DefaultEcs.System;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Rendering
{
    public class RenderEnd : ISystem<RenderContext>
    {
        readonly SpriteBatch spriteBatch;

        public RenderEnd(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
        }

        public void Dispose()
        {
        }

        public void Update(RenderContext state)
        {
            spriteBatch.End();
        }

        public bool IsEnabled { get; set; }
    }
}
