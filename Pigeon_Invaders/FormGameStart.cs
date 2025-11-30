using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using WMPLib;

namespace Pigeon_Invaders
{
    public partial class FormGameStart : Form
    {
        public WindowsMediaPlayer player = new WindowsMediaPlayer(); // publiczne, aby main miał dostęp
        private bool soundOn = true;
        private string tempPath; // ścieżka tymczasowego pliku mp3

        public FormGameStart()
        {
            InitializeComponent();

            // ======= Ustawienia grafiki =======
            this.BackgroundImage = Pigeon_Invaders.Properties.Resources.background;
            this.BackgroundImageLayout = ImageLayout.Stretch;

            pictureBoxPigeonInvaders.Image = Pigeon_Invaders.Properties.Resources.PigeonInvaders;
            pictureBoxPigeonInvaders.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxPigeonInvaders.BackColor = Color.Transparent;

            // ======= MUZYKA Z ZASOBÓW =======
            tempPath = Path.Combine(Path.GetTempPath(), "pigeon_song.mp3");

            // Odczyt strumienia z zasobu i zapis do pliku tymczasowego
            using (var stream = Properties.Resources.pigeon_song) // UnmanagedMemoryStream
            using (var fileStream = new FileStream(tempPath, FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(fileStream);
            }

            player.URL = tempPath;
            player.settings.volume = 50;
            player.settings.setMode("loop", true);
            player.controls.play();

            // Obsługa zdarzenia zamykania, żeby usunąć tymczasowy plik
            this.FormClosing += FormGameStart_FormClosing;
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            FormGameMain game = new FormGameMain(player, this);
            this.Hide();
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

        private void FormGameStart_FormClosing(object sender, FormClosingEventArgs e)
        {
            player.controls.stop();
            if (File.Exists(tempPath))
                File.Delete(tempPath); // usuwamy tymczasowy plik mp3
        }
    }
}