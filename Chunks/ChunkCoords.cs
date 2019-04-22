using Microsoft.Xna.Framework;
using System;

namespace ARA2D.Chunks
{
    public class ChunkCoords : IEquatable<ChunkCoords>
    {
        public long Cx;
        public long Cy;

        public ChunkCoords(long cx, long cy)
        {
            Cx = cx;
            Cy = cy;
        }

        public static ChunkCoords FromBlockCoords(long x, long y)
        {
            return new ChunkCoords(x >> TileChunk.Bits, y >> TileChunk.Bits);
        }

        public static ChunkCoords FromWorldSpace(float x, float y)
        {
            long scaledX = (long)(x / Tile.Size);
            long scaledY = (long)(y / Tile.Size);
            return new ChunkCoords(scaledX >> TileChunk.Bits, scaledY >> TileChunk.Bits);
        }

        public Vector2 ToWorldCoords()
        {
            return new Vector2(Cx << TileChunk.Bits, Cy << TileChunk.Bits) * Tile.Size;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Cx.GetHashCode() * 397) ^ Cy.GetHashCode();
            }
        }

        public bool Equals(ChunkCoords other)
        {
            return Cx == other.Cx && Cy == other.Cy;
        }

        public override string ToString()
        {
            return $"Chunk{Cx},{Cy}";
        }

        public static void GlobalToLocalBlockCoords(int x, int y, out int lx, out int ly)
        {
            lx = x & TileChunk.LocalBitMask;
            ly = y & TileChunk.LocalBitMask;
        }
    }
}
