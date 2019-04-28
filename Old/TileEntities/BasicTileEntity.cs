using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using System;
using ARA2D.Chunks;

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
                CalculateScale();
            }
        }
        public int Height
        {
            get => height;
            set
            {
                height = Math.Max(Math.Min(value, TileChunk.Size), 1);
                CalculateScale();
            }
        }

        public Vector2 BaseScale { get; set; }
        public Vector2 Origin { get; set; }
        public Texture2D Texture { get; set; }
        public Entity Entity { get; set; }
        public Sprite Sprite { get; set; }

        public BasicTileEntity(Texture2D texture, int width, int height) : this(texture, width, height, Vector2.One, Vector2.Zero)
        { }

        public BasicTileEntity(Texture2D texture, int width, int height, Vector2 baseScale) : this(texture, width, height, baseScale, Vector2.Zero)
        { }

        public BasicTileEntity(Texture2D texture, int width, int height, Vector2 baseScale, Vector2 origin)
        {
            BaseScale = baseScale;
            Texture = texture;
            Origin = origin;
            Width = width;
            Height = height;
        }

        public void CreateEntity(Scene scene, long tx, long ty)
        {
            Entity = scene.createEntity($"TERenderable{ID}");
            CalculateScale();
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

        protected void CalculateScale()
        {
            if (Entity == null) return;
            Entity.scale = BaseScale * new Vector2(Width, Height);
        }

        public ITileEntity Clone()
        {
            return new BasicTileEntity(Texture, Width, Height, BaseScale, Origin); 
        }
    }
}
