using Microsoft.Xna.Framework;
using Nez;

namespace ARA2D
{
    public class TestScene : Scene
    {
        Game game;

        public TestScene(Game game)
        {
            this.game = game;
        }

        public override void initialize()
        {
            base.initialize();
            clearColor = Color.Black;
            addRenderer(new DefaultRenderer());
            setDefaultDesignResolution(1280, 720, SceneResolutionPolicy.ShowAllPixelPerfect);
        }

        public override void update()
        {
            base.update();
        }
    }
}