using System.Drawing;

namespace Pigeon_Invaders
{
    public class Pigeon
    {
        public const int Width = 24;
        public const int Height = 39;

        public float X;
        public float Y;

        private float speed = 2f;

        public Pigeon(float x, float y)
        {
            X = x;
            Y = y;
        }

        public void Move()
        {
            Y += speed;
        }

        public RectangleF Bounds => new RectangleF(X, Y, Width, Height);
    }
}
