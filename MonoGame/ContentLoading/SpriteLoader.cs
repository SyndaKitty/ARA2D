using Core;
using Core.Rendering;
using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.ContentLoading
{
    public class SpriteLoader : AEntitySystem<RenderContext>
    {
        readonly ContentManager content;

        public SpriteLoader(ContentManager content) : base(Engine.World.GetEntities().With<SpriteLoad>().Build())
        {
            this.content = content;
        }

        protected override void Update(RenderContext state, in Entity entity)
        {
            var load = entity.Get<SpriteLoad>();
            entity.Remove<SpriteLoad>();

            var sprite = new Sprite(content.Load<Texture2D>(load.AssetName));

            entity.Set(sprite);
        }
    }
}
