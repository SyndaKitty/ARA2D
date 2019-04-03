using Nez;

namespace ARA2D.Components
{
    public class ChunkGeneratedEvent : Component
    {
        public readonly ChunkCoords Coords;
        public readonly Chunk Chunk;

        public ChunkGeneratedEvent(ChunkCoords coords, Chunk chunk)
        {
            Coords = coords;
            Chunk = chunk;
        }
    }
}
