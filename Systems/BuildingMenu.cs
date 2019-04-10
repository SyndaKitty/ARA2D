using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.UI;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Microsoft.Xna.Framework;
using IDrawable = Nez.UI.IDrawable;

namespace ARA2D.Systems
{
    public class BuildingMenu : EntityProcessingSystem
    {
        //List<Texture2D> buildingTextures;
        List<ImageButton> imageButtons;
        Entity uiRoot;
        IDrawable selectedFrame;
        IDrawable frame;

        // TODO: Pass list of building info
        public BuildingMenu() : base(new Matcher().all(typeof(Camera)))
        {
            imageButtons = new List<ImageButton>();
        }

        public void Initialize(Texture2D selectedBuildingFrame, Texture2D buildingFrame)
        {
            selectedFrame = new SubtextureDrawable(selectedBuildingFrame);
            frame = new SubtextureDrawable(buildingFrame);

            uiRoot = scene.createEntity("UIRoot");
            var uiCanvas = new UICanvas();
            uiCanvas.renderLayer = -100;
            uiCanvas.isFullScreen = true;
            uiRoot.addComponent(uiCanvas);

            var table = uiCanvas.stage.addElement(new Table());
            table.setFillParent(true);

            for (int i = 0; i < 10; i++)
            {
                var container = new Container();
                var imageButton = new ImageButton(frame, frame, selectedFrame);
                imageButton.padLeft(10);
                imageButton.padBottom(10);
                container.setElement(imageButton);
                table.add(container);
                imageButtons.Add(imageButton);
            }

            table.left().bottom();
        }

        public override void process(Entity entity)
        {
            // Move uiRoot to camera position to stop it from culling
            uiRoot.position = entity.position;
        }
    }
}
