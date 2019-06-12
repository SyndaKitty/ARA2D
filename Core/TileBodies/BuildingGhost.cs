using System.Numerics;

namespace Core.TileBodies
{
    public class BuildingGhost
    {
        public static readonly Vector4 Valid = new Vector4(.6f, 1f, .6f, .4f);
        public static readonly Vector4 Invalid = new Vector4(1f, .3f, .3f, .4f);
        public static readonly Vector4 Invisible = new Vector4(1f, 1f, 1f, 0f);

        public Vector4 Color;

        public BuildingGhost()
        {
            Color = Invisible;
        }
    }
}
