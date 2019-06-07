namespace Core.Tiles
{
    public class Chunk
    {
        public const int Bits = 3;
        public const int Size = 1 << Bits;

        public short[] Tiles = new short[Size * Size]; 

        public bool TilesChanged;
    }
}
