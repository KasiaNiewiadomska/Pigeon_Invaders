using System.Windows.Forms;
using System.Drawing;

namespace Pigeon_Invaders
{
    public class Power
    {
        public PictureBox Picture { get; private set; }
        private int speed = 5;       // prędkość pocisku
        private float angle;         // kąt ruchu w radianach



        public Power(Control parent, int startX, int startY)
        {
            Picture = new PictureBox();
            Picture.Size = new Size(20, 20);
            
            Picture.Left = startX;
            Picture.Top = startY;

            Picture.Image = Pigeon_Invaders.Properties.Resources.power;
            Picture.SizeMode = PictureBoxSizeMode.Zoom;


            Picture.BackColor = Color.FromArgb(1, 0, 0, 0);
            //Picture.BackColor = Color.Transparent;


            parent.Controls.Add(Picture);
            Picture.BringToFront();

           

            // Losowy kąt odchylenia od pionu (-15° do +15°)
            Random rand = new Random();
            float degrees = rand.Next(-15, 16);
            angle = degrees * (float)(Math.PI / 180);
        }

        // Metoda aktualizująca pozycję pocisku
        public void Move()
        {
            int deltaX = (int)(speed * Math.Sin(angle));
            int deltaY = (int)(-speed * Math.Cos(angle));

            Picture.Left += deltaX;
            Picture.Top += deltaY;
        }

        // Sprawdzenie czy pocisk wyszedł poza formę
        public bool IsOutOfBounds(Control parent)
        {
            return Picture.Top + Picture.Height < 0 || Picture.Left < 0 || Picture.Left > parent.Width;
        }

        // Usuwanie pocisku
        public void Destroy(Control parent)
        {
            parent.Controls.Remove(Picture);
            Picture.Dispose();
        }
    }
}