using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.UI;
using System.Collections.Generic;
using ARA2D.Core;
using ARA2D.TileEntities.Components;
using Microsoft.Xna.Framework;
using Nez.Sprites;
using IDrawable = Nez.UI.IDrawable;

namespace ARA2D.UI
{
    /// <summary>
    /// Creates building menu UI, responds to clicks on UI by updating the TileEntityTemplate component
    /// TODO: This system needs a lot of work to become more ECS friendly, but we need more base functionality
    /// like UI collision detection before that can become possible
    /// </summary>
    public class BuildingMenu : EntityProcessingSystem
    {
        // TODO: True ECS refactor
        //List<Texture2D> buildingTextures;
        readonly List<ImageButton> imageButtons;
        readonly List<TileEntityTemplate> templates;
        readonly IDrawable selectedFrame;
        readonly IDrawable frame;
        TileEntityTemplate template;
        Sprite templateSprite;

        public BuildingMenu(UICanvas canvas, Texture2D selectedBuildingFrame, Texture2D buildingFrame) : base(new Matcher().all(typeof(TileEntityTemplate), typeof(Sprite)))
        {
            // Create UI
            imageButtons = new List<ImageButton>();
            selectedFrame = new SubtextureDrawable(selectedBuildingFrame);
            frame = new SubtextureDrawable(buildingFrame);

            var table = canvas.stage.addElement(new Table());
            table.setFillParent(true);
            var container = new HorizontalGroup(10);

            templates = new List<TileEntityTemplate>();

            var texture = Nez.Core.content.Load<Texture2D>("images/TestEntity2");
            for (int i = 0; i < 5; i++)
            {
                templates.Add(new TileEntityTemplate(texture, new IntVector2(i+1, i+1), new Vector2(i + 1, i + 1)));
                var imageButton = new ImageButton(frame, frame, selectedFrame);
                imageButton.onClicked += ImageButton_onClicked;
                container.addElement(imageButton);
                table.add(container);
                imageButtons.Add(imageButton);
            }

            table.add(container).setPadLeft(10).setPadBottom(10);
            table.left().bottom();
        }

        public void Initialize()
        {
            // TODO: Replace this with more ECS centric code. We should just be creating entities and components here, no caching.
            var tet = scene.createEntity("TileEntityTemplate");

            tet.addComponent(template = new TileEntityTemplate(templates[0].Texture, templates[0].Size, templates[0].Size.ToVector2()));
            tet.addComponent(templateSprite = new Sprite(template.Texture));
            templateSprite.origin = Vector2.Zero;
        }

        // TODO: Handle UI and click detection/handling with ECS
        void ImageButton_onClicked(Button obj)
        {
            for (int i = 0; i < imageButtons.Count; i++)
            {
                if (imageButtons[i] == obj)
                {
                    template.Texture = templates[i].Texture;
                    template.Size = templates[i].Size;
                    template.Scale = templates[i].Scale;
                }
                else
                {
                    imageButtons[i].isChecked = false;
                }
            }
        }

        public override void process(Entity entity)
        {
            var template = entity.getComponent<TileEntityTemplate>();
            var sprite = entity.getComponent<Sprite>();

            sprite.transform.scale = template.Size.ToVector2();
        }
    }
}
