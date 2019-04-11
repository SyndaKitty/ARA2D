using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.UI;
using System.Collections.Generic;
using ARA2D.TileEntities;
using IDrawable = Nez.UI.IDrawable;

namespace ARA2D.Systems
{
    public class BuildingMenu : ProcessingSystem
    {
        //List<Texture2D> buildingTextures;
        List<ImageButton> imageButtons;
        List<ITileEntity> tileEntities;
        IDrawable selectedFrame;
        IDrawable frame;

        public BuildingMenu(UICanvas canvas, Texture2D selectedBuildingFrame, Texture2D buildingFrame)
        {
            // TODO: Pass list of building info
            var texture = Core.content.Load<Texture2D>("images/TestEntity2");
            tileEntities = new List<ITileEntity>();
            for (int i = 0; i < 5; i++)
            {
                tileEntities.Add(new BasicTileEntity(texture, i + 1, i + 1));
            }

            imageButtons = new List<ImageButton>();
            selectedFrame = new SubtextureDrawable(selectedBuildingFrame);
            frame = new SubtextureDrawable(buildingFrame);
            
            var table = canvas.stage.addElement(new Table());
            table.setFillParent(true);
            var container = new HorizontalGroup(10);

            for (int i = 0; i < tileEntities.Count; i++)
            {
                var imageButton = new ImageButton(frame, frame, selectedFrame);
                imageButton.onClicked += ImageButton_onClicked;
                container.addElement(imageButton);
                table.add(container);
                imageButtons.Add(imageButton);
            }

            table.add(container).setPadLeft(10).setPadBottom(10);
            table.left().bottom();
        }

        void ImageButton_onClicked(Button obj)
        {
            // TODO: Use a better way to find the corresponding button. Dictionary? Maybe I shouldn't prematurely optimize..
            for (int i = 0; i < imageButtons.Count; i++)
            {
                if (imageButtons[i] == obj) Events.TriggerBuildMenuItemClick(tileEntities[i]);
            }
        }

        public override void process()
        {
        }
    }
}
