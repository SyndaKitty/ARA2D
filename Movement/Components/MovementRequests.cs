using System.Collections.Generic;
using Nez;

namespace ARA2D.Movement
{
    public class MovementRequests : Component
    {
        public List<MovementRequest> Requests;
        public Dictionary<TileCoords, int>[] Directional;

        public MovementRequests()
        {
            Requests = new List<MovementRequest>();
            Directional = new Dictionary<TileCoords, int>[4];
            for (int i = 0; i < Directional.Length; i++)
                Directional[i] = new Dictionary<TileCoords, int>();
        }

        public void Clear()
        {
            Requests.Clear();
            for (int i = 0; i < Directional.Length; i++)
                Directional[i].Clear();
        }
    }
}
