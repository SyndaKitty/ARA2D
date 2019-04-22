namespace ARA2D.Movement
{
    public class MovementRequest
    {
        public int Index;
        public int DestX;
        public int DestY;
        public Direction Direction;
        // Whether or not there is another move that depends on this move's result
        public bool HasDependent;
        // Whether or not this move depends on another's result
        public bool IsDependent;

        public MovementRequest(int destX, int destY, Direction direction, int index)
        {
            DestX = destX;
            DestY = destY;
            Direction = direction;
            Index = index;
        }
    }
}
