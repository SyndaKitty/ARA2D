using Core.Position;

namespace Core.TileBodies
{
    public class BodyPlacement
    {
        public PlacementType Type;
        public TileCoords Anchor;
        public int Width;
        public int Height;
        public bool Success;

        public BodyPlacement(PlacementType type, TileCoords anchor, int width, int height)
        {
            Type = type;
            Anchor = anchor;
            Width = width;
            Height = height;
        }
    }

    public enum PlacementType
    {
        Place,
        Check
    }
}