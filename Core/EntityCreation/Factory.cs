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

        public Entity PlaceBuilding(PlacementType type, TileCoords anchor, int width, int height)
        {
            var entity = Engine.World.CreateEntity();
            entity.Set(new BodyPlacement(PlacementType.Place, anchor, width, height));

            plugin?.BuildingPlacement(entity);
            return entity;
        }

        public Entity CreateBuilding(BodyPlacement placement)
        {
            Debug.Assert(placement.Success);

            var entity = Engine.World.CreateEntity();
            entity.Set(new GridTransform(placement.Anchor));
            
            plugin?.Building(entity);

            return entity;
        }

        public Entity CreateGlobal()
        {
            var entity = Engine.World.CreateEntity();
            entity.Set(new ChunkCache());
            entity.Set(new ChunkLoadRequests());
            entity.Set(new Global());

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
    }
}
