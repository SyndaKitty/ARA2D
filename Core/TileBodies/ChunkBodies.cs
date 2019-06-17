using Core.Tiles;

namespace Core.TileBodies
{
    public class ChunkBodies
    {
        public int[] Bodies = new int[Chunk.Size * Chunk.Size];

        public int this[int index] => Bodies[index]; 
    }
}