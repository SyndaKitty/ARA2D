namespace Core.Tiles
{
    public class Chunk
    {
        public const int Bits = 6;
        public const int Size = 1 << Bits;

        public short[] Tiles = new short[Size * Size]; 

        public bool TilesChanged;
    }
}
