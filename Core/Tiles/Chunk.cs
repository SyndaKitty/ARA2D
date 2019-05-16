namespace Core.Tiles
{
    public class Chunk
    {
        const int Power = 3;
        public const int Size = 1 << Power;

        public short[] Tiles;

        public bool New = true;
        public bool TilesChanged;
    }
}
