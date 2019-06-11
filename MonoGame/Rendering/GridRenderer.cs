using System;
using Core;
using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Rendering
{
    public class GridRenderer : AEntitySystem<FrameContext>
    {
        readonly SpriteBatch spriteBatch;

        public GridRenderer(SpriteBatch spriteBatch) : base(Engine.World.GetEntities().With(typeof(GridTransform), typeof(Sprite)).Without<Transform>().Build())
        {
            this.spriteBatch = spriteBatch;
        }

        protected override void Update(FrameContext state, ReadOnlySpan<Entity> entities)
        {
            // TODO: Handle multiple cameras
            var cameraEntity = state.Factory.CameraSet.GetEntities()[0];

            Matrix viewMatrix = cameraEntity.Get<Transform>().Matrix.Convert();
            Matrix worldMatrix;
            Vector2 position;
            foreach (var entity in entities)
            {
                GridTransform transform = entity.Get<GridTransform>();
                Sprite sprite = entity.Get<Sprite>();

                worldMatrix = transform.Matrix.Convert();

                position.X = (worldMatrix * viewMatrix).Translation.X;
                position.Y = (worldMatrix* viewMatrix).Translation.Y;
                spriteBatch.Draw(sprite.Texture, position, Color.White);
            }
        }
    }
}
