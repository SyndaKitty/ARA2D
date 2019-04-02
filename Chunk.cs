using Microsoft.Xna.Framework;
using Nez;

namespace ARA2D
{
    public class Chunk
    {
        public const int Size = 32;
        public readonly ChunkCoords Coords;
        public Tile[,] tiles;

        public Chunk(ChunkCoords coords)
        {
            Coords = coords;
        }

        public Mesh GenerateMesh()
        {
            // Things we can look at for examples of how to do this
            //new Model();
            //new QuadMesh();
            //new Mesh();
            
            Vector2[] positions = new Vector2[(Size + 1) * (Size + 1)];

            for (int i = 0, y = 0; y <= Size; y++)
            {
                for (int x = 0; x <= Size; x++, i++)
                {
                    positions[i] = new Vector2(x, y);
                }
            }

            int[] triangles = new int[Size * Size * 6];
            for (int ti = 0, vi = 0, y = 0; y < Size; y++, vi++)
            {
                for (int x = 0; x < Size; x++, ti += 6, vi++)
                {
                    triangles[ti] = vi;
                    triangles[ti + 3] = triangles[ti + 1] = vi + 1;
                    triangles[ti + 5] = triangles[ti + 2] = vi + Size + 1;
                    triangles[ti + 4] = vi + Size + 2;
                }
            }

            Mesh m = new Mesh();
            m.setVertPositions(positions);
            m.setTriangles(triangles);
            m.recalculateBounds(false);
            m.setColorForAllVerts(Color.White);

            return m;
        }
    }
}
