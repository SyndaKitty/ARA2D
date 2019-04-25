using ARA2D.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;

namespace ARA2D.TileEntities.Components
{
    public class TileEntityTemplate : Component
    {
        public TileEntityTemplate(Texture2D texture, IntVector2 size, Vector2 scale)
        {
            Texture = texture;
            Size = size;
            Scale = scale;
        }

        public Texture2D Texture;
        public IntVector2 Size;
        public Vector2 Scale;
    }
}
