using Core;
using DefaultEcs.System;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Rendering
{
    public class RenderBegin : ISystem<RenderContext>
    {
        readonly SpriteBatch spriteBatch;

        public RenderBegin(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
        }

        public void Dispose()
        {
        }

        public void Update(RenderContext state)
        {
            spriteBatch.Begin();
        }

        public bool IsEnabled { get; set; }
    }
}
