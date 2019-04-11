using ARA2D.Components;
using ARA2D.Systems;
using ARA2D.TileEntities;
using ARA2D.WorldGenerators;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using static ARA2D.Renderers.ScreenSpaceRenderer;
using ScreenSpaceRenderer = ARA2D.Renderers.ScreenSpaceRenderer;

namespace ARA2D
{
    public class TestScene : Scene
    {
        // Content
        Texture2D chunkTextures;
        Texture2D testEntityTexture;
        Texture2D buildingFrameTexture;
        Texture2D selectedBuildingFrameTexture;

        // Systems
        WorldLoader worldLoader;
        ChunkMeshGenerator chunkMeshGenerator;
        World world;
        TileEntitySystem tileEntitySystem;
        CameraController cameraController;
        TileEntityPlacer tileEntityPlacer;
        BuildingMenu buildingMenu;

        // UI
        UICanvas canvas;

        public TestScene()
        {
            tileEntityPlacer.SetTemplate(new BasicTileEntity(testEntityTexture, 1, 1, Vector2.One));
        }

        public override void initialize()
        {
            //Core.debugRenderEnabled = true;

            var uiCameraEntity = createEntity("UICamera");
            var uiCamera = new Camera();
            uiCameraEntity.addComponent(new ScreenSpace());
            uiCameraEntity.addComponent(uiCamera);

            var uiRoot = createEntity("UIRoot");
            canvas = new UICanvas { renderLayer = Layers.ScreenSpace, isFullScreen = true };
            uiRoot.addComponent(canvas);

            addRenderer(new ScreenSpaceRenderer(uiCamera, 100, Layers.ScreenSpace));
            addRenderer(new RenderLayerExcludeRenderer(100, Layers.ScreenSpace));

            clearColor = Color.Black;
            setDefaultDesignResolution(1920, 1080, SceneResolutionPolicy.ShowAllPixelPerfect);

            LoadContent();
            CreateSystems();

            worldLoader.Enabled = true;
        }

        public override void update()
        {
            base.update();
        }

        void LoadContent()
        {
            chunkTextures = content.Load<Texture2D>("images/TestGrid2");
            testEntityTexture = content.Load<Texture2D>("images/TestEntity2");
            buildingFrameTexture = content.Load<Texture2D>("UI/BuildingFrame");
            selectedBuildingFrameTexture = content.Load<Texture2D>("UI/SelectedBuildingFrame");
        }

        void CreateSystems()
        {
            addEntityProcessor(new UICollisionDetector());
            addEntityProcessor(chunkMeshGenerator = new ChunkMeshGenerator(chunkTextures));
            addEntityProcessor(worldLoader = new WorldLoader(chunkMeshGenerator, 2, 2));
            addEntityProcessor(tileEntitySystem = new TileEntitySystem());
            addEntityProcessor(world = new World(new SandboxGenerator()));
            addEntityProcessor(cameraController = new CameraController(camera));
            addEntityProcessor(tileEntityPlacer = new TileEntityPlacer(tileEntitySystem));
            addEntityProcessor(buildingMenu = new BuildingMenu(canvas, selectedBuildingFrameTexture, buildingFrameTexture));
        }
    }
}
