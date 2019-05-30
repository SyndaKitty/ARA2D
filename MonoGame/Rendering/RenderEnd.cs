using Core;
using DefaultEcs.System;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Rendering
{
    public class RenderEnd : ISystem<FrameContext>
    {
        readonly SpriteBatch spriteBatch;

        public RenderEnd(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
        }

        public void Dispose()
        {
        }

        public void Update(FrameContext state)
        {
            spriteBatch.End();
        }

        public bool IsEnabled { get; set; }
    }
}
