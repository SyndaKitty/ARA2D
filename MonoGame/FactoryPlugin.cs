using Core.Plugins;
using DefaultEcs;
using MonoGame.Rendering;

namespace MonoGame
{
    public class FactoryPlugin : IFactoryPlugin
    {
        public void Chunk(Entity entity)
        {
            entity.Set(new ChunkMesh());
        }
    }
}
