using ARA2D.Camera;
using ARA2D.Chunks;
using ARA2D.Commands;
using ARA2D.Commands.Systems;
using ARA2D.Core;
using ARA2D.Movement;
using ARA2D.Movement.Systems;
using ARA2D.Rendering;
using ARA2D.Systems;
using ARA2D.Ticks;
using ARA2D.TileEntities;
using ARA2D.TileEntities.Systems;
using ARA2D.UI;
using ARA2D.WorldGeneration;
using ARA2D.WorldGeneration.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using ScreenSpaceRenderer = ARA2D.Rendering.ScreenSpaceRenderer;

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
        CameraController cameraController;
        TileEntityPlacer tileEntityPlacer;
        BuildingMenu buildingMenu;

        // Components
        GlobalComponentProvider componentProvider;

        // UI
        UICanvas canvas;

        public TestScene()
        {
        }

        public override void initialize()
        {
            //Core.debugRenderEnabled = true;

            var uiCameraEntity = createEntity("UICamera");
            var uiCamera = new Nez.Camera();
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
            LoadGlobalComponents();
            CreateSystems();

            buildingMenu.Initialize();
            worldLoader.Enabled = true;

            // TODO: Take this test code out
            var script = "wait 5";
            var rawScript = createEntity("RawScript");
            rawScript.addComponent(new RawCommandScript(script));
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

        void LoadGlobalComponents()
        {
            componentProvider = new GlobalComponentProvider();

            componentProvider.CacheComponent(new Grid());
            componentProvider.CacheComponent(new CommandRepository());
            componentProvider.CacheComponent(new TickInfo());
            componentProvider.CacheComponent(new MovementRequests());
            componentProvider.CacheComponent(new CommandActions());
        }

        void CreateSystems()
        {
            MoveRequester moveRequester = new MoveRequester(componentProvider);

            addEntityProcessor(new TickProcessor(componentProvider));
            addEntityProcessor(new SandboxGenerator(componentProvider));
            addEntityProcessor(new UICollisionDetector());
            addEntityProcessor(chunkMeshGenerator = new ChunkMeshGenerator(chunkTextures));
            addEntityProcessor(worldLoader = new WorldLoader(componentProvider, 2, 2));
            addEntityProcessor(cameraController = new CameraController(camera));
            addEntityProcessor(buildingMenu = new BuildingMenu(canvas, selectedBuildingFrameTexture, buildingFrameTexture));
            addEntityProcessor(new TemplatePlacementSystem());
            addEntityProcessor(tileEntityPlacer = new TileEntityPlacer(componentProvider));
            addEntityProcessor(new ActionResultsWriter(componentProvider));
            addEntityProcessor(new CommandParser(componentProvider));
            addEntityProcessor(new CommandScriptRunner(componentProvider, moveRequester));
            addEntityProcessor(new ActionRunner(componentProvider));
            addEntityProcessor(new MovementEvaluator(componentProvider));
        }
    }
}
