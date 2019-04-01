using System.Collections.Generic;

namespace ARA2D
{
    public class World
    {
        Dictionary<ChunkCoords, Chunk> chunks;

        public World()
        {
            chunks = new Dictionary<ChunkCoords, Chunk>();
        }
    }
}
