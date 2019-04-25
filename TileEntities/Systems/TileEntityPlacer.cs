﻿using ARA2D.Chunks;
using Nez;
using ARA2D.Core;
using ARA2D.TileEntities.Components;

namespace ARA2D.TileEntities
{
    public class TileEntityPlacer : EntityProcessingSystem
    {
        // TODO: True ECS refactor
        //readonly Color validPlacementColor = new Color(255, 255, 255, 180);
        //readonly Color invalidPlacementColor = new Color(255, 64, 64, 180);

        readonly IComponentProvider componentProvider;

        public TileEntityPlacer(IComponentProvider componentProvider) : base(new Matcher().all(typeof(TileEntityPlacement)))
        {
            this.componentProvider = componentProvider;
        }

        public override void process(Entity entity)
        {
            var grid = componentProvider.GetComponent<Grid>();
            var placement = entity.getComponent<TileEntityPlacement>();

            var (ax, ay) = placement.Anchor;
            var (width, height) = placement.Size;

            // If we're placing we should check all tiles before we place, so run this loop twice, and second time set tiles
            var runs = placement.Type == TileEntityPlacement.PlacementType.Check ? 1 : 2;

            // TODO: This is gross
            for (int run = 0; run < runs; run++)
            {
                for (long y = ay; y < ay + height; y++)
                {
                    for (long x = ax; x < ax + width; x++)
                    {
                        ChunkCoords coords = ChunkCoords.FromBlockCoords(x, y);
                        // If the chunk isn't loaded return early
                        if (!grid.TileEntityChunks.TryGetValue(coords, out var chunk))
                        {
                            return;
                        }

                        var (lx, ly) = TileCoords.FromWorldSpace(x, y);
                        // If space is already occupied
                        if (chunk.TileEntityIDs[lx, ly] > 0)
                        {
                            placement.Result = false;
                            return;
                        }
                        if (run == 1)
                        {
                            chunk.TileEntityIDs[lx, ly] = placement.TileEntityID;
                        }
                    }
                }
                
                placement.Result = true;
            }
        }
    }
}
