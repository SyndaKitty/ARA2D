using ARA2D.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;

namespace ARA2D
{
    public class TestScene : Scene
    {
        public Texture2D ChunkTextures;
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

            LoadContent();
            CreateSystems();
        }

        public override void update()
        {
            const float CameraSpeed = 10;
            base.update();

            float xInput = (Input.isKeyDown(Keys.A) ? -1 : 0) + (Input.isKeyDown(Keys.D) ? 1 : 0);
            float yInput = (Input.isKeyDown(Keys.W) ? -1 : 0) + (Input.isKeyDown(Keys.S) ? 1 : 0);
            camera.setPosition(camera.position + new Vector2(xInput, yInput) * CameraSpeed * Time.deltaTime);
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

        void LoadContent()
        {
            ChunkTextures = content.Load<Texture2D>("images/TestGrid");
        }

        void CreateSystems()
        {
            addEntityProcessor(new ChunkMeshGenerator(ChunkTextures));
        }
    }
}