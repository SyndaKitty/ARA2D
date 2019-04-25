using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;
using ARA2D.Chunks;
using ARA2D.Core;
using ARA2D.WorldGeneration.Components;

namespace ARA2D.WorldGeneration
{
    public class WorldLoader : EntityProcessingSystem
    {
        // TODO: True ECS refactor
        public bool Enabled;
        public float Frames = 20;
        
        int maxX;
        public int MaxX
        {
            get => maxX;
            set
            {
                if (maxX == value) return;
                maxX = value;
                CalcOffsetPoints();
            }
        }

        int maxY;
        public int MaxY
        {
            get => maxY;
            set
            {
                if (maxY == value) return;
                maxY = value;
                CalcOffsetPoints();
            }
        }

        readonly IComponentProvider componentProvider;
        List<OffsetPoint> offsetPoints;
        float frameNumber;

        public WorldLoader(IComponentProvider componentProvider, int maxX, int maxY) : base(new Matcher().all(typeof(Nez.Camera)))
        {
            this.componentProvider = componentProvider;
            this.maxX = maxX;
            this.maxY = maxY;
            CalcOffsetPoints();
        }

        public override void process(Entity entity)
        {
            if (!Enabled || frameNumber++ < Frames) return;
            frameNumber = 0;
            var cam = entity.getComponent<Nez.Camera>();
            var screenCenter =
                cam.screenToWorldPoint(new Point((int) (Screen.width * .5f), (int) (Screen.height * .5f)));
            var coords = ChunkCoords.FromWorldSpace(screenCenter.X, screenCenter.Y);

            foreach (var offsetPoint in offsetPoints)
            {
                var offsetCoords = new ChunkCoords(coords.Cx + offsetPoint.Ox, coords.Cy + offsetPoint.Oy);
                scene.createEntity("ChunkGenerationRequest").addComponent(new ChunkGenerationRequest(offsetCoords));
            }
        }

        void CalcOffsetPoints()
        {
            if (offsetPoints == null) offsetPoints = new List<OffsetPoint>();

            int tier = Math.Max(MaxX, MaxY);
            offsetPoints.Clear();
            for (int y = -tier; y <= tier; y++)
            {
                if (Math.Abs(y) > maxY) continue;
                for (int x = -tier; x <= tier; x++)
                {
                    if (Math.Abs(x) > MaxX) continue;
                    offsetPoints.Add(new OffsetPoint(x, y));
                }
            }
            offsetPoints.Sort();
        }
    }

    public struct OffsetPoint : IComparable<OffsetPoint>
    {
        public int Ox;
        public int Oy;
        public float distance;

        public OffsetPoint(int ox, int oy)
        {
            Ox = ox;
            Oy = oy;
            distance = (float)Math.Sqrt(Ox * Ox + Oy * Oy);
        }

        public int CompareTo(OffsetPoint other)
        {
            return distance.CompareTo(other.distance);
        }
    }
}