using System.Collections.Generic;
using Core.Position;
using DefaultEcs;

namespace Core.Movement
{
    public class MovementRequests
    {
        public List<MovementRequestInfo> Requests;
        Dictionary<DirectionalKey, int> directional;

        public MovementRequests()
        {
            Requests = new List<MovementRequestInfo>(1024);
            directional = new Dictionary<DirectionalKey, int>();
        }

        public void Add(TileCoords from, Direction direction, Entity entity)
        {
            int dx = direction == Direction.Left ? -1 : direction == Direction.Right ? 1 : 0;
            int dy = direction == Direction.Up ? -1 : direction == Direction.Down ? 1 : 0;

            TileCoords destination = TileCoords.Create(from, dx, dy);
            TileCoords ahead = TileCoords.Create(destination, dx, dy);
            TileCoords behind = from;

            // TODO: request creation and direction insert is not thread safe at this point.. make sure to not make Requests in a multi-threaded context!

            MovementRequestInfo requestInfo = new MovementRequestInfo(direction, from, destination, entity);
            Requests.Add(requestInfo);

            // TODO: Movement Chains
            //int requestIndex = Requests.Count - 1;
            //bool isDep = false;
            //if (directional.TryGetValue(new DirectionalKey(), ))
            


            //DirectionalKey key = new DirectionalKey(direction, destination);
            //directional.Add(key, requestIndex);
        }

        public void Clear()
        {
            Requests.Clear();
        }
    }

    struct DirectionalKey : IEqualityComparer<DirectionalKey>
    {
        public Direction Direction;
        public TileCoords Destination;

        public DirectionalKey(Direction direction, TileCoords destination)
        {
            Direction = direction;
            Destination = destination;
        }

        public bool Equals(DirectionalKey x, DirectionalKey y)
        {
            return x.Direction == y.Direction && x.Destination.Equals(y.Destination);
        }

        public int GetHashCode(DirectionalKey obj)
        {
            unchecked
            {
                return ((int)obj.Direction * 397) ^ obj.Destination.GetHashCode();
            }
        }
    }

    public class MovementRequestInfo
    {
        public TileCoords From;
        public TileCoords Destination;
        public Direction Direction;
        public Entity Entity;
        public bool MovesAhead; // There is another move in front of this move
        public bool MovesBehind; // There is another move behind this move

        public MovementRequestInfo(Direction direction, TileCoords from, TileCoords destination, Entity entity)
        {
            Destination = destination;
            From = from;
            Direction = direction;
            Entity = entity;
        }
    }
}
