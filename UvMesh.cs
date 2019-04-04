using Microsoft.Xna.Framework.Graphics;
using Nez;
using Microsoft.Xna.Framework;

namespace ARA2D
{
    #region Old implementation

    //public class UvMesh : RenderableComponent
    //{
    //    VertexBuffer vBuffer;
    //    IndexBuffer iBuffer;

    //    VertexPositionColorTexture[] vertices;
    //    public VertexPositionColorTexture[] Vertices
    //    {
    //        get => vertices;
    //        set
    //        {
    //            vertices = value;
    //            vBuffer = new VertexBuffer(Core.graphicsDevice, typeof(VertexPositionColorTexture), Vertices.Length, BufferUsage.WriteOnly);
    //            vBuffer.SetData(Vertices);
    //        }
    //    }

    //    short[] indices;
    //    public short[] Indices
    //    {
    //        get => indices;
    //        set
    //        {
    //            indices = value;
    //            iBuffer = new IndexBuffer(Core.graphicsDevice, typeof(short), Indices.Length, BufferUsage.WriteOnly);
    //            iBuffer.SetData(Indices);
    //        }
    //    }

    //    public UvMesh()
    //    {
    //    }

    //    public override void render(Graphics graphics, Camera camera)
    //    {
    //        if (vBuffer == null || iBuffer == null) return;

    //        var g = graphics.batcher.graphicsDevice;
    //        g.Indices = iBuffer;
    //        g.SetVertexBuffer(vBuffer);

    //        RasterizerState rasterizerState = new RasterizerState();
    //        rasterizerState.CullMode = CullMode.CullClockwiseFace;
    //        g.RasterizerState = rasterizerState;

    //        var basicEffect = new BasicEffect(g);
    //        basicEffect.World = entity.

    //        foreach (EffectPass pass in basic)
    //    }
    //}

    #endregion

    /// <summary>
    /// basic class that can be used to create simple meshes. For more advanced usage subclass and override what is needed. The general gist
    /// of usage is the following:
    /// - call setVertPositions
    /// - call setTriangles to set the triangle indices
    /// - call recalculateBounds to prepare the Mesh for rendering and culling
    /// </summary>
    public class UvMesh : RenderableComponent
    {
        BasicEffect basicEffect;
        int primitiveCount;
        Vector2 topLeftVertPosition;

        float _width;
        float _height;
        public override float width => _width;
        public override float height => _height;

        Texture2D texture;
        short[] indices;
        VertexPositionColorTexture[] vertices;

        #region configuration

        /// <summary>
        /// recalculates the bounds
        /// </summary>
        public void RecalculateBounds()
        {
            topLeftVertPosition = new Vector2(float.MaxValue, float.MaxValue);
            var max = new Vector2(float.MinValue, float.MinValue);

            for (var i = 0; i < vertices.Length; i++)
            {
                topLeftVertPosition.X = MathHelper.Min(topLeftVertPosition.X, vertices[i].Position.X);
                topLeftVertPosition.Y = MathHelper.Min(topLeftVertPosition.Y, vertices[i].Position.Y);
                max.X = MathHelper.Max(max.X, vertices[i].Position.X);
                max.Y = MathHelper.Max(max.Y, vertices[i].Position.Y);
            }

            _width = max.X - topLeftVertPosition.X;
            _height = max.Y - topLeftVertPosition.Y;
            _areBoundsDirty = true;
        }

        /// <summary>
        /// sets the texture. Pass in null to unset the texture.
        /// </summary>
        /// <returns>The texture.</returns>
        /// <param name="texture">Texture.</param>
        public void SetTexture(Texture2D texture)
        {
            if (basicEffect != null)
            {
                basicEffect.Texture = texture;
                basicEffect.TextureEnabled = texture != null;
            }
            else
            {
                // store this away until the BasicEffect is created
                this.texture = texture;
            }
        }

        /// <summary>
        /// helper that sets the color for all verticess
        /// </summary>
        /// <param name="color">Color.</param>
        public void SetColorForAllVertices(Color color)
        {
            for (var i = 0; i < vertices.Length; i++)
                vertices[i].Color = color;
        }

        /// <summary>
        /// sets the vertex color for a single vertex
        /// </summary>
        /// <returns>The color for vertex.</returns>
        /// <param name="vertexIndex">Vertex index.</param>
        /// <param name="color">Color.</param>
        public void SetColorForVertex(int vertexIndex, Color color)
        {
            vertices[vertexIndex].Color = color;
        }

        /// <summary>
        /// sets the vertex positions. If the positions array does not match the vertex array size the vertex array will be recreated.
        /// </summary>
        /// <param name="positions">Positions.</param>
        public void SetVertexPositions(Vector2[] positions)
        {
            if (vertices == null || vertices.Length != positions.Length)
                vertices = new VertexPositionColorTexture[positions.Length];

            for (var i = 0; i < vertices.Length; i++)
                vertices[i].Position = positions[i].toVector3();
        }

        /// <summary>
        /// sets the vertex positions. If the positions array does not match the vertices array size the vertex array will be recreated.
        /// </summary>
        /// <param name="positions">Positions.</param>
        public void SetVertexPositions(Vector3[] positions)
        {
            if (vertices == null || vertices.Length != positions.Length)
                vertices = new VertexPositionColorTexture[positions.Length];

            for (var i = 0; i < vertices.Length; i++)
                vertices[i].Position = positions[i];
        }

        /// <summary>
        /// sets the triangle indices for rendering
        /// </summary>
        /// <param name="indices">Triangle indices.</param>
        public void SetIndices(short[] indices)
        {
            Insist.isTrue(indices.Length % 3 == 0, "triangles must be a multiple of 3");
            primitiveCount = indices.Length / 3;
            this.indices = indices;
        }

        #endregion


        #region Component/RenderableComponent overrides

        public override void onAddedToEntity()
        {
            basicEffect = entity.scene.content.loadMonoGameEffect<BasicEffect>();
            basicEffect.VertexColorEnabled = true;

            if (texture != null)
            {
                basicEffect.Texture = texture;
                basicEffect.TextureEnabled = true;
                texture = null;
            }
        }

        public override void onRemovedFromEntity()
        {
            entity.scene.content.unloadEffect(basicEffect);
            basicEffect = null;
        }

        public override void render(Graphics graphics, Camera camera)
        {
            if (vertices == null)
                return;

            basicEffect.Projection = camera.projectionMatrix;
            basicEffect.View = camera.transformMatrix;
            basicEffect.World = entity.transform.localToWorldTransform;
            basicEffect.CurrentTechnique.Passes[0].Apply();

            Core.graphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, 
                vertices, 0, vertices.Length, 
                indices, 0, primitiveCount);
        }
        #endregion
    }
}
