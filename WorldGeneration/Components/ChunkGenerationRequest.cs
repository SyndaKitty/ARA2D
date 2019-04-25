using ARA2D.Chunks;
using Nez;

namespace ARA2D.WorldGeneration.Components
{
    public class ChunkGenerationRequest : Component
    {
        public ChunkCoords Coords;

        public ChunkGenerationRequest(ChunkCoords coords)
        {
            Coords = coords;
        }
    }
}
