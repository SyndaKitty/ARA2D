using System;
using Core.Rendering;
using DefaultEcs.System;

namespace Core.WorldGeneration
{
    public class CameraDistanceLoader : AComponentSystem<RenderContext, Camera>
    {
        public CameraDistanceLoader() : base(Engine.World)
        {
        }

        protected override void Update(RenderContext state, Span<Camera> components)
        {
            var updates = state.GlobalEntity.Get<ChunkLoadRequests>();
            foreach (var camera in components)
            {
                // TODO: Add requests for chunks in vicinity of camera
            }
        }
    }
}
