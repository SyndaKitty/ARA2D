using Core.Plugins;
using Core.Position;
using Core.Rendering;
using Core.Tiles;
using Core.WorldGeneration;
using DefaultEcs;
using System.Numerics;

namespace Core.Archetypes
{
    public class Factory
    {
        readonly IFactoryPlugin plugin;

        public Factory(IFactoryPlugin plugin)
        {
            this.plugin = plugin;
        }

        public Entity CreateChunk(TileCoords coords, Chunk chunk)
        {
            var entity = Engine.World.CreateEntity();
            chunk.Tiles = new short[Chunk.Size * Chunk.Size];
            chunk.New = false;
            chunk.TilesChanged = true;

            entity.Set(chunk);
            entity.Set(new GridTransform(coords));

            plugin?.Chunk(entity);

            return entity;
        }

        public Entity CreateBuilding(long chunkX, int localX, long chunkY, int localY)
        {
            var entity = Engine.World.CreateEntity();
            entity.Set(new GridTransform(new TileCoords(chunkX, localX, chunkY, localY)));
            
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
            entity.Set(new Transform(position));

            plugin?.Camera(entity);

            return entity;
        }
    }
}
