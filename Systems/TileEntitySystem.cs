using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Nez;

namespace ARA2D.Systems
{
    public class TileEntitySystem : ProcessingSystem
    {
        readonly Dictionary<int, TileEntity> tileEntities;
        readonly IDTracker idTracker;

        public TileEntitySystem()
        {
            tileEntities = new Dictionary<int, TileEntity>();
            idTracker = new IDTracker();
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
            tileEntity.ID = idTracker.GetNextID();
            tileEntities[tileEntity.ID] = tileEntity;

            // Add tileEntity renderable
            var entity = Core.scene.createEntity($"TileEntity{tileEntity.ID} Renderable");
            entity.addComponent(tileEntity.GenerateRenderable());
            entity.position = new Vector2(bx * Tile.Size, by * Tile.Size);
        }

        public void DeleteTileEntity(int id)
        {
            Insist.isTrue(IsTileEntityLoaded(id));
            tileEntities.Remove(id);
            idTracker.ReleaseID(id);
        }
    }
}
