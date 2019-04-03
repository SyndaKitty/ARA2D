using System;
using Nez;

namespace ARA2D.Components
{
    public class ChunkRemovedEvent : Component
    {
        public readonly ChunkCoords Coords;

        public ChunkRemovedEvent(ChunkCoords coords)
        {
            Coords = coords;
        }
    }
}
