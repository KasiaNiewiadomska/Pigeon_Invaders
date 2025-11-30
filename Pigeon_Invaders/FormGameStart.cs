using System;
using System.Windows.Forms;
using WMPLib;

namespace Pigeon_Invaders
{
    public partial class FormGameStart : Form
    {
        public WindowsMediaPlayer player = new WindowsMediaPlayer(); // publiczne, aby main miał dostęp
        private bool soundOn = true;

        public FormGameStart()
        {
            InitializeComponent();

            this.BackgroundImage = Pigeon_Invaders.Properties.Resources.background;
            this.BackgroundImageLayout = ImageLayout.Stretch; // dopasowanie do okna

            // Ustaw obrazek na pictureBoxPigeonInvaders
            pictureBoxPigeonInvaders.Image = Pigeon_Invaders.Properties.Resources.PigeonInvaders;
            pictureBoxPigeonInvaders.SizeMode = PictureBoxSizeMode.Zoom; // opcjonalne, ładne dopasowanie
            pictureBoxPigeonInvaders.BackColor = Color.Transparent;

            using (var ms = Pigeon_Invaders.Properties.Resources.pigeon_song)
            using (var fs = new System.IO.FileStream(
                       System.IO.Path.Combine(System.IO.Path.GetTempPath(), "pigeon_song.mp3"),
                       System.IO.FileMode.Create, System.IO.FileAccess.Write))
            {
                ms.CopyTo(fs);
            }

            // Teraz ustawiamy URL playera
            string tempPath = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "pigeon_song.mp3");
            player.URL = tempPath;
            //player.URL = @"Pigeon_Invaders\pigeon_song.mp3";
            player.settings.volume = 50;
            player.settings.setMode("loop", true); // zapętlanie
            player.controls.play();
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            // Utwórz nowe okno gry, przekazując player
            FormGameMain game = new FormGameMain(player, this);

            // Ukryj menu startowe
            this.Hide();

            // Pokaż okno gry
            game.Show();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            player.controls.stop();
            this.Close();
        }

        private void buttonMusic_Click(object sender, EventArgs e)
        {
            if (soundOn)
            {
                player.controls.stop();
                buttonMusic.Text = "Music OFF";
                soundOn = false;
            }
            else
            {
                player.controls.play();
                buttonMusic.Text = "Music ON";
                soundOn = true;
            }
        }
    }
}
