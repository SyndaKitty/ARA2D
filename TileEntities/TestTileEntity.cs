using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using Nez.UI;

namespace ARA2D.TileEntities
{
    public class TestTileEntity : TileEntity
    {
        public static Texture2D texture;

        public int ID { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public TestTileEntity()
        {
            Width = Height = 2;
        }

        public RenderableComponent GenerateRenderable()
        {
            if (texture == null)
            {
                texture = Core.content.Load<Texture2D>("images/TestEntity2");
            }

            var sprite = new Sprite(texture) {origin = new Vector2(0, 0)};
            
            return sprite;
        }

        public Tuple<int, int> GetBounds()
        {
            return new Tuple<int, int>(Width, Height);
        }

        public Vector2 DefaultScale()
        {
            return new Vector2(.5f, .5f);
        }

        public bool CanSleep()
        {
            return false;
        }

        public void Update()
        {
        }

        public TileEntity Clone()
        {
            return new TestTileEntity {Width = Width, Height = Height};
        }
    }
}
