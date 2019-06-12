using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Rendering
{
    public class Sprite
    {
        public Texture2D Texture;
        public Color Color;

        public Sprite(Texture2D texture)
        {
            Texture = texture;
        }
    }
}
