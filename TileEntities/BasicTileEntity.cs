using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using System;

namespace ARA2D.TileEntities
{
    public class BasicTileEntity : ITileEntity
    {
        protected int width;
        protected int height;

        public int ID { get; set; }
        public int Width
        {
            get => width;
            set
            {
                width = Math.Max(Math.Min(value, TileChunk.Size), 1);
            }
        }
        public int Height
        {
            get => height;
            set
            {
                height = Math.Max(Math.Min(value, TileChunk.Size), 1);
            }
        }

        public Vector2 Scale { get; protected set; }
        public Vector2 Origin { get; protected set; }
        public Texture2D Texture { get; protected set; }
        public Entity Entity { get; protected set; }
        public Sprite Sprite { get; protected set; }

        public BasicTileEntity(Texture2D texture, int width, int height) : this(texture, width, height, Vector2.One, Vector2.Zero)
        { }

        public BasicTileEntity(Texture2D texture, int width, int height, Vector2 scale) : this(texture, width, height, scale, Vector2.Zero)
        { }

        public BasicTileEntity(Texture2D texture, int width, int height, Vector2 scale, Vector2 origin)
        {
            Scale = scale;
            Texture = texture;
            Origin = origin;
            Width = width;
            Height = height;
        }

        public void CreateEntity(Scene scene, long tx, long ty)
        {
            Entity = scene.createEntity($"TIRenderable{ID}");
            Entity.scale = Scale * new Vector2(Width, Height);
            Entity.position = TileCoords.ToWorldSpace(tx, ty);
            Sprite = new Sprite(Texture) { origin = Origin };
            Entity.addComponent(Sprite);
        }
        
        public void DeleteEntity()
        {
            Entity.destroy();
            Entity = null;
        }

        public virtual void Update()
        {
        }
    }
}
