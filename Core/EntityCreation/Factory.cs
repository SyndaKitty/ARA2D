using System.Diagnostics;
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
using Core.Movement;

namespace Core.Archetypes
{
    public class Factory
    {
        readonly IFactoryPlugin plugin;

        public readonly EntitySet ChunkSet;
        public readonly EntitySet CameraSet;
        public readonly EntitySet GlobalSet;
        public readonly EntitySet BodyPlacementSet;
        public readonly EntitySet BuildingPlacementSet;
        public readonly EntitySet BuildingGhostSet;
        public readonly EntitySet ComputerSet;
        public readonly EntitySet MovementResultsSet;
        public readonly EntitySet PureTransformSet;

        public readonly IDTracker TileBodyID = new IDTracker();
        public ChunkCache ChunkCache;
        public ChunkBodyCache ChunkBodyCache;
        
        public readonly MovementRequests MovementRequests;

        public Factory(IFactoryPlugin plugin)
        {
            this.plugin = plugin;
            ChunkSet = Engine.World.GetEntities().With<Chunk>().Build();
            CameraSet = Engine.World.GetEntities().With<Camera>().Build();
            GlobalSet = Engine.World.GetEntities().With<Global>().Build();
            BodyPlacementSet = Engine.World.GetEntities().With<BodyPlacement>().Build();
            BuildingPlacementSet = Engine.World.GetEntities().With(typeof(BodyPlacement), typeof(Building)).Build();
            BuildingGhostSet = Engine.World.GetEntities().With<BuildingGhost>().Build();
            ComputerSet = Engine.World.GetEntities().With<Computer>().Build();
            MovementResultsSet = Engine.World.GetEntities().With<MovementResults>().Build();
            PureTransformSet = Engine.World.GetEntities().With<Transform>().Without<Camera>().Build();

            MovementRequests = new MovementRequests();
        }

        public Entity CreateChunk(TileCoords coords, Chunk chunk)
        {
            var entity = Engine.World.CreateEntity();
            chunk.TilesChanged = true;

            entity.Set(chunk);
            entity.Set(new GridTransform(coords, Chunk.Size, Chunk.Size));

            plugin?.Chunk(entity);

            return entity;
        }

        public Entity BuildingPlacementGhost(BuildingMenu menu)
        {
            var entity = Engine.World.CreateEntity();
            entity.Set(new Building(menu.SelectedBuildingType));
            entity.Set(new BuildingGhost());

            plugin?.BuildingPlacementGhost(entity);

            return entity;
        }

        public Entity CheckBuildingPlacement(Entity entity, TileCoords anchor, int width, int height)
        {
            entity.Set(new BodyPlacement(PlacementType.Check, anchor, width, height));

            plugin?.CheckBodyPlacement(entity);
            return entity;
        }

        public Entity CheckBuildingPlacement(TileCoords anchor, int width, int height)
        {
            return CheckBuildingPlacement(Engine.World.CreateEntity(), anchor, width, height);
        }

        public Entity TryPlaceBuilding(TileCoords anchor, int width, int height, BuildingType type)
        {
            Debug.Assert(type != BuildingType.None);

            var entity = Engine.World.CreateEntity();
            entity.Set(new BodyPlacement(PlacementType.Place, anchor, width, height));
            entity.Set(new TileBody(TileBodyID.Next()));
            entity.Set(new Building(type));

            plugin?.BuildingPlacement(entity);
            return entity;
        }

        public Entity CreateBuilding(BodyPlacement placement, Building building, TileBody body)
        {
            Debug.Assert(placement.Success);

            var entity = Engine.World.CreateEntity();
            entity.Set(new GridTransform(placement.Anchor, placement.Width, placement.Height));
            entity.Set(body);
            entity.Set(building);

            if (building.Type == BuildingType.Computer)
            {
                entity.Set(new Computer());
            }

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

            var buildingMenu = new BuildingMenu
            {
                Enabled = true,
                SelectedBuildingType = BuildingType.Computer,
                SelectedBuildingWidth = 1,
                SelectedBuildingHeight = 1
            };
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

        public int GetChunkBody(TileCoords coords)
        {
            return GetChunkBodies(coords)[coords.Index];
        }

        public void RequestMovement(Direction direction, Entity entity)
        {
            Debug.Assert(entity.Has<TileBody>());
            var transform = entity.Get<GridTransform>();
            Debug.Assert(transform.Width == 1 && transform.Height == 1);

            entity.Set<MovementResults>();
            MovementRequests.Add(transform.Coords, direction, entity);
        }
    }
}
