using System;
using Core;
using Core.Tiles;
using DefaultEcs;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Rendering
{
    public class ChunkMeshGenerator : AEntitySystem<RenderContext>
    {
        public const int TileTextureMapWidth = 4;
        public const int TileTextureMapHeight = 4;

        public static Vector2 TileTextureMapInverse = new Vector2(1f / TileTextureMapWidth, 1f / TileTextureMapHeight);

        public ChunkMeshGenerator() : base(Engine.World.GetEntities().With(typeof(ChunkMesh), typeof(Chunk)).Build())
        {
        }

        protected override void Update(RenderContext state, ReadOnlySpan<Entity> entities)
        {
            ChunkMesh mesh;
            Chunk chunk;
            foreach (var entity in entities)
            {
                chunk = entity.Get<Chunk>();
                if (!chunk.TilesChanged) continue;

                mesh = entity.Get<ChunkMesh>();
                int numberOfTiles = Chunk.Size * Chunk.Size;

                // Calculate vertices
                VertexPositionColorTexture[] vertices = new VertexPositionColorTexture[numberOfTiles * 4];

                int i = 0;
                int tileIndex;
                Vector2 textureCoords;
                for (int y = 0; y < Chunk.Size; y++)
                {
                    for (int x = 0; x < Chunk.Size; x++)
                    {
                        tileIndex = chunk.Tiles[y * Chunk.Size + x];
                        textureCoords.X = (tileIndex % TileTextureMapWidth);
                        textureCoords.Y = (tileIndex / TileTextureMapWidth);
                        textureCoords *= TileTextureMapInverse;

                        vertices[i++] = new VertexPositionColorTexture(new Vector3(x + 0, y + 0, 0), Color.White, textureCoords + new Vector2(0, 0) * TileTextureMapWidth);
                        vertices[i++] = new VertexPositionColorTexture(new Vector3(x + 1, y + 0, 0), Color.White, textureCoords + new Vector2(1, 0) * TileTextureMapWidth);
                        vertices[i++] = new VertexPositionColorTexture(new Vector3(x + 0, y + 1, 0), Color.White, textureCoords + new Vector2(0, 1) * TileTextureMapWidth);
                        vertices[i++] = new VertexPositionColorTexture(new Vector3(x + 1, y + 1, 0), Color.White, textureCoords + new Vector2(1, 1) * TileTextureMapWidth);
                    }
                }

                // Calculate indices
                short[] indices = new short[numberOfTiles * 6];
                short offset = 0;

                for (i = 0; i < indices.Length;)
                {
                    indices[i++] = offset;
                    indices[i++] = (short)(offset + 1);
                    indices[i++] = (short)(offset + 3);

                    indices[i++] = offset;
                    indices[i++] = (short)(offset + 3);
                    indices[i++] = (short)(offset + 2);

                    offset += 4;
                }

                // Assign calculated arrays
                mesh.Vertices = vertices;
                mesh.Indices = indices;
            }
        }
    }
}
