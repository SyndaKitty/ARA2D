using System;
using System.Collections.Generic;
using Nez;

namespace ARA2D
{
    public interface TileEntity
    {
        RenderableComponent GenerateRenderable();
        int GetID();
        Tuple<int, int> GetBounds();
        bool CanSleep();
        List<ChunkCoords> ContainingChunks();
    }
}
