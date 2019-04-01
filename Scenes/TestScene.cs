using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;

namespace ARA2D
{
    public class TestScene : Scene, WorldScene
    {
        Game game;
        World world;
        Camera camera;

        public TestScene(Game game)
        {
            this.game = game;
            world = game.world;
            //world.Scene = this;
        }

        public override void initialize()
        {
            base.initialize();
            clearColor = Color.Black;
            camera = new Camera();
            addRenderer(new DefaultRenderer(camera: camera));
            
            setDefaultDesignResolution(1280, 720, SceneResolutionPolicy.ShowAllPixelPerfect);
            var chunk = world.GenerateChunk(new ChunkCoords(0, 0));
            

        }

        public override void update()
        {
            base.update();
        }
    }
}