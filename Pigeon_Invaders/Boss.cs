using System.Drawing;

namespace Pigeon_Invaders
{
    public class Boss
    {
        public const int Width = 160;
        public const int Height = 160;

        public float X;
        public float Y;
        public int Lives = 10;

        private float speedY = 1.5f;

        public Boss(float x, float y)
        {
            X = x;
            Y = y;
        }

        public void Move()
        {
            Y += speedY;
        }

        public RectangleF Bounds =>
            new RectangleF(X, Y, Width, Height);
    }
}