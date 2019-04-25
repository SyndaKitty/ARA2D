using System;
using ARA2D.Core;
using ARA2D.TileEntities.Components;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Sprites;

namespace ARA2D.TileEntities.Systems
{
    /// <summary>
    /// Moves around the tile entity template and colors according to whether or not it can be placed
    /// </summary>
    public class TemplatePlacementSystem : EntityProcessingSystem
    {
        public Color ValidPlacementColor = new Color(255, 255, 255, 180);
        public Color InvalidPlacementColor = new Color(255, 64, 64, 180);

        public TemplatePlacementSystem() : base(new Matcher().all(typeof(TileEntityTemplate), typeof(Sprite)))
        {
        }

        public override void process(Entity entity)
        {
            var template = entity.getComponent<TileEntityTemplate>();
            var sprite = entity.getComponent<Sprite>();
            var placement = entity.getOrCreateComponent<TileEntityPlacement>();

            // Calculate anchor position
            var mousePoint = scene.camera.screenToWorldPoint(Input.mousePosition);
            var anchorPoint = mousePoint.ToTileSpace();
            var (width, height) = template.Size;

            anchorPoint.X = width % 2L == 0
                ? (float)Math.Round(anchorPoint.X)
                : (float)Math.Round(anchorPoint.X + .5f) - .5f;
            anchorPoint.Y = height % 2 == 0
                ? (float)Math.Round(anchorPoint.Y) 
                : (float)Math.Round(anchorPoint.Y + .5f) - .5f;
            anchorPoint.X -= width * .5f;
            anchorPoint.Y -= height * .5f;

            entity.position = anchorPoint * Tile.Size;
            placement.Anchor = anchorPoint.Round();
            placement.Size = template.Size;
            
            placement.Type = Input.leftMouseButtonDown
                ? TileEntityPlacement.PlacementType.Place
                : TileEntityPlacement.PlacementType.Check;
            placement.TileEntityID = 1; // TODO: Figure out a way to get this from creation system

            // Change color depending on placement results
            sprite.color = (placement.Result == true) ? ValidPlacementColor : InvalidPlacementColor;
        }
    }
}
