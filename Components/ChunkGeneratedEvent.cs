using Nez;

namespace ARA2D.Components
{
    public class ChunkGeneratedEvent : Component
    {
        public ChunkCoords Coords;
        public Chunk Chunk;

        public ChunkGeneratedEvent(ChunkCoords coords, Chunk chunk)
        {
            Coords = coords;
            Chunk = chunk;
        }
    }
}
