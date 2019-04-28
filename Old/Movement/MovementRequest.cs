using ARA2D.Core;

namespace ARA2D.Movement
{
    public class MovementRequest
    {
        public int Index;
        public IntVector2 Destination;
        public Direction Direction;
        // Whether or not there is another move that depends on this move's result
        public bool HasDependent;
        // Whether or not this move depends on another's result
        public bool IsDependent;

        public MovementRequest(IntVector2 destination, Direction direction, int index)
        {
            Destination = destination;
            Direction = direction;
            Index = index;
        }
    }
}
