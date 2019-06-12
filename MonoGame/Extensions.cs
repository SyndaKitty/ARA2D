using Microsoft.Xna.Framework;
using System.Numerics;
using Vector4 = System.Numerics.Vector4;

namespace MonoGame
{
    public static class Extensions
    {
        public static Matrix Convert(this Matrix4x4 matrix)
        {
            return new Matrix(matrix.M11, matrix.M12, matrix.M13, matrix.M14, matrix.M21, matrix.M22, matrix.M23, matrix.M24, matrix.M31, matrix.M32, matrix.M33, matrix.M34, matrix.M41, matrix.M42, matrix.M43, matrix.M44);
        }

        public static Color Convert(this Vector4 vector)
        {
            return new Color(vector.X, vector.Y, vector.Z, vector.W);
        }
    }
}
