using System;
using Nez;

namespace ARA2D
{
    public interface TileEntity
    {
        RenderableComponent GenerateRenderable();
        int GetID();
        Tuple<int, int> GetBounds();
    }
}
