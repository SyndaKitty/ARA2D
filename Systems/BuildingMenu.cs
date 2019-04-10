using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.UI;
using System.Collections.Generic;

namespace ARA2D.Systems
{
    public class BuildingMenu : ProcessingSystem
    {
        //List<Texture2D> buildingTextures;
        //List<>

        // TODO: Pass list of building info
        public BuildingMenu()
        {
        }

        public void Initialize(Texture2D selectedBuildingFrame, Texture2D buildingFrame)
        {
            var uiRoot = scene.createEntity("UIRoot");
            var uiCanvas = new UICanvas();
            uiCanvas.renderLayer = -1;
            uiCanvas.isFullScreen = true;
            uiRoot.addComponent(uiCanvas);

            var table = uiCanvas.stage.addElement(new Table());
            table.setFillParent(true);

            // TODO also draw image of building
            for (int i = 0; i < 10; i++)
            {
                var container = new Container();
                var imageButton = new ImageButton(new SubtextureDrawable(selectedBuildingFrame));
                imageButton.pad(10);
                container.setElement(imageButton);
                table.add(container);
            }
            
            table.left().bottom();
        }

        public override void process()
        {
        }
    }
}
