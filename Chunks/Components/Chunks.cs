using System.Collections.Generic;
using Nez;

namespace ARA2D.Chunks
{
    public class Chunks : Component
    {
        Dictionary<ChunkCoords, TileChunk> TileChunks = new Dictionary<ChunkCoords, TileChunk>();
        Dictionary<ChunkCoords, TileEntityChunk> TileEntityChunks = new Dictionary<ChunkCoords, TileEntityChunk>();
    }
}
