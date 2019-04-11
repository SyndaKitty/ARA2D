using System;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.UI;
using System.Collections.Generic;

namespace ARA2D.Systems
{
    public class BuildingMenu : ProcessingSystem
    {
        //List<Texture2D> buildingTextures;
        List<ImageButton> imageButtons;
        IDrawable selectedFrame;
        IDrawable frame;

        // TODO: Pass list of building info
        public BuildingMenu(UICanvas canvas, Texture2D selectedBuildingFrame, Texture2D buildingFrame)
        {
            imageButtons = new List<ImageButton>();
            selectedFrame = new SubtextureDrawable(selectedBuildingFrame);
            frame = new SubtextureDrawable(buildingFrame);
            
            var table = canvas.stage.addElement(new Table());
            table.setFillParent(true);
            var container = new HorizontalGroup(10);

            for (int i = 0; i < 5; i++)
            {
                var imageButton = new ImageButton(frame, frame, selectedFrame);
                container.addElement(imageButton);
                table.add(container);
                imageButtons.Add(imageButton);
            }

            table.add(container).setPadLeft(10).setPadBottom(10);
            table.left().bottom();
        }

        public override void process()
        {
        }
    }
}
