using ARA2D.Core;
using Nez;

namespace ARA2D.TileEntities.Components
{
    public class TileEntityPlacement : Component
    {
        public TileEntityPlacement() : this(PlacementType.Check, new IntVector2(), new IntVector2()) {}

        public TileEntityPlacement(PlacementType type, IntVector2 anchor, IntVector2 size, int tileEntityID = 0)
        {
            TileEntityID = tileEntityID;
            Type = type;
            Anchor = anchor;
            Size = size;
        }

        public enum PlacementType
        {
            Check,
            Place
        }

        public int TileEntityID;
        public PlacementType Type;
        public IntVector2 Anchor;
        public IntVector2 Size;
        public bool? Result = null;
    }
}
