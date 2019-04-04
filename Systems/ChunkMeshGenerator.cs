using Nez;
using ARA2D.Components;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ARA2D.Systems
{
    public class ChunkMeshGenerator : EntityProcessingSystem
    {
        // Generate up to 4 chunk meshes per frame for now 
        // TODO: Have variable handling depending on current performance     
        public int HandlePerFrame = 4;

        Texture2D chunkTextures;

        public ChunkMeshGenerator(Texture2D chunkTextures) : base(new Matcher().all(typeof(ChunkGeneratedEvent)))
        {
            this.chunkTextures = chunkTextures;
        }

        public override void process(Entity entity)
        {
            var chunkGenEvent = entity.getComponent<ChunkGeneratedEvent>();
            var chunk = chunkGenEvent.Chunk;

            var vertices = CreateVertexArray();
            var indices = CreateTrianglesArray();
            var mesh = CreateMesh(vertices, indices);

            Entity e = new Entity($"ChunkMesh{chunkGenEvent.Coords.Cx},{chunkGenEvent.Coords.Cy}");
            e.addComponent(mesh);
            Core.scene.addEntity(e);

            entity.destroy();
        }

        static VertexPositionColorTexture[] CreateVertexArray()
        {
            var vertices = new VertexPositionColorTexture[(Chunk.Size + 1) * (Chunk.Size + 1)];

            for (int i = 0, y = 0; y <= Chunk.Size; y++)
            {
                for (int x = 0; x <= Chunk.Size; x++, i++)
                {
                    vertices[i].Position = new Vector3(x * Tile.Size, y * Tile.Size, 0 );
                    vertices[i].TextureCoordinate = new Vector2((float)x / Chunk.Size, (float)y / Chunk.Size);
                    vertices[i].Color = Color.White;
                }
            }

            return vertices;
        }

        static short[] CreateTrianglesArray()
        {
            short[] triangles = new short[Chunk.Size * Chunk.Size * 6];
            for (int ti = 0, vi = 0, y = 0; y < Chunk.Size; y++, vi++)
            {
                for (int x = 0; x < Chunk.Size; x++, ti += 6, vi++)
                {
                    triangles[ti] = (short)vi;
                    triangles[ti + 3] = triangles[ti + 1] = (short)(vi + 1);
                    triangles[ti + 5] = triangles[ti + 2] = (short)(vi + Chunk.Size + 1);
                    triangles[ti + 4] = (short)(vi + Chunk.Size + 2);
                }
            }
            return triangles;
        }

        RenderableComponent CreateMesh(VertexPositionColorTexture[] vertices, short[] indices)
        {
            UvMesh m = new UvMesh();
            m.SetVertices(vertices);
            m.SetIndices(indices);
            m.RecalculateBounds();
            m.SetTexture(chunkTextures);
            m.debugRenderEnabled = true;
            return m;
        }

        protected override void process(List<Entity> entities)
        {
            for (int i = 0; i < HandlePerFrame && i < entities.Count; i++)
            {
                process(entities[i]);
            }
        }
    }
}
