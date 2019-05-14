using System.Numerics;

namespace Core
{
    public class Transform
    {
        #region Fields
        Vector2 position;
        Vector2 scale;

        public bool Dirty;
        public Matrix4x4 Matrix; // Calculated
        #endregion

        #region Properties
        public Vector2 Position
        {
            get => position;
            set
            {
                position = value;
                Dirty = true;
            }
        }

        public float X
        {
            get => position.X;
            set
            {
                position.X = value;
                Dirty = true;
            }
        }

        public float Y
        {
            get => position.Y;
            set
            {
                position.Y = value;
                Dirty = true;
            }
        }

        public Vector2 Scale
        {
            get => scale;
            set
            {
                scale = value;
                Dirty = true;
            }
        }

        public float ScaleX
        {
            get => scale.X;
            set
            {
                scale.X = value;
                Dirty = true;
            }
        }

        public float ScaleY
        {
            get => scale.Y;
            set
            {
                scale.Y = value;
                Dirty = true;
            }
        }
        #endregion

        public Transform(Vector2 position, Vector2 scale) : this(position.X, position.Y, scale.X, scale.Y)
        {
        }

        public Transform(Vector2 position) : this(position, Vector2.One)
        {
        }

        public Transform(float x, float y, float scaleX = 1, float scaleY = 1)
        {
            position = new Vector2(x, y);
            Scale = new Vector2(scaleX, scaleY);
            Dirty = true;
        }
    }
}
