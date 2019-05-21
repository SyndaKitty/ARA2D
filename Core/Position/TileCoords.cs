using System.Collections.Generic;
using Core.Tiles;

namespace Core.Position
{
    public class TileCoords : IEqualityComparer<TileCoords>
    {
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

        public TileCoords(long chunkX, int localX, long chunkY, int localY)
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
                LocalX += Chunk.Size;
                ChunkX--;
            }

            while (LocalX > Chunk.Size)
            {
                LocalX -= Chunk.Size;
                ChunkX++;
            }
        }

        void NormalizeLocalY()
        {
            while (LocalY < 0)
            {
                LocalY += Chunk.Size;
                ChunkY--;
            }

            while (LocalY > Chunk.Size)
            {
                LocalY -= Chunk.Size;
                ChunkY++;
            }
        }

        #region Equality
        public bool Equals(TileCoords a, TileCoords b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (ReferenceEquals(a, null))
            {
                return false;
            }

            if (ReferenceEquals(b, null))
            {
                return false;
            }

            return a.chunkX == b.chunkX && a.chunkY == b.chunkY && a.localX == b.localX && a.localY == b.localY;
        }

        public int GetHashCode(TileCoords coords)
        {
            unchecked
            {
                var hashCode = coords.chunkX.GetHashCode();
                hashCode = (hashCode * 397) ^ coords.chunkY.GetHashCode();
                hashCode = (hashCode * 397) ^ coords.localX;
                hashCode = (hashCode * 397) ^ coords.localY;
                return hashCode;
            }
        }

        sealed class TileCoordsChunkComparer : IEqualityComparer<TileCoords>
        {
            public bool Equals(TileCoords a, TileCoords b)
            {
                return a.ChunkX == b.ChunkX && a.ChunkY == b.ChunkY;
            }

            public int GetHashCode(TileCoords obj)
            {
                unchecked
                {
                    var hashCode = obj.chunkX.GetHashCode();
                    hashCode = (hashCode * 397) ^ obj.chunkY.GetHashCode();
                    return hashCode;
                }
            }
        }
        public static IEqualityComparer<TileCoords> ChunkCoordsComparer { get; } = new TileCoordsChunkComparer();
        #endregion
    }
}
