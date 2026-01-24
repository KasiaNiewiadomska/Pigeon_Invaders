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

        private float speedY = 1.5f;   // ruch w dół, prędkość spadania
        private float speedX = 2.5f;   // ruch prawo–lewo, prędkość boczna


        private float leftBound;
        private float rightBound;

        private float stopY = 100f;        // wysokość, na której boss się zatrzymuje

        private int stopTicks = 20;        // ile ticków stoi - jak długo stoi
        
        private int currentStopTicks = 0;
        private bool isStopped = false;

        private int horizontalTicks = 0;
        private int maxHorizontalTicks = 660; // ruch prawo-lewo, jak długo chodzi bokiem

        private bool movingDownAgain = false;

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

            // Ruch poziomy
            if (!movingDownAgain)
            {
                //ruch poziomy
                X += speedX;
                horizontalTicks++;

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

                // po czasie - znowu spada
                if (horizontalTicks >= maxHorizontalTicks)
                {
                    movingDownAgain = true;
                }

                return;
            }

            // Ponowny spadek
            Y += speedY;
        }

        public RectangleF Bounds => new RectangleF(X, Y, Width, Height);

    }
}