using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Nez;

namespace ARA2D.Systems
{
    public class TileEntitySystem : ProcessingSystem
    {
        readonly Dictionary<int, TileEntity> tileEntities;

        // TODO: Probably move this to it's own class
        // TODO: Find a better system to track IDs for tile entities
        // This way isn't flexible and likely to break
        int currentTileEntityID;
        Queue<int> ReleasedTileEntityIDs = new Queue<int>(128);

        public TileEntitySystem()
        {
            tileEntities = new Dictionary<int, TileEntity>();
        }

        public void ChunkUnloaded(ChunkCoords coords)
        {
            
        }

        public override void process()
        {
        }

        public bool IsTileEntityLoaded(int id)
        {
            return tileEntities.ContainsKey(id);
        }

        public void PlaceTileEntity(TileEntity tileEntity, long bx, long by)
        {
            tileEntity.ID = NextTileEntityID();
            tileEntities[tileEntity.ID] = tileEntity;

            // Add tileEntity renderable
            var entity = Core.scene.createEntity($"TileEntity{tileEntity.ID} Renderable");
            entity.addComponent(tileEntity.GenerateRenderable());
            entity.position = new Vector2(bx * Tile.Size, by * Tile.Size);
        }

        public int NextTileEntityID()
        {
            if (ReleasedTileEntityIDs.Count == 0)
            {
                return currentTileEntityID++;
            }
            return ReleasedTileEntityIDs.Dequeue();
        }

        public void DeleteTileEntity(int id)
        {
            Insist.isTrue(IsTileEntityLoaded(id));
            tileEntities.Remove(id);
            ReleasedTileEntityIDs.Enqueue(id);
        }
    }
}
