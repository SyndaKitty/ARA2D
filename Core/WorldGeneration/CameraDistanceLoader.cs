using System;
using System.Collections.Generic;
using Core.Rendering;
using DefaultEcs.System;

namespace Core.WorldGeneration
{
    public class CameraDistanceLoader : AComponentSystem<FrameContext, Camera>
    {
        int distance;
        public int Distance
        {
            get => distance;
            set
            {
                distance = value;
                CalculateOffsetPoints();
            }
        }

        List<OffsetPoint> offsetPoints = new List<OffsetPoint>();

        public CameraDistanceLoader() : base(Engine.World)
        {
            
        }

        public void CalculateOffsetPoints()
        {
            offsetPoints.Clear();
            for (int y = -Distance; y <= Distance y++)
            {
                for (int x = -Distance; x <= Distance; x++)
                {
                    offsetPoints.Add(new OffsetPoint(x, y));
                }
            }
            offsetPoints.Sort();
        }

        protected override void Update(FrameContext state, Span<Camera> components)
        {
            var updates = state.GlobalEntity.Get<ChunkLoadRequests>();
            var cache = state.GlobalEntity.Get<ChunkCache>();
            foreach (var camera in components)
            {
                foreach (var offsetPoint in offsetPoints)
                {
                    
                }
            }
        }
    }

    struct OffsetPoint : IComparable<OffsetPoint>
    {
        public int Ox;
        public int Oy;
        public float Distance;

        public OffsetPoint(int ox, int oy)
        {
            Ox = ox;
            Oy = oy;
            Distance = (float)Math.Sqrt(Ox * Ox + Oy * Oy);
        }

        public int CompareTo(OffsetPoint other)
        {
            return Distance.CompareTo(other.Distance);
        }
    }
}
