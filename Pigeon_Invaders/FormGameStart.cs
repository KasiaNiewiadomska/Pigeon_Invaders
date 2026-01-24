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
        
        private bool soundOn = true; //prywatne pole do śledzenia stanu muzyki
        public bool SoundOn => soundOn; //publiczne pole do sprawdzania stanu muzyki
        private string tempPath; // ścieżka tymczasowego pliku mp3

        public FormGameStart()
        {
            InitializeComponent();

            // Ustawienia grafiki
            this.BackgroundImage = Pigeon_Invaders.Properties.Resources.background;
            this.BackgroundImageLayout = ImageLayout.Stretch;

            pictureBoxPigeonInvaders.Image = Pigeon_Invaders.Properties.Resources.PigeonInvaders;
            pictureBoxPigeonInvaders.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxPigeonInvaders.BackColor = Color.Transparent;

            // Przygotowanie pliku mp3 z zasobów
            tempPath = Path.Combine(Path.GetTempPath(), "doveBird.mp3");

            using (var ms = Pigeon_Invaders.Properties.Resources.doveBird) //nazwa zasobu w resources
            using (var fs = new FileStream(tempPath, FileMode.Create, FileAccess.Write))
            {
                ms.CopyTo(fs);
            }

            //Ustawienie playera
            player.URL = tempPath;
            player.settings.volume = 50;
            player.settings.setMode("loop", true);
            player.controls.play();

            //Obsługa zdarzenia zamykania, żeby usunąć tymczasowy plik =======
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
            // zatrzymanie odtwarzacza
            player.controls.stop();

            // usuń starą muzykę główną
            if (!string.IsNullOrEmpty(tempPath) && File.Exists(tempPath))
                File.Delete(tempPath); // usuwa doveBird.mp3

            // jeśli włączyliśmy muzykę Bossa, usuń też tymczasowy plik bossa
            string bossMusicPath = Path.Combine(Path.GetTempPath(), "StMarysbuglecall.mp3");
            if (File.Exists(bossMusicPath))
                File.Delete(bossMusicPath);
        }
    }
}