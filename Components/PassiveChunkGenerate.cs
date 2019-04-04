using Nez;

namespace ARA2D.Components
{
    public class PassiveChunkGenerate : Component
    {
        public readonly bool Visual;
        public readonly ChunkCoords Coords;

        public PassiveChunkGenerate(ChunkCoords coords, bool visual = true)
        {
            Coords = coords;
            Visual = visual;
        }
    }
}
