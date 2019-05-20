using System.Collections.Generic;
using Core.Position;

namespace Core.WorldGeneration
{
    public class ChunkLoadRequests
    {
        public HashSet<TileCoords> Requests = new HashSet<TileCoords>(TileCoords.ChunkCoordsComparer);
    }
}
