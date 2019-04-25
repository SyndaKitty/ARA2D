using System;
using Microsoft.Xna.Framework;

namespace ARA2D.Core
{
    public static class Vector2Extensions
    {
        public static Vector2 ToTileSpace(this Vector2 vec)
        {
            return vec / Tile.Size;
        }

        public static IntVector2 Floor(this Vector2 vec)
        {
            return new IntVector2((long)Math.Floor(vec.X), (long)Math.Floor(vec.Y));
        }

        public static IntVector2 Round(this Vector2 vec)
        {
            return new IntVector2((long)Math.Round(vec.X), (long)Math.Round(vec.Y));
        }
    }
}
