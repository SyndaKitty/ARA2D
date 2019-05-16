using Microsoft.Xna.Framework.Graphics;

namespace MonoGame.Rendering
{
    public class ChunkMesh
    {
        public VertexPositionColorTexture[] Vertices;
        public short[] Indices;
        // TODO: Move TileTextureMap to global component
        public Texture2D TileTextureMap;
    }
}
