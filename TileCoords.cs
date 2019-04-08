using Microsoft.Xna.Framework;

namespace ARA2D
{
    public static class TileCoords
    {
        public static Vector2 FromWorldSpace(float x, float y)
        {
            return new Vector2(x / Tile.Size, y / Tile.Size);
        }

        public static Vector2 ToWorldSpace (float tx, float ty)
        {
            return new Vector2(tx * Tile.Size, ty * Tile.Size);
        }
    }
}
