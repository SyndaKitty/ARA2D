using ARA2D.Core;
using Nez;

namespace ARA2D.TileEntities.Components
{
    public class TileEntityCreation : Component
    {
        public IntVector2 Anchor;

        public TileEntityCreation(IntVector2 anchor)
        {
            Anchor = anchor;
        }
    }
}
