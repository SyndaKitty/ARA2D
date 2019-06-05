using System;
using Core;
using Core.Rendering;
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
            var cameraEntity = Engine.World.GetEntities().With(typeof(Camera), typeof(Transform)).Build().GetEntities()[0];

            Matrix viewMatrix = cameraEntity.Get<Transform>().Matrix.Convert();
            Matrix worldMatrix;
            Vector2 position;
            foreach (var entity in entities)
            {
                GridTransform transform = entity.Get<GridTransform>();
                Sprite sprite = entity.Get<Sprite>();

                worldMatrix = new Matrix(
                    transform.Matrix.M11, transform.Matrix.M12, transform.Matrix.M13, transform.Matrix.M14,
                    transform.Matrix.M21, transform.Matrix.M22, transform.Matrix.M23, transform.Matrix.M24,
                    transform.Matrix.M31, transform.Matrix.M32, transform.Matrix.M33, transform.Matrix.M34,
                    transform.Matrix.M41, transform.Matrix.M42, transform.Matrix.M43, transform.Matrix.M44);

                position.X = (worldMatrix * viewMatrix).Translation.X;
                position.Y = (worldMatrix* viewMatrix).Translation.Y;
                spriteBatch.Draw(sprite.Texture, position, Color.White);
            }
        }
    }
}
