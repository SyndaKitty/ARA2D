using ARA2D.Systems;
using ARA2D.WorldGenerators;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;

namespace ARA2D
{
    public class TestScene : Scene
    {
        public Texture2D ChunkTextures;
        
        WorldLoader worldLoader;
        ChunkMeshGenerator chunkMeshGenerator;

        public TestScene()
        {
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
            const float CameraSpeed = 400;
            base.update();

            float xInput = (Input.isKeyDown(Keys.A) ? -1 : 0) + (Input.isKeyDown(Keys.D) ? 1 : 0);
            float yInput = (Input.isKeyDown(Keys.W) ? -1 : 0) + (Input.isKeyDown(Keys.S) ? 1 : 0);
            camera.setPosition(camera.position + new Vector2(xInput, yInput) * CameraSpeed * Time.deltaTime);

            float dz = (Input.mouseWheelDelta) * .001f;
            camera.setZoom(camera.zoom + dz);
        }

        public void InitialGeneration()
        {
            worldLoader.Enabled = true;
        }

        void CreateCamera()
        {
            var cameraEntity = createEntity("Camera");
            cameraEntity.addComponent(camera = new Camera());
            camera.setPosition(new Vector2(-Screen.width * .5f, -Screen.height * .5f));

            camera.maximumZoom = 10f;
            camera.minimumZoom = 1f;
            camera.zoom = 0f;
        }

        void LoadContent()
        {
            ChunkTextures = content.Load<Texture2D>("images/TestGrid2");
        }

        void CreateSystems()
        {
            addEntityProcessor(chunkMeshGenerator = new ChunkMeshGenerator(ChunkTextures));
            addEntityProcessor(worldLoader = new WorldLoader(chunkMeshGenerator, 2, 2));
            addEntityProcessor(new World(new SandboxGenerator()));
        }
    }
}