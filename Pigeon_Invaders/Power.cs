using System;
using System.Drawing;

namespace Pigeon_Invaders
{
    public class Power
    {
        public const int Size = 20;

        public float X;
        public float Y;

        private float speed = 5f;   // prędkość pocisku
        private float angle;        // kąt ruchu w radianach

        private static readonly Random rand = new Random();

        public Power(float startX, float startY)
        {
            X = startX;
            Y = startY;

            // Losowy kąt odchylenia od pionu (-15° do +15°)
            float degrees = rand.Next(-15, 16);
            angle = degrees * (float)(Math.PI / 180.0);
        }

        // Aktualizacja pozycji pocisku
        public void Move()
        {
            X += (float)(speed * Math.Sin(angle));
            Y -= (float)(speed * Math.Cos(angle));
        }

        public RectangleF Bounds => new RectangleF(X, Y, Size, Size);
    }
}
