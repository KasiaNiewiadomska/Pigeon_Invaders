using System;
using System.Windows.Forms;
using WMPLib;

namespace Pigeon_Invaders
{
    public partial class FormGameEnd : Form
    {
        private FormGameStart startForm;      // referencja do startu
        private WindowsMediaPlayer startPlayer; // odtwarzacz muzyki

        public FormGameEnd(FormGameStart start, WindowsMediaPlayer player)
        {
            InitializeComponent();
            startForm = start;
            startPlayer = player;

            this.BackgroundImage = Pigeon_Invaders.Properties.Resources.background;
            this.BackgroundImageLayout = ImageLayout.Stretch; // dopasowanie do okna

            pictureBoxGameOver.Image = Pigeon_Invaders.Properties.Resources.GameOver;
            pictureBoxGameOver.SizeMode = PictureBoxSizeMode.Zoom; // opcjonalne, ładne dopasowanie
            pictureBoxGameOver.BackColor = Color.Transparent;

        }

        private void buttonPlayAgain_Click(object sender, EventArgs e)
        {
            // Zamykamy okno końcowe
            this.Close();

            // Tworzymy nową instancję FormGameMain, czyli nową grę od początku
            FormGameMain newGame = new FormGameMain(startPlayer, startForm);

            // Pokazujemy ją jako modalne okno
            newGame.ShowDialog();
        }
    }
}