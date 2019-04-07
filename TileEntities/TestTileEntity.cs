using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace ARA2D.TileEntities
{
    public class TestTileEntity : TileEntity
    {
        public static Texture2D texture;

        public int ID { get; set; }
        public List<ChunkCoords> ContainingChunkCoords { get; set; }

        public RenderableComponent GenerateRenderable()
        {
            if (texture == null)
            {
                texture = Core.content.Load<Texture2D>("images/TestEntity");
            }

            return new Sprite(texture);
        }

        public Tuple<int, int> GetBounds()
        {
            return new Tuple<int, int>(2, 2);
        }

        public bool CanSleep()
        {
            return false;
        }

        public void Update()
        {
        }
    }
}
