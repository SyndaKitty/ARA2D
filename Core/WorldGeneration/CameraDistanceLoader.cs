using System;
using System.Collections.Generic;
using Core.Position;
using Core.Rendering;
using Core.Tiles;
using DefaultEcs;
using DefaultEcs.System;

namespace Core.WorldGeneration
{
    public class CameraDistanceLoader : AEntitySystem<TickContext>
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

        readonly List<OffsetPoint> offsetPoints = new List<OffsetPoint>();

        public CameraDistanceLoader() : base(Engine.World.GetEntities().With(typeof(Camera), typeof(Transform)).Build())
        {
            Distance = 3;
        }

        public void CalculateOffsetPoints()
        {
            offsetPoints.Clear();
            for (int y = -Distance; y <= Distance; y++)
            {
                for (int x = -Distance; x <= Distance; x++)
                {
                    offsetPoints.Add(new OffsetPoint(x, y));
                }
            }
            offsetPoints.Sort();
        }
        
        protected override void Update(TickContext state, ReadOnlySpan<Entity> entities)
        {
            var requests = state.GlobalEntity.Get<ChunkLoadRequests>().Requests;
            var cache = state.GlobalEntity.Get<ChunkCache>();

            foreach (var entity in entities)
            {
                var camera = entity.Get<Camera>();
                var transform = entity.Get<Transform>();

                // Calculate chunk coords of camera
                long chunkX = (long) transform.X >> Chunk.Bits;
                long chunkY = (long) transform.Y >> Chunk.Bits;

                foreach (var offsetPoint in offsetPoints)
                {
                    TileCoords offsetCoords = new TileCoords(chunkX + offsetPoint.Ox, 0, chunkY + offsetPoint.Oy, 0);
                    if (cache.ChunkLookup.ContainsKey(offsetCoords)) continue;
                    requests.Add(offsetCoords);
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
