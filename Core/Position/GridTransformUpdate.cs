using System;
using System.Numerics;
using Core.Tiles;
using DefaultEcs.System;

namespace Core.Position
{
    public class GridTransformUpdate : AComponentSystem<LogicContext, GridTransform>
    {
        public GridTransformUpdate() : base(Engine.World, new SystemRunner<LogicContext>(Environment.ProcessorCount))
        {
        }

        protected override void Update(LogicContext state, Span<GridTransform> components)
        {
            foreach (GridTransform transform in components)
            {
                if (!transform.Coords.Dirty) continue;

                // Recalculate matrix
                float globalX = transform.Coords.ChunkX * Chunk.Size + transform.Coords.LocalX;
                float globalY = transform.Coords.ChunkY * Chunk.Size + transform.Coords.LocalY;
                transform.Matrix = Matrix4x4.CreateTranslation(globalX, globalY, 0);
            }
        }
    }
}
