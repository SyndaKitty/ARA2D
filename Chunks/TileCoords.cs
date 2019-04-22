using System;
using Microsoft.Xna.Framework;

namespace ARA2D
{
    public class TileCoords : IEquatable<TileCoords>
    {
        public static Vector2 FromWorldSpace(float x, float y)
        {
            return new Vector2(x / Tile.Size, y / Tile.Size);
        }

        public static Vector2 ToWorldSpace (float tx, float ty)
        {
            return new Vector2(tx * Tile.Size, ty * Tile.Size);
        }

        public long X;
        public long Y;

        public TileCoords(long x, long y)
        {
            X = x;
            Y = y;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }

        public bool Equals(TileCoords other)
        {
            return X == other.X && Y == other.Y;
        }

        public override string ToString()
        {
            return $"Tile{X},{Y}";
        }
    }
}
