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
        public float Frames = 10;

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

            long x = coords.Cx;
            long y = coords.Cy;
            // Check chunks in spiral fashion
            for (int length = 1, direction = 1; length < 6; length++, direction = -direction)
            {
                for (int i = 0; i < length; i++)
                {
                    x += direction;
                    CheckCoords(new ChunkCoords(x, y));
                }
                for (int i = 0; i < length; i++)
                {
                    y += direction;
                    CheckCoords(new ChunkCoords(x, y));
                }
            }
        }

        void CheckCoords(ChunkCoords coords)
        {
            if (chunkMeshGenerator.ChunkLoaded(coords)) return;
            var entity = scene.createEntity($"ChunkGenerateRequest{coords.Cx},{coords.Cy}");
            entity.addComponent(new PassiveChunkGenerate(coords, true));
        }
    }
}
