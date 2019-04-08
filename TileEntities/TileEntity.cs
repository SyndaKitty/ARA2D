using System;
using Microsoft.Xna.Framework;
using Nez;

namespace ARA2D
{
    public interface TileEntity
    {
        int ID { get; set; }

        RenderableComponent GenerateRenderable();
        Tuple<int, int> GetBounds();
        Vector2 DefaultScale();
        bool CanSleep();

        void Update();
    }
}
