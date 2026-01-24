using System.Drawing;

namespace Pigeon_Invaders
{
    public class Boss
    {
        public const int Width = 140;
        public const int Height = 140;

        public float X;
        public float Y;
        public int Lives = 10;

        private float speedY = 1.5f;

        public Boss(float x, float y)
        {
            X = x;
            Y = y;
            this.leftBound = leftBound;
            this.rightBound = rightBound;
        }

        public void Move()
        {
            Y += speedY;
        }

        public RectangleF Bounds =>
            new RectangleF(X, Y, Width, Height);
    }
}