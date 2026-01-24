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

        private float speedY = 1.5f;   // ruch w dół
        private float speedX = 2.5f;   // ruch prawo–lewo

        private float leftBound;
        private float rightBound;

        private float stopY = 100f;        // wysokość, na której boss się zatrzymuje
        private int stopTicks = 50;        // ile ticków stoi (50 = ~1s przy 20ms)
        private int currentStopTicks = 0;
        private bool isStopped = false;

        public Boss(float x, float y, float leftBound, float rightBound)
        {
            X = x;
            Y = y;
            this.leftBound = leftBound;
            this.rightBound = rightBound;
        }

        public void Move()
        {

            // Zjazd z góry
            if (!isStopped && Y < stopY)
            {
                Y += speedY;
                return;
            }

            // Pauza po dojechaniu
            if (!isStopped && Y >= stopY)
            {
                isStopped = true;
                currentStopTicks = 0;
                return;
            }

            if (isStopped && currentStopTicks < stopTicks)
            {
                currentStopTicks++;
                return; // stoi w miejscu
            }

            // ruch poziomy
            X += speedX;

            // odbicie od lewej granicy
            if (X <= leftBound)
            {
                X = leftBound;
                speedX = -speedX;
            }

            // odbicie od prawej granicy
            if (X + Width >= rightBound)
            {
                X = rightBound - Width;
                speedX = -speedX;
            }
        }

        public RectangleF Bounds =>
           new RectangleF(X, Y, Width, Height);
    }
}