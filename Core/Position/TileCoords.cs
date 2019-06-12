using System.Collections.Generic;
using Core.Tiles;

namespace Core.Position
{
    public struct TileCoords : IEqualityComparer<TileCoords>
    {
        public static TileCoords Create(long cx, long cy, int lx, int ly)
        {
            // Normalize coordinates (LocalX and LocalY must be from 0 to Chunk.Size-1)
            int dx = lx / Chunk.Size;
            if (lx < 0) dx--;
            cx += dx;
            lx -= dx * Chunk.Size;

            int dy = ly / Chunk.Size;
            if (ly < 0) dy--;
            cy += dy;
            ly -= dy * Chunk.Size;

            return new TileCoords
            {
                ChunkX = cx,
                ChunkY = cy,
                LocalX = lx,
                LocalY = ly
            };
        }

        // The coords of the chunk
        public long ChunkX;
        public long ChunkY;

        // The coords within the chunk
        public int LocalX;
        public int LocalY;
        
        #region Equality
        public bool Equals(TileCoords a, TileCoords b)
        {
            return a.ChunkX == b.ChunkX && a.ChunkY == b.ChunkY && a.LocalX == b.LocalX && a.LocalY == b.LocalY;
        }

        public int GetHashCode(TileCoords coords)
        {
            unchecked
            {
                var hashCode = coords.ChunkX.GetHashCode();
                hashCode = (hashCode * 397) ^ coords.ChunkY.GetHashCode();
                hashCode = (hashCode * 397) ^ coords.LocalX;
                hashCode = (hashCode * 397) ^ coords.LocalY;
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
                    var hashCode = obj.ChunkX.GetHashCode();
                    hashCode = (hashCode * 397) ^ obj.ChunkY.GetHashCode();
                    return hashCode;
                }
            }
        }
        public static IEqualityComparer<TileCoords> ChunkCoordsComparer { get; } = new TileCoordsChunkComparer();
        #endregion
    }
}
