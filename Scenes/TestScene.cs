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
            Core.debugRenderEnabled = true;
            addRenderer(new DefaultRenderer(camera: camera));
            clearColor = Color.Black;
            setDefaultDesignResolution(1920, 1080, SceneResolutionPolicy.ShowAllPixelPerfect);

            LoadContent();
            CreateSystems();
        }

        public override void update()
        {
            const float CameraSpeed = 100;
            base.update();

            float xInput = (Input.isKeyDown(Keys.A) ? -1 : 0) + (Input.isKeyDown(Keys.D) ? 1 : 0);
            float yInput = (Input.isKeyDown(Keys.W) ? -1 : 0) + (Input.isKeyDown(Keys.S) ? 1 : 0);
            camera.setPosition(camera.position + new Vector2(xInput, yInput) * CameraSpeed * Time.deltaTime);

            float dz = (Input.mouseWheelDelta) * .1f;
            camera.setZoom(camera.zoom + dz);
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

            camera.maximumZoom = 10f;
            camera.minimumZoom = 1f;
            camera.zoom = 0f;
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