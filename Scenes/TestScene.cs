using ARA2D.Systems;
using Microsoft.Xna.Framework;
using Nez;

namespace ARA2D
{
    public class TestScene : Scene
    {
        Game game;
        readonly World world;

        public TestScene(Game game)
        {
            this.game = game;
            world = game.world;
        }

        public override void initialize()
        {
            CreateCamera();

            addRenderer(new DefaultRenderer(camera: camera));
            clearColor = Color.Black;
            setDefaultDesignResolution(1280, 720, SceneResolutionPolicy.ShowAllPixelPerfect);

            CreateSystems();
        }

        public override void update()
        {
            base.update();
        }

        public void InitialGeneration()
        {
            world.GenerateChunk(new ChunkCoords(0, 0));
        }

        void CreateCamera()
        {
            var cameraEntity = createEntity("Camera");
            cameraEntity.addComponent(camera = new Camera());
            camera.setPosition(new Vector2(-Screen.width / 2, -Screen.height / 2));
            camera.maximumZoom = 32;
            camera.minimumZoom = 1;
            camera.zoom = 1;
        }

        void CreateSystems()
        {
            addEntityProcessor(new ChunkMeshGenerator());
        }
    }
}