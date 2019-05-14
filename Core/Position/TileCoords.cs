namespace Core.Position
{
    public class TileCoords
    {
        const int ChunkPower = 5;
        public const int ChunkSize = 1 << ChunkPower;

        // The coords of the chunk
        long chunkX;
        long chunkY;

        // The coords within the chunk
        int localX;
        int localY;

        public bool Dirty;

        public long ChunkX
        {
            get => chunkX;
            set
            {
                chunkX = value;
                Dirty = true;
            }
        }

        public long ChunkY
        {
            get => chunkY;
            set
            {
                chunkY = value;
                Dirty = true;
            }
        }

        public int LocalX
        {
            get => localX;
            set
            {
                localX = value;
                NormalizeLocalX();
                Dirty = true;
            }
        }

        public int LocalY
        {
            get => localY;
            set
            {
                localY = value;
                NormalizeLocalY();
                Dirty = true;
            }
        }

        public TileCoords(long chunkX, int localX, long ChunkY, int localY)
        {
            ChunkX = chunkX;
            LocalX = localX;

            ChunkY = chunkY;
            LocalY = localY;
        }

        void NormalizeLocalX()
        {
            while (LocalX < 0)
            {
                LocalX += ChunkSize;
                ChunkX--;
            }

            while (LocalX > ChunkSize)
            {
                LocalX -= ChunkSize;
                ChunkX++;
            }
        }

        void NormalizeLocalY()
        {
            while (LocalY < 0)
            {
                LocalY += ChunkSize;
                ChunkY--;
            }

            while (LocalY > ChunkSize)
            {
                LocalY -= ChunkSize;
                ChunkY++;
            }
        }
    }
}
