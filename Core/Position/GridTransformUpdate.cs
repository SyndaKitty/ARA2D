﻿using System;
using DefaultEcs.System;
using Microsoft.Xna.Framework;

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
                float globalX = transform.Coords.ChunkX * TileCoords.ChunkSize + transform.Coords.LocalX;
                float globalY = transform.Coords.ChunkY * TileCoords.ChunkSize + transform.Coords.LocalY;
                transform.Matrix = Matrix.CreateTranslation(globalX, globalY, 0);
            }
        }
    }
}