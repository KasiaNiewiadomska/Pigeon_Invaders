using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace Pigeon_Invaders
{
    public partial class FormGameWin : Form
    {

        private FormGameStart startForm;      // referencja do startu
        private WindowsMediaPlayer startPlayer; // odtwarzacz muzyki


        public FormGameWin(FormGameStart start, WindowsMediaPlayer player)
        {
            InitializeComponent();

            startForm = start;
            startPlayer = player;

            this.BackgroundImage = Pigeon_Invaders.Properties.Resources.background;
            this.BackgroundImageLayout = ImageLayout.Stretch; // dopasowanie do okna

            pictureBoxYouWin.Image = Pigeon_Invaders.Properties.Resources.you_win;
            pictureBoxYouWin.SizeMode = PictureBoxSizeMode.Zoom; // opcjonalne, ładne dopasowanie
            pictureBoxYouWin.BackColor = Color.Transparent;
        }

        private void buttonPlayAgain_Click(object sender, EventArgs e)
        {
            this.Close(); // zamknij FormGameWin
            FormGameMain newGame = new FormGameMain(startPlayer, startForm);
            newGame.Show(); // nie ShowDialog

        }
    }
}


