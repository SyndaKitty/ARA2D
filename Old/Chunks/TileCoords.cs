using System;
using ARA2D.Chunks;
using ARA2D.Core;
namespace ARA2D
{
    public static class TileCoords
    {
        public static IntVector2 FromWorldSpace(float x, float y)
        {
            return new IntVector2((long)(x / Tile.Size), (long)(y / Tile.Size));
        }

        public static IntVector2 ToWorldSpace(long tx, long ty)
        {
            return new IntVector2(tx * Tile.Size, ty * Tile.Size);
        }

        public static IntVector2 ToLocalSpace(long tx, long ty)
        {
            return new IntVector2(tx & TileChunk.LocalBitMask, ty & TileChunk.LocalBitMask);
        }
    }
}
