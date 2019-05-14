using Microsoft.Xna.Framework;

namespace Core
{
    public class GridTransform
    {
        // The coords of the chunk
        public long ChunkX { get; private set; }
        public long ChunkY { get; private set; }

        // The coords within the chunk
        public int LocalX { get; private set; }
        public int LocalY { get; private set; }

        public int Width { get; }
        public int Height { get; }

        public bool Dirty;
        public Matrix Matrix;

        public GridTransform(long chunkX, int localX, long chunkY, int localY, int width, int height)
        {
            ChunkX = chunkX;
            LocalX = localX;

            ChunkY = chunkY;
            LocalY = localY;

            Width = width;
            Height = height;
        }

        public void SetX(long chunkX, int localX)
        {
            ChunkX = chunkX;
            LocalX = localX;
            Dirty = true;
        }

        public void SetY(long chunkY, int localY)
        {
            ChunkY = chunkY;
            LocalY = localY;
            Dirty = true;
        }

        public void Set(long chunkX, int localX, long chunkY, int localY)
        {
            SetX(chunkX, localX);
            SetY(chunkY, localY);
        }
    }
}
