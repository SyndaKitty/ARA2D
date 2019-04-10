using ARA2D.Systems;
using ARA2D.TileEntities;
using ARA2D.WorldGenerators;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.UI;

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
        
        public TestScene()
        {
            tileEntityPlacer.SetTemplate(new BasicTileEntity(testEntityTexture, 1, 1, Vector2.One));
        }

        public override void initialize()
        {
            //Core.debugRenderEnabled = true;
            addRenderer(new DefaultRenderer(camera: camera));
            clearColor = Color.Black;
            setDefaultDesignResolution(1920, 1080, SceneResolutionPolicy.ShowAllPixelPerfect);

            LoadContent();
            CreateSystems();
        }

        public override void update()
        {
            base.update();
        }

        public void InitialGeneration()
        {
            worldLoader.Enabled = true;
            buildingMenu.Initialize(selectedBuildingFrameTexture, buildingFrameTexture);
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
            addEntityProcessor(chunkMeshGenerator = new ChunkMeshGenerator(chunkTextures));
            addEntityProcessor(worldLoader = new WorldLoader(chunkMeshGenerator, 2, 2));
            addEntityProcessor(tileEntitySystem = new TileEntitySystem());
            addEntityProcessor(world = new World(new SandboxGenerator()));
            addEntityProcessor(cameraController = new CameraController(camera));
            addEntityProcessor(tileEntityPlacer = new TileEntityPlacer(tileEntitySystem));
            addEntityProcessor(buildingMenu = new BuildingMenu());
        }
    }
}
