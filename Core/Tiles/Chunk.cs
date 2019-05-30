namespace Core.Tiles
{
    public class Chunk
    {
        public const int Bits = 3;
        public const int Size = 1 << Bits;

        public short[] Tiles;

        public bool New = true;
        public bool TilesChanged;
    }
}
