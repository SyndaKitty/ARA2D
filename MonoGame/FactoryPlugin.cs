using Core.Plugins;
using DefaultEcs;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Rendering;

namespace MonoGame
{
    public class FactoryPlugin : IFactoryPlugin
    {
        readonly Texture2D buildingTexture;

        public FactoryPlugin(Texture2D buildingTexture)
        {
            this.buildingTexture = buildingTexture;
        }

        public void Chunk(Entity entity)
        {
            entity.Set(new ChunkMesh());
        }

        public void Building(Entity entity)
        {
            entity.Set(new Sprite(buildingTexture));
        }

        public void Global(Entity entity)
        {
        }

        public void Camera(Entity entity)
        {
        }
    }
}
