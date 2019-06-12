using System;
using Core.Tiles;
using DefaultEcs.System;
using System.Numerics;

namespace Core.Position
{
    public class GridMatrixUpdate : AComponentSystem<FrameContext, GridTransform>
    {
        public GridMatrixUpdate() : base(Engine.World, new SystemRunner<FrameContext>(Environment.ProcessorCount))
        {
        }

        protected override void Update(FrameContext state, Span<GridTransform> components)
        {
            foreach (GridTransform transform in components)
            {
                if (!transform.Dirty) continue;

                // Recalculate matrix
                float globalX = transform.Coords.ChunkX * Chunk.Size + transform.Coords.LocalX;
                float globalY = transform.Coords.ChunkY * Chunk.Size + transform.Coords.LocalY;
                transform.Matrix = Matrix4x4.CreateTranslation(globalX, globalY, 0);
            }
        }
    }
}
