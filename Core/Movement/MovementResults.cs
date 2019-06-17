using Core.Position;

namespace Core.Movement
{
    public class MovementResults
    {
        public TileCoords From;
        public TileCoords To;
        public bool Success;

        public MovementResults(TileCoords from, TileCoords to)
        {
            From = from;
            To = to;
        }
    }
}
