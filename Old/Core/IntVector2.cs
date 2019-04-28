using System;
using Microsoft.Xna.Framework;

namespace ARA2D.Core
{
    public class IntVector2 : IEquatable<IntVector2>
    {
        public long X;
        public long Y;

        public IntVector2() : this(0, 0) {}

        public IntVector2(long xy) : this(xy, xy) {}

        public IntVector2(long x, long y)
        {
            X = x;
            Y = y;
        }

        public Vector2 ToVector2()
        {
            return new Vector2(X, Y);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }

        public bool Equals(IntVector2 other)
        {
            return X == other.X && Y == other.Y;
        }

        public void Deconstruct(out long x, out long y)
        {
            x = X;
            y = Y;
        }

        public override string ToString()
        {
            return $"{X},{Y}";
        }
    }
}
