namespace ARA2D.Movement
{
    public class MovementRequest
    {
        public int Index;
        public TileCoords Destination;
        public Direction Direction;
        // Whether or not there is another move that depends on this move's result
        public bool HasDependent;
        // Whether or not this move depends on another's result
        public bool IsDependent;

        public MovementRequest(TileCoords destination, Direction direction, int index)
        {
            Destination = destination;
            Direction = direction;
            Index = index;
        }
    }
}
