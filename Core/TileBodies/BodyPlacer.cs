using Core.Archetypes;
using Core.Position;
using Core.Tiles;
using DefaultEcs;
using DefaultEcs.System;

namespace Core.TileBodies
{
    public class BodyPlacer : AEntitySystem<FrameContext>
    {
        public BodyPlacer(Factory factory) : base(factory.BodyPlacementSet)
        {
        }
        
        protected override void Update(FrameContext state, in Entity entity)
        {
            var placement = entity.Get<BodyPlacement>();
            int lx = placement.Anchor.LocalX;
            int ly = placement.Anchor.LocalY;
            long cx = placement.Anchor.ChunkX;
            long cy = placement.Anchor.ChunkY;

            TileBody tileBody = new TileBody { ID = -1 };
            if (placement.Type == PlacementType.Place) tileBody = entity.Get<TileBody>();

            for (int y = ly; y < ly + placement.Height; y++)
            {
                for (int x = lx; x < lx + placement.Width; x++)
                {
                    var coords = new TileCoords(cx, x, cy, y);
                    var chunkBodies = state.Factory.GetChunkBodies(coords);
                    if (chunkBodies.Bodies[coords.LocalY * Chunk.Size + coords.LocalX] >= 0)
                    {
                        placement.Success = false;
                        if (placement.Type == PlacementType.Place)
                        {
                            // The TileBody was unable to be created, so release the ID
                            state.Factory.TileBodyID.ReleaseID(tileBody.ID);
                        }
                        return;
                    }
                }
            }

            if (placement.Type == PlacementType.Place)
            {
                for (int y = ly; y < ly + placement.Height; y++)
                {
                    for (int x = lx; x < lx + placement.Width; x++)
                    {
                        var coords = new TileCoords(cx, x, cy, y);
                        var chunkBodies = state.Factory.GetChunkBodies(coords);
                        chunkBodies.Bodies[coords.LocalY * Chunk.Size + coords.LocalX] = tileBody.ID;
                    }
                }
            }

            placement.Success = true;
        }
    }
}