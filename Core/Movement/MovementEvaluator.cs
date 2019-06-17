using DefaultEcs.System;

namespace Core.Movement
{
    public class MovementEvaluator : ISystem<TickContext>
    {
        public bool IsEnabled { get; set; }

        public void Update(TickContext state)
        {
            var requests = state.Factory.MovementRequests.Requests;
            foreach (var request in requests)
            {
                var results = new MovementResults(request.From, request.Destination);
                if (state.Factory.GetChunkBody(request.Destination) >= 0)
                {
                    // Something is in the way
                    // TODO: Take into account MovesAhead
                    // TODO: Should probably handle case where Entity doesn't exist anymore
                    results.Success = false;
                }
                else
                {
                    results.Success = true;
                }
                request.Entity.Set(results);
            }
        }

        public void Dispose()
        {
        }
    }
}
