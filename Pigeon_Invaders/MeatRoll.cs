using System.Drawing;

namespace Pigeon_Invaders
{
    public class MeatRoll
    {
        public const int Width = 20;
        public const int Height = 20;

        public float X;
        public float Y;

        private float speed = 3.5f;

        public MeatRoll(float x, float y)
        {
            X = x;
            Y = y;
        }

        // gołąbek spada w dół
        public void Move()
        {
            Y += speed;
        }

        public RectangleF Bounds => new RectangleF(X, Y, Width, Height);
    }
}