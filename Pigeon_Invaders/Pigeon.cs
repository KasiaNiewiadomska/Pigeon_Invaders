using System.Windows.Forms;
using System.Drawing;

namespace Pigeon_Invaders
{
    public class Pigeon
    {
        public PictureBox Picture { get; private set; }
        private int speed = 2;

        public Pigeon(Form parent, int x, int y)
        {
            Picture = new PictureBox();
            Picture.Size = new Size(24, 39);
            Picture.SizeMode = PictureBoxSizeMode.StretchImage;
            Picture.Image = Pigeon_Invaders.Properties.Resources.pigeon; // pełna ścieżka


            Picture.BackColor = Color.FromArgb(1, 0, 0, 0);

            //Picture.BackColor = Color.Transparent;
            Picture.Left = x;
            Picture.Top = y;
            

            parent.Controls.Add(Picture);
            Picture.BringToFront();
        }

        public void Move()
        {
            Picture.Top += speed;
        }

        public bool IsOutOfBounds(Form parent)
        {
            return Picture.Top > parent.ClientSize.Height;
        }

        public void Destroy(Form parent)
        {
            parent.Controls.Remove(Picture);
            Picture.Dispose();
        }
    }
}