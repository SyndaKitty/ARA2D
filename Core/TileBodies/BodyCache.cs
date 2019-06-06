using Core.Position;
using System.Collections.Generic;

namespace Core.TileBodies
{
    public class BodyCache
    {
        public Dictionary<TileCoords, ChunkBodies> ChunkLookup = new Dictionary<TileCoords, ChunkBodies>(TileCoords.ChunkCoordsComparer);
    }
}