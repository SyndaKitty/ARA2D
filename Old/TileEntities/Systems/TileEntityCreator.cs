using ARA2D.Core;
using ARA2D.TileEntities.Components;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;

namespace ARA2D.TileEntities.Systems
{
    public class TileEntityCreator : EntityProcessingSystem
    {
        readonly IComponentProvider componentProvider;

        public TileEntityCreator(IComponentProvider componentProvider) : base(new Matcher().all(typeof(TileEntityTemplate), typeof(TileEntityCreation)))
        {
            this.componentProvider = componentProvider;
        }

        public override void process(Entity entity)
        {
            var ids = componentProvider.GetComponent<ExistingIDs>();

            var template = entity.getComponent<TileEntityTemplate>();
            var creation = entity.getComponent<TileEntityCreation>();

            var id = ids.GetNextID();
            var tileEntity = scene.createEntity($"TileEntity{id}");

            tileEntity.addComponent<TileEntity>(new TileEntity(template.Size));

            // Create sprite for tile entity
            var sprite = new Sprite(template.Texture) {origin = Vector2.Zero };
            tileEntity.addComponent<Sprite>(sprite);

            // Place tile entity in correct location
            tileEntity.position = TileCoords.ToWorldSpace(creation.Anchor.X, creation.Anchor.Y).ToVector2();
            tileEntity.transform.scale = template.Scale;

            entity.removeComponent<TileEntityCreation>();
        }
    }
}
