using System;
using System.Drawing;

namespace Pigeon_Invaders
{
    public class Power
    {
        public const int Size = 20;

        public float X;
        public float Y;

        private float speed = 5f;   // prędkość pocisku (pionowo w górę)

        public Power(float startX, float startY)
        {
            X = startX;
            Y = startY;

        }

        // Aktualizacja pozycji pocisku - tylko do góry
        public void Move()
        {
            Y -= speed;
        }

        public RectangleF Bounds => new RectangleF(X, Y, Size, Size);
    }
}
