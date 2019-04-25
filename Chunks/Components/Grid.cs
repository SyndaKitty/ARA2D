using System.Collections.Generic;
using Nez;

namespace ARA2D.Chunks
{
    public class Grid : Component
    {
        public Dictionary<ChunkCoords, TileChunk> TileChunks = new Dictionary<ChunkCoords, TileChunk>();
        public Dictionary<ChunkCoords, TileEntityChunk> TileEntityChunks = new Dictionary<ChunkCoords, TileEntityChunk>();
    }
}
