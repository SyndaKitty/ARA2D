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

        public void PlaceTileEntity(TileEntity tileEntity, long bx, long by)
        {
            tileEntity.ID = idTracker.GetNextID();
            tileEntities[tileEntity.ID] = tileEntity;

            // Add tileEntity renderable
            var entity = Core.scene.createEntity($"TileEntity{tileEntity.ID} Renderable");
            entity.addComponent(tileEntity.GenerateRenderable());
            entity.position = new Vector2(bx * Tile.Size, by * Tile.Size);

            MarkBounds(tileEntity, bx, by);
        }

        void MarkBounds(TileEntity tileEntity, long bx, long by)
        {
            var (width, height) = tileEntity.GetBounds();
            //long prevX, prevY;
            for (long y = by; y < by + height; y++)
            {
                for (long x = bx; x < bx + width; x++)
                {
                    // TODO: Optimize this by avoiding unnecessary re-lookups of chunkcoords
                    ChunkCoords coords = ChunkCoords.FromBlockCoords(x, y);
                    RequiredChunk(coords).TileEntityIDs[x & Chunk.LocalBitMask, y & Chunk.LocalBitMask] = tileEntity.ID;
                    Console.WriteLine($"ID {tileEntity.ID} written at {coords} localX: {x & Chunk.LocalBitMask} localY: {y & Chunk.LocalBitMask}");
                    //prevX = x;
                } 
                //prevY = y;
            }
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
