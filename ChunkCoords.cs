using System;

namespace ARA2D
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
