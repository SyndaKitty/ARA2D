using System;
using ARA2D.Systems;
using ARA2D.TileEntities;
using ARA2D.WorldGenerators;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;

namespace ARA2D
{
    public class TestScene : Scene
    {
        Texture2D ChunkTextures;
        Texture2D TestEntityTexture;

        WorldLoader worldLoader;
        ChunkMeshGenerator chunkMeshGenerator;
        World world;
        TileEntitySystem tileEntitySystem;

        TestTileEntity tileEntityToPlace;
        Color validPlacementColor = new Color(255, 255, 255, 180);
        Color invalidPlacementColor = new Color(255, 64, 64, 180);

        public TestScene()
        {
            tileEntityToPlace = new TestTileEntity(TestEntityTexture);
            tileEntityToPlace.CreateEntity(this, 0, 0);
        }

        public override void initialize()
        {
            CreateCamera();
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

            // Adjust tileEntityToPlace bounds
            if (Input.isKeyPressed(Keys.Left)) tileEntityToPlace.Width -= 1;
            if (Input.isKeyPressed(Keys.Right)) tileEntityToPlace.Width += 1;

            if (Input.isKeyPressed(Keys.Up)) tileEntityToPlace.Height -= 1;
            if (Input.isKeyPressed(Keys.Down)) tileEntityToPlace.Height += 1;
            
            // Adjust tileEntityToPlace ghost position
            var mousePoint = camera.screenToWorldPoint(Input.mousePosition);
            var anchorPoint = mousePoint /= Tile.Size;
            
            anchorPoint.X = tileEntityToPlace.Width % 2 == 0
                ? (float)Math.Round(anchorPoint.X)
                : (float)Math.Round(anchorPoint.X + .5f) - .5f;
            anchorPoint.Y = tileEntityToPlace.Height % 2 == 0
                ? (float)Math.Round(anchorPoint.Y)
                : (float)Math.Round(anchorPoint.Y + .5f) - .5f;
            anchorPoint.X -= tileEntityToPlace.Width * .5f;
            anchorPoint.Y -= tileEntityToPlace.Height * .5f;

            tileEntityToPlace.Entity.position = anchorPoint * Tile.Size;
            
            long anchorX = (long) Math.Round(anchorPoint.X);
            long anchorY = (long) Math.Round(anchorPoint.Y);

            var canPlace = tileEntitySystem.CanPlaceTileEntity(tileEntityToPlace, anchorX, anchorY);

            tileEntityToPlace.Sprite.setColor(canPlace ? validPlacementColor : invalidPlacementColor);

            // Place tile entity
            if (Input.leftMouseButtonDown && canPlace)
            {
                tileEntitySystem.PlaceTileEntity(tileEntityToPlace.Clone(), anchorX, anchorY);
            }
        }

        public void InitialGeneration()
        {
            worldLoader.Enabled = true;
        }

        void CreateCamera()
        {
            camera.setPosition(new Vector2(-Screen.width * .5f, -Screen.height * .5f));
            camera.maximumZoom = 10f;
            camera.minimumZoom = 1f;
            camera.zoom = 0f;
        }

        void LoadContent()
        {
            ChunkTextures = content.Load<Texture2D>("images/TestGrid2");
            TestEntityTexture = content.Load<Texture2D>("images/TestEntity2");
        }

        void CreateSystems()
        {
            addEntityProcessor(chunkMeshGenerator = new ChunkMeshGenerator(ChunkTextures));
            addEntityProcessor(worldLoader = new WorldLoader(chunkMeshGenerator, 2, 2));
            addEntityProcessor(tileEntitySystem = new TileEntitySystem());
            addEntityProcessor(world = new World(new SandboxGenerator()));
        }
    }
}