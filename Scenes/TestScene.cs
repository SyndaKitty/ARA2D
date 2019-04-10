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
        Texture2D ChunkTextures;
        Texture2D TestEntityTexture;
        Texture2D BuildingFrameTexture;
        Texture2D SelectedBuildingFrameTexture;

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
            tileEntityPlacer.SetTemplate(new BasicTileEntity(TestEntityTexture, 1, 1, new Vector2(.5f, .5f)));
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
            buildingMenu.Initialize(BuildingFrameTexture, SelectedBuildingFrameTexture);
        }

        void LoadContent()
        {
            ChunkTextures = content.Load<Texture2D>("images/TestGrid2");
            TestEntityTexture = content.Load<Texture2D>("images/TestEntity2");
            BuildingFrameTexture = content.Load<Texture2D>("UI/BuildingFrame");
            SelectedBuildingFrameTexture = content.Load<Texture2D>("UI/SelectedBuildingFrame");
        }

        void CreateSystems()
        {
            addEntityProcessor(chunkMeshGenerator = new ChunkMeshGenerator(ChunkTextures));
            addEntityProcessor(worldLoader = new WorldLoader(chunkMeshGenerator, 2, 2));
            addEntityProcessor(tileEntitySystem = new TileEntitySystem());
            addEntityProcessor(world = new World(new SandboxGenerator()));
            addEntityProcessor(cameraController = new CameraController(camera));
            addEntityProcessor(tileEntityPlacer = new TileEntityPlacer(tileEntitySystem));
            addEntityProcessor(buildingMenu = new BuildingMenu());
        }
    }
}
