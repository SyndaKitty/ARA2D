using ARA2D.Systems;
using Microsoft.Xna.Framework;
using Nez;

namespace ARA2D
{
    public class TestScene : Scene, WorldScene
    {
        Game game;
        World world;

        public TestScene(Game game)
        {
            this.game = game;
            world = game.world;
            world.worldScene = this;
        }

        public override void initialize()
        {
            //var cameraEntity = createEntity("Camera");
            //cameraEntity.addComponent(camera = new Camera());

            addRenderer(new DefaultRenderer(/*camera: camera*/));
            clearColor = Color.Black;
            setDefaultDesignResolution(1280, 720, SceneResolutionPolicy.ShowAllPixelPerfect);

            addEntityProcessor(new ChunkMeshGenerator());
        }

        public override void update()
        {
            base.update();
        }

        public void InitialGeneration()
        {
            world.GenerateChunk(new ChunkCoords(0, 0));
        }

        public void ChunkMeshGenerated(Mesh m)
        {
            var chunkMeshEntity = createEntity("chunk");
            chunkMeshEntity.addComponent(m);
        }
    }
}