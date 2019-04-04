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
            var indices = CreateIndicesArray();
            var mesh = CreateMesh(vertices, indices);

            Entity e = new Entity($"ChunkMesh{chunkGenEvent.Coords.Cx},{chunkGenEvent.Coords.Cy}");
            e.addComponent(mesh);
            Core.scene.addEntity(e);

            entity.destroy();
        }

        static VertexPositionColorTexture[] CreateVertexArray()
        {
            var vertices = new VertexPositionColorTexture[Chunk.Size * Chunk.Size * 4];

            int vi = 0;
            for (int y = 0; y < Chunk.Size; y++)
            {
                for (int x = 0; x < Chunk.Size; x++)
                {
                    int blockIndex = Random.nextInt(16);
                    for (int corner = 0; corner < 4; corner++, vi++)
                    {
                        int cx = corner % 2;
                        int cy = corner / 2;
                        vertices[vi].Position = new Vector3((x + cx) * Tile.Size, (y+cy) * Tile.Size, 0 );
                        vertices[vi].TextureCoordinate = GetUVCoordsFromIndex(blockIndex, corner);
                        vertices[vi].Color = Color.White;
                    }
                }
            }

            return vertices;
        }

        /// <summary>
        /// Get texture coordinates for the block index given
        /// </summary>
        /// <param name="blockIndex"></param>
        /// <param name="corner">The corner of the block wanted
        /// 0 = Top-left
        /// 1 = Top-right
        /// 2 = Bottom-left
        /// 3 = Bottom-right
        /// </param>
        /// <returns></returns>
        static Vector2 GetUVCoordsFromIndex(int blockIndex, int corner)
        {
            // TODO: pull texture info from JSON
            int width = 4;
            int height = 4;

            int x = blockIndex % width + corner % 2;
            int y = blockIndex / width + corner / 2;

            return new Vector2((float) x / width, (float) y / height);
        }

        static short[] CreateIndicesArray()
        {
            short[] indices = new short[Chunk.Size * Chunk.Size * 6];
            int i = 0;
            int vi = 0;
            for (int y = 0; y < Chunk.Size; y++)
            {
                for (int x = 0; x < Chunk.Size; x++, vi += 4)
                {
                    indices[i++] = (short) vi;
                    indices[i++] = (short) (vi + 1);
                    indices[i++] = (short) (vi + 3);
                    indices[i++] = (short) vi;
                    indices[i++] = (short) (vi + 3);
                    indices[i++] = (short) (vi + 2);
                }
            }
            return indices;
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
