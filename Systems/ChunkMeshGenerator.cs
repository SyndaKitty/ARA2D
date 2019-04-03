using Nez;
using ARA2D.Components;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ARA2D.Systems
{
    public class ChunkMeshGenerator : EntityProcessingSystem
    {
        // Generate up to 4 chunk meshes per frame for now 
        // TODO: Have variable handling depending on current performance     
        public int HandlePerFrame = 4;

        public ChunkMeshGenerator() : base(new Matcher().all(typeof(ChunkGeneratedEvent)))
        {
        }

        public override void process(Entity entity)
        {
            var chunkGenEvent = entity.getComponent<ChunkGeneratedEvent>();
            var chunk = chunkGenEvent.Chunk;

            Vector2[] positions = CreatePositionsArray();
            int[] triangles = CreateTrianglesArray();
            Mesh m = CreateMesh(positions, triangles);

            Entity e = new Entity($"ChunkMesh{chunkGenEvent.Coords.Cx},{chunkGenEvent.Coords.Cy}");
            e.addComponent(m);
            Core.scene.addEntity(e);

            entity.destroy();
        }

        static Vector2[] CreatePositionsArray()
        {
            var positions = new Vector2[(Chunk.Size + 1) * (Chunk.Size + 1)];

            for (int i = 0, y = 0; y <= Chunk.Size; y++)
            {
                for (int x = 0; x <= Chunk.Size; x++, i++)
                {
                    positions[i] = new Vector2(x, y);
                }
            }

            return positions;
        }

        static int[] CreateTrianglesArray()
        {
            int[] triangles = new int[Chunk.Size * Chunk.Size * 6];
            for (int ti = 0, vi = 0, y = 0; y < Chunk.Size; y++, vi++)
            {
                for (int x = 0; x < Chunk.Size; x++, ti += 6, vi++)
                {
                    triangles[ti] = vi;
                    triangles[ti + 3] = triangles[ti + 1] = vi + 1;
                    triangles[ti + 5] = triangles[ti + 2] = vi + Chunk.Size + 1;
                    triangles[ti + 4] = vi + Chunk.Size + 2;
                }
            }
            return triangles;
        }

        Mesh CreateMesh(Vector2[] positions, int[] triangles)
        {
            Mesh m = new Mesh();
            m.setVertPositions(positions);
            m.setTriangles(triangles);
            m.recalculateBounds(false);
            m.setColorForAllVerts(Color.White);
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
