using Core;
using DefaultEcs.System;

namespace MonoGame.Rendering
{
    public class ChunkMeshRenderer : AEntitySystem<RenderContext>
    {
        public ChunkMeshRenderer() : base(Engine.World.GetEntities().With(typeof(GridTransform), typeof(ChunkMesh)).Build())
        {
        }
    }
}
