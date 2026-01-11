using System;
using System.Drawing;

namespace Pigeon_Invaders
{
    public class Pigeon
    {
        public const float Width = 28.8f;
        public const float Height = 46.8f;

        public float X;
        public float Y;

        private float vx; // pozioma prędkość
        private float vy; // pionowa prędkość
        private float leftBound;
        private float rightBound;
        private static readonly Random rand = new Random();

        // KONSTRUKTOR LEVEL2 – odbijanie od ścian i losowy kąt
        public Pigeon(float x, float y, float leftBound, float rightBound)
        {
            X = x;
            Y = y;
            this.leftBound = leftBound;
            this.rightBound = rightBound;

            // losowy kąt od -60° do +60° w dół
            float angleDeg = (float)(rand.NextDouble() * 120 - 60);
            float angleRad = angleDeg * (float)Math.PI / 180f;
            float speed = 1.8f;

            vx = (float)Math.Sin(angleRad) * speed;
            vy = (float)Math.Cos(angleRad) * speed;
        }

        // KONSTRUKTOR LEVEL1 – lecą prosto w dół, brak odbić
        public Pigeon(float x, float y)
        {
            X = x;
            Y = y;
            vx = 0;
            vy = 1.8f;

            leftBound = 0;
            rightBound = float.MaxValue; // brak odbić
        }

        public void Move()
        {
            X += vx;
            Y += vy;

            // odbicie od granic rzędu (tylko Level2)
            if (X <= leftBound)
            {
                X = leftBound;
                vx = -vx;
            }
            if (X + Width >= rightBound)
            {
                X = rightBound - Width;
                vx = -vx;
            }
        }

        public RectangleF Bounds => new RectangleF(X, Y, Width, Height);
    }
}
