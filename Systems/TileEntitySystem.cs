using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Nez;

namespace ARA2D.Systems
{
    public class TileEntitySystem : ProcessingSystem
    {
        readonly Dictionary<int, TileEntity> tileEntities;
        readonly Dictionary<ChunkCoords, TileEntityChunk> loadedChunks;

        readonly IDTracker idTracker;

        // TODO: Hook up ChunkGenerated event from World.cs to load TileEntity renderables
        public TileEntitySystem()
        {
            tileEntities = new Dictionary<int, TileEntity>();
            loadedChunks = new Dictionary<ChunkCoords, TileEntityChunk>();
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
        
        public bool PlaceTileEntity(TileEntity tileEntity, long bx, long by)
        {
            var bounds = tileEntity.GetBounds();
            if (!CheckOrMarkBounds(bounds, bx, by)) return false;
            
            tileEntity.ID = idTracker.GetNextID();
            CheckOrMarkBounds(bounds, bx, by, tileEntity.ID);
            tileEntities[tileEntity.ID] = tileEntity;
            
            // Add tileEntity renderable
            var entity = Core.scene.createEntity($"TIRenderable{tileEntity.ID}");
            entity.addComponent(tileEntity.GenerateRenderable());
            entity.position = new Vector2(bx * Tile.Size, by * Tile.Size);
            return true;
        }

        // TODO: Find a better way to prevent repitition
        /// <summary>
        /// This method has two different uses:
        /// If passed id is 0, then check to see if the bounds can fit at the x,y provided
        /// If passed it is not 0, then set ID of tile entity for the bounds at the x,y provided
        /// </summary>
        /// <param name="bounds">The bounds of the tile entity</param>
        /// <param name="bx">The x location of the anchor</param>
        /// <param name="by">The y location of the anchor</param>
        /// <param name="id">The id of the tileEntity, use 0 to perform a check to see if the bounds fit.</param>
        /// <returns>If id is 0, returns whether or not the bounds fit at the location provided. Otherwise returns true</returns>
        bool CheckOrMarkBounds(Tuple<int, int> bounds, long bx, long by, int id = 0)
        {
            var (width, height) = bounds;
            //long prevX, prevY;
            for (long y = by; y < by + height; y++)
            {
                for (long x = bx; x < bx + width; x++)
                {
                    // TODO: Optimize this by avoiding unnecessary re-lookups of chunkcoords
                    ChunkCoords coords = ChunkCoords.FromBlockCoords(x, y);

                    if (id == 0)
                    {
                        if (RequiredChunk(coords).TileEntityIDs[x & Chunk.LocalBitMask, y & Chunk.LocalBitMask] > 0) return false;
                    }
                    else
                    {
                        RequiredChunk(coords).TileEntityIDs[x & Chunk.LocalBitMask, y & Chunk.LocalBitMask] = id;
                    }
                    //prevX = x;
                } 
                //prevY = y;
            }
            return true;
        }

        TileEntityChunk RequiredChunk(ChunkCoords coords)
        {
            if (!loadedChunks.ContainsKey(coords)) return GenerateChunk(coords);
            return loadedChunks[coords];
        }

        public TileEntityChunk GenerateChunk(ChunkCoords coords)
        {
            var chunk = new TileEntityChunk(coords);
            loadedChunks[coords] = chunk;
            return chunk;
        }

        public void DeleteTileEntity(int id)
        {
            Insist.isTrue(IsTileEntityLoaded(id));
            tileEntities.Remove(id);
            idTracker.ReleaseID(id);
        }
    }
}
