using System;
using System.Collections.Generic;
using Nez;

namespace ARA2D
{
    public interface TileEntity
    {
        int ID { get; set; }
        List<ChunkCoords> ContainingChunkCoords { get; set; }

        RenderableComponent GenerateRenderable();
        Tuple<int, int> GetBounds();
        bool CanSleep();

        void Update();
    }
}
