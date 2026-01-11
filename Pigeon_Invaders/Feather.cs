using System;
using System.Drawing;

namespace Pigeon_Invaders
{
    public class Feather
    {
        public const int Width = 15;
        public const int Height = 25;

        public float X;
        public float Y;

        private float vx;   // prędkość pozioma
        private float vy;   // prędkość pionowa

        // granice odbicia
        private float leftBound;
        private float rightBound;

        private const float Speed = 3.5f;

        public Feather(float x, float y, float leftBound, float rightBound)
        {
            X = x;
            Y = y;

            this.leftBound = leftBound;
            this.rightBound = rightBound;

            // losowy kąt od -60° do +60°
            float angleDeg = (float)(new Random().NextDouble() * 120 - 60);
            float angleRad = angleDeg * (float)Math.PI / 180f;

            vx = (float)Math.Sin(angleRad) * Speed;
            vy = (float)Math.Cos(angleRad) * Speed;
        }

        public void Move()
        {
            X += vx;
            Y += vy;

            // odbicie od lewej ściany
            if (X <= leftBound)
            {
                X = leftBound;
                vx = -vx;
            }

            // odbicie od prawej ściany
            if (X + Width >= rightBound)
            {
                X = rightBound - Width;
                vx = -vx;
            }
        }

        public RectangleF Bounds => new RectangleF(X, Y, Width, Height);
    }
}