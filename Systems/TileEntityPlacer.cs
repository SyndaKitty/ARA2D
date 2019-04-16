using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nez;
using System;
using ARA2D.Components;

namespace ARA2D.Systems
{
    public class TileEntityPlacer : EntityProcessingSystem
    {
        // TODO: True ECS refactor
        readonly TileEntitySystem tileEntitySystem;
        ITileEntity template;
        readonly Color validPlacementColor = new Color(255, 255, 255, 180);
        readonly Color invalidPlacementColor = new Color(255, 64, 64, 180);

        public TileEntityPlacer(TileEntitySystem tileEntitySystem) : base(new Matcher().all(typeof(UICollided)))
        {
            this.tileEntitySystem = tileEntitySystem;
            Events.OnBuildMenuItemClick += SetTemplate;
        }

        public void SetTemplate(ITileEntity template)
        {
            if (template == null)
            {
                this.template?.Entity?.destroy();
                this.template = null;
                return;
            }
            this.template = template;
            template.CreateEntity(scene, 0, 0);
        }

        public override void process(Entity entity)
        {
            if (template?.Entity == null) return;
            // Hide ghost entity if mouse is over UI element
            if (entity.getComponent<UICollided>().Collided)
            {
                if (template.Sprite != null)
                {
                    template.Sprite.color = Color.Transparent;
                }
                return;
            }

            if (Input.isKeyPressed(Keys.Left)) template.Width -= 1;
            if (Input.isKeyPressed(Keys.Right)) template.Width += 1;
            if (Input.isKeyPressed(Keys.Up)) template.Height -= 1;
            if (Input.isKeyPressed(Keys.Down)) template.Height += 1;


            // Adjust tileEntityToPlace ghost position
            var mousePoint = scene.camera.screenToWorldPoint(Input.mousePosition);
            var anchorPoint = mousePoint / Tile.Size;

            anchorPoint.X = template.Width % 2 == 0
                ? (float)Math.Round(anchorPoint.X)
                : (float)Math.Round(anchorPoint.X + .5f) - .5f;
            anchorPoint.Y = template.Height % 2 == 0
                ? (float)Math.Round(anchorPoint.Y)
                : (float)Math.Round(anchorPoint.Y + .5f) - .5f;
            anchorPoint.X -= template.Width * .5f;
            anchorPoint.Y -= template.Height * .5f;

            template.Entity.position = anchorPoint * Tile.Size;

            long anchorX = (long)Math.Round(anchorPoint.X);
            long anchorY = (long)Math.Round(anchorPoint.Y);

            var canPlace = tileEntitySystem.CanPlaceTileEntity(template, anchorX, anchorY);

            template.Sprite.setColor(canPlace ? validPlacementColor : invalidPlacementColor);

            // Place tile entity
            if (Input.leftMouseButtonDown && canPlace)
            {
                tileEntitySystem.PlaceTileEntity(template.Clone(), anchorX, anchorY);
            }
        }
    }
}
