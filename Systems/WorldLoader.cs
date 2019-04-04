using ARA2D.Components;
using Microsoft.Xna.Framework;
using Nez;
using System;

namespace ARA2D.Systems
{
    public class WorldLoader : EntityProcessingSystem
    {
        public float Modifier;
        public bool Enabled;
        public float Frames = 1;

        ChunkMeshGenerator chunkMeshGenerator;
        float frameNumber;

        public WorldLoader(ChunkMeshGenerator chunkMeshGenerator, float modifier) : base(new Matcher().all(typeof(Camera)))
        {
            this.chunkMeshGenerator = chunkMeshGenerator;
            Modifier = modifier;
        }

        public override void process(Entity entity)
        {
            if (!Enabled || frameNumber++ < Frames) return;
            frameNumber = 0;

            var cam = entity.getComponent<Camera>();

            var screenCenter = cam.screenToWorldPoint(new Point((int)(Screen.width * .5f), (int)(Screen.height * .5f)));
            var coords = ChunkCoords.FromWorldSpace(screenCenter.X, screenCenter.Y);
            CheckCoords(coords);
            // TODO: Replace spiral pattern with radial expansion
            // Check chunks in spiral fashion
            //for (int length = 1, direction = 1; length < 5; length++, direction = -direction)
            //{
            //    for (int i = 0; i < length; i++)
            //    {
            //        coords.Cx += direction;
            //        CheckCoords(coords);
            //    }
            //    for (int i = 0; i < length; i++)
            //    {
            //        coords.Cy += direction;
            //        CheckCoords(coords);
            //    }
            //}
        }

        void CheckCoords(ChunkCoords coords)
        {
            if (chunkMeshGenerator.ChunkLoaded(coords)) return;
            Console.WriteLine("ChunkGenerateRequest " + coords);
            var entity = scene.createEntity($"ChunkGenerateRequest{coords.Cx},{coords.Cy}");
            entity.addComponent(new PassiveChunkGenerate(coords, true));
        }
    }
}
