using System.Diagnostics;
using System.Linq.Expressions;
using Core.Plugins;
using Core.Position;
using Core.Rendering;
using Core.TileBodies;
using Core.Tiles;
using Core.WorldGeneration;
using DefaultEcs;
using System.Numerics;
using Core.Buildings;
using Core.Input;

namespace Core.Archetypes
{
    public class Factory
    {
        readonly IFactoryPlugin plugin;

        public readonly EntitySet ChunkSet;
        public readonly EntitySet CameraSet;
        public readonly EntitySet GlobalSet;
        public readonly EntitySet BodyPlacementSet;
        //public readonly EntitySet BuildingSet
        public ChunkCache ChunkCache;
        public ChunkBodyCache ChunkBodyCache;

        public Factory(IFactoryPlugin plugin)
        {
            this.plugin = plugin;
            ChunkSet = Engine.World.GetEntities().With<Chunk>().Build();
            CameraSet = Engine.World.GetEntities().With<Camera>().Build();
            GlobalSet = Engine.World.GetEntities().With<Global>().Build();
            BodyPlacementSet = Engine.World.GetEntities().With<BodyPlacement>().Build();
        }

        public Entity CreateChunk(TileCoords coords, Chunk chunk)
        {
            var entity = Engine.World.CreateEntity();
            chunk.TilesChanged = true;

            entity.Set(chunk);
            entity.Set(new GridTransform(coords));

            plugin?.Chunk(entity);

            return entity;
        }

        public Entity CheckBuildingPlacement(TileCoords anchor, int width, int height)
        {
            var entity = Engine.World.CreateEntity();
            entity.Set(new BodyPlacement(PlacementType.Check, anchor, width, height));

            plugin?.CheckBodyPlacement(entity);
            return entity;
        }

        public Entity PlaceBuilding(TileCoords anchor, int width, int height, BuildingType type)
        {
            Debug.Assert(type != BuildingType.None);

            var entity = Engine.World.CreateEntity();
            entity.Set(new BodyPlacement(PlacementType.Place, anchor, width, height));
            entity.Set(new Building(type));

            plugin?.BuildingPlacement(entity);
            return entity;
        }

        public Entity CreateBuilding(BodyPlacement placement, Building building)
        {
            Debug.Assert(placement.Success);

            var entity = Engine.World.CreateEntity();
            entity.Set(new GridTransform(placement.Anchor));
            entity.Set(building);

            plugin?.Building(entity);

            return entity;
        }

        public Entity CreateGlobal()
        {
            var entity = Engine.World.CreateEntity();

            ChunkCache = new ChunkCache();
            ChunkBodyCache = new ChunkBodyCache();

            entity.Set(ChunkCache);
            entity.Set(ChunkBodyCache);
            entity.Set(new ChunkLoadRequests());
            entity.Set(new Global());

            var buildingMenu = new BuildingMenu();
            buildingMenu.Enabled = true;
            buildingMenu.SelectedBuildingType = BuildingType.Test;
            buildingMenu.SelectedBuildingWidth = 3;
            buildingMenu.SelectedBuildingHeight = 2;
            entity.Set(buildingMenu);

            plugin?.Global(entity);

            return entity;
        }

        public Entity CreateCamera(Vector2 position)
        {
            var entity = Engine.World.CreateEntity();
            entity.Set(new Camera());
            entity.Set(new Transform(position, new Vector2(16, 16)));

            plugin?.Camera(entity);

            return entity;
        }

        public ChunkBodies GetChunkBodies(TileCoords coords)
        {
            if (ChunkBodyCache.ChunkLookup.TryGetValue(coords, out var bodies))
            {
                return bodies;
            }
            // TODO: Lookup bodies in save file
            var entity = Engine.World.CreateEntity();
            bodies = new ChunkBodies();
            for (int i = 0; i < Chunk.Size * Chunk.Size; i++) bodies.Bodies[i] = -1;
            entity.Set(bodies);
            entity.Set(coords);
            ChunkBodyCache.ChunkLookup.Add(coords, bodies);
            return bodies;
        }
    }
}
