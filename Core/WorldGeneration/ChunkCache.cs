using System.Collections.Generic;
using Core.Position;
using Core.Tiles;

namespace Core.WorldGeneration
{
    public class ChunkCache
    {
        public Dictionary<TileCoords, Chunk> ChunkLookup = new Dictionary<TileCoords, Chunk>(TileCoords.ChunkCoordsComparer);
    }
}
