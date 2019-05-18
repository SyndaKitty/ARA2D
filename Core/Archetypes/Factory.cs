using Core.Plugins;
using Core.Position;
using Core.Tiles;

namespace Core.Archetypes
{
    public class Factory
    {
        readonly IFactoryPlugin plugin;

        public Factory(IFactoryPlugin plugin)
        {
            this.plugin = plugin;
        }

        public void CreateChunk(long chunkX, long chunkY)
        {
            var entity = Engine.World.CreateEntity();
            var chunk = new Chunk();
            // TODO: Replace this with world generator
            chunk.Tiles = new short[Chunk.Size * Chunk.Size];
            chunk.New = false;
            chunk.TilesChanged = true;

            entity.Set(chunk);
            entity.Set(new GridTransform(new TileCoords(chunkX, 0, chunkY, 0)));

            plugin?.Chunk(entity);
        }

        public void CreateBuilding(long chunkX, int localX, long chunkY, int localY)
        {
            var entity = Engine.World.CreateEntity();
            entity.Set(new GridTransform(new TileCoords(chunkX, localX, chunkY, localY)));
            
            plugin?.Building(entity);
        }
    }
}
