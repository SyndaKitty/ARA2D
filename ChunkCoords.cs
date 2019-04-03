using System;

namespace ARA2D
{
    public class ChunkCoords : IEquatable<ChunkCoords>
    {
        const int X_SIZE = 6; // 2^6 = 64
        const int Y_SIZE = 6; // 2^6 = 64

        public long Cx;
        public long Cy;

        public ChunkCoords(long cx, long cy)
        {
            Cx = cx;
            Cy = cy;
        }

        public static ChunkCoords FromBlockCoords(long x, long y)
        {
            return new ChunkCoords(x << X_SIZE, y << Y_SIZE);
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
    }
}
