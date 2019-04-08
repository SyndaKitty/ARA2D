using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ARA2D.TileEntities
{
    public class TestTileEntity : BasicTileEntity
    {
        public static Texture2D texture;

        public TestTileEntity(Texture2D texture) : base(texture, 1, 1, new Vector2(.5f, .5f))
        { }

        public void RecalculateScale()
        {
            Entity.scale = Scale * new Vector2(Width, Height);
        }

        public ITileEntity Clone()
        {
            return new TestTileEntity(Texture) {Width = Width, Height = Height};
        }
    }
}
