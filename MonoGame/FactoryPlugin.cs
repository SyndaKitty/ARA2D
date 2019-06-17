using System.Collections.Generic;
using Core.Buildings;
using Core.Plugins;
using DefaultEcs;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Rendering;

namespace MonoGame
{
    public class FactoryPlugin : IFactoryPlugin
    {
        readonly Dictionary<BuildingType, Texture2D> buildingTextures;

        public FactoryPlugin(Texture2D buildingTexture, Texture2D computerTexture)
        {
            buildingTextures = new Dictionary<BuildingType, Texture2D>
            {
                [BuildingType.Test] = buildingTexture, [BuildingType.Computer] = computerTexture
            };
        }

        public void Chunk(Entity entity)
        {
            entity.Set(new ChunkMesh());
        }

        public void Building(Entity entity)
        {
            var building = entity.Get<Building>();
            entity.Set(new Sprite(buildingTextures[building.Type]));
        }

        public void Global(Entity entity)
        {
        }

        public void Camera(Entity entity)
        {
        }

        public void BuildingPlacement(Entity entity)
        {
        }

        public void CheckBodyPlacement(Entity entity)
        {
        }

        public void ChunkBodies(Entity entity)
        {
        }

        public void BuildingPlacementGhost(Entity entity)
        {
            var building = entity.Get<Building>();
            var sprite = new Sprite(buildingTextures[building.Type]);
            entity.Set(sprite);
        }
    }
}
