using System;
using Core;
using Core.Rendering;
using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Rendering
{
    public class ChunkMeshRenderer : AEntitySystem<FrameContext>
    {
        readonly GraphicsDevice graphicsDevice;
        readonly Texture2D tileMapTexture;

        public ChunkMeshRenderer(GraphicsDevice graphicsDevice, Texture2D tileMapTexture) : base(Engine.World.GetEntities().With(typeof(GridTransform), typeof(ChunkMesh)).Build())
        {
            this.graphicsDevice = graphicsDevice;
            this.tileMapTexture = tileMapTexture;
        }

        protected override void Update(FrameContext state, ReadOnlySpan<Entity> entities)
        {
            // TODO: Handle multiple cameras - probably with tag component
            var cameraEntity = state.Factory.CameraSet.GetEntities()[0];
            var cameraTransform = cameraEntity.Get<Transform>();

            GridTransform transform;
            ChunkMesh mesh;
            BasicEffect effect = new BasicEffect(graphicsDevice);
            effect.VertexColorEnabled = true;
            effect.TextureEnabled = true;
            effect.Texture = tileMapTexture;
            effect.Projection = Matrix.CreateOrthographicOffCenter(0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height, 0, 0, 1);
            effect.View = cameraTransform.Matrix.Convert();

            foreach (var entity in entities)
            {
                transform = entity.Get<GridTransform>();
                mesh = entity.Get<ChunkMesh>();
                effect.World = transform.Matrix.Convert();
                effect.CurrentTechnique.Passes[0].Apply();

                graphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, mesh.Vertices, 0, mesh.Vertices.Length, mesh.Indices, 0, mesh.Indices.Length / 3);
            }
        }
    }
}
