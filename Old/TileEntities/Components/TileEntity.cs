using ARA2D.Core;
using Nez;

namespace ARA2D
{
    public class TileEntity : Component
    {
        public IntVector2 Size;

        public TileEntity(IntVector2 size)
        {
            Size = size;
        }
    }
}
