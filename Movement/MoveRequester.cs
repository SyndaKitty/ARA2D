using ARA2D.Core;

namespace ARA2D.Movement
{
    public class MoveRequester
    {
        readonly IComponentProvider componentProvider;

        public MoveRequester(IComponentProvider componentProvider)
        {
            this.componentProvider = componentProvider;
        }

        public int Request(int originX, int originY, Direction direction)
        {
            var data = componentProvider.GetComponent<MovementRequests>();

            int dx = direction == Direction.Right ? 1 : (direction == Direction.Left ? -1 : 0);
            int dy = direction == Direction.Up ? 1 : (direction == Direction.Down ? -1 : 0);

            var behind = new TileCoords(originX, originY);
            var destination = new TileCoords(originX + dx, originY + dy);
            var after = new TileCoords(originX + dx * 2, originY + dy * 2);

            var request = new MovementRequest(destination, direction, data.Requests.Count);
            data.Requests.Add(request);

            // Check for moves directly behind this one
            if (data.Directional[(int) direction].TryGetValue(behind, out int index))
            {
                request.HasDependent = true;
                data.Requests[index].IsDependent = true;
            }

            // Check for moves directly after this one
            if (data.Directional[(int)direction].TryGetValue(after, out index))
            {
                request.IsDependent = true;
                data.Requests[index].HasDependent = true;
            }

            data.Directional[(int) direction][destination] = request.Index;

            return request.Index;
        }
    }
}
