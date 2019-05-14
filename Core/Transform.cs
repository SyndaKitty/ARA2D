using Microsoft.Xna.Framework;

namespace Core
{
    public class Transform
    {
        public float X { get; private set; }
        public float Y { get; private set; }

        public float ScaleX { get; private set; }
        public float ScaleY { get; private set; }
        
        public bool Dirty;
        public Matrix Matrix;

        public Transform(Vector2 position, Vector2 scale) : this(position.X, position.Y, scale.X, scale.Y)
        {
        }

        public Transform(Vector2 position) : this(position, Vector2.One)
        {
        }

        public Transform(float x, float y, float scaleX = 1, float scaleY = 1)
        {
            X = x;
            Y = y;
            ScaleX = scaleX;
            ScaleY = scaleY;
            Dirty = true;
        }

        public void SetPosition(Vector2 position)
        {
            SetPosition(position.X, position.Y);
        }

        public void SetPosition(float x, float y)
        {
            X = x;
            Y = y;
            Dirty = true;
        }

        public void SetX(float x)
        {
            X = x;
            Dirty = true;
        }

        public void SetY(float y)
        {
            Y = y;
            Dirty = true;
        }
    }
}
