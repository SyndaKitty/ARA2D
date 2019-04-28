using System.Collections.Generic;
using ARA2D.Core;
using Nez;

namespace ARA2D.Movement
{
    public class MovementRequests : Component
    {
        public List<MovementRequest> Requests;
        public Dictionary<IntVector2, int>[] Directional;

        public MovementRequests()
        {
            Requests = new List<MovementRequest>();
            Directional = new Dictionary<IntVector2, int>[4];
            for (int i = 0; i < Directional.Length; i++)
                Directional[i] = new Dictionary<IntVector2, int>();
        }

        public void Clear()
        {
            Requests.Clear();
            for (int i = 0; i < Directional.Length; i++)
                Directional[i].Clear();
        }
    }
}
