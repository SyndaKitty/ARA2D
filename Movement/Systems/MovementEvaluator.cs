using System.Collections.Generic;
using ARA2D.Core;
using ARA2D.Ticks;
using Nez;

namespace ARA2D.Movement.Systems
{
    public class MovementEvaluator : ProcessingSystem
    {
        readonly IComponentProvider componentProvider;

        public MovementEvaluator(IComponentProvider componentProvider)
        {
            this.componentProvider = componentProvider;
        }

        public override void process()
        {
            var tickInfo = componentProvider.GetComponent<TickInfo>();
            if (!tickInfo.Ticking) return;

            var requestComponent = componentProvider.GetComponent<MovementRequests>();
            var requests = requestComponent.Requests;

            Dictionary<TileCoords, int> validRequests = new Dictionary<TileCoords, int>();
            HashSet<TileCoords> invalidRequests = new HashSet<TileCoords>();

            foreach (var request in requests)
            {
                // TODO: Take into account TileEntityChunk so we don't run into stationary things


                if (!validRequests.ContainsKey(request.Destination))
                {
                    // We don't remove right away so that 2 duplicate requests don't cancel out
                    invalidRequests.Add(request.Destination);
                }
                else validRequests.Add(request.Destination, request.Index);
            }

            foreach (var coords in invalidRequests)
            {
                validRequests.Remove(coords);
            }

        }
    }
}
