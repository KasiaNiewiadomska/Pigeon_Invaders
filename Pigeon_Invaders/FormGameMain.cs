using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WMPLib;

namespace Pigeon_Invaders
{
    public partial class FormGameMain : Form
    {
        private WindowsMediaPlayer startPlayer;
        private FormGameStart startForm;

        private List<Power> bullets = new List<Power>();
        private List<Pigeon> pigeons = new List<Pigeon>();

        private int rowTickCounter = 0;
        private int rowTickInterval = 50; // co ile ticków tworzymy nowy rząd

        private int points = 0; // licznik punktów
        private int hearts = 5; // liczba serc na początku gry

        private int energyPowerPoints = 1000;
        private int weaponPoints = 1000;
        private int earlyPigeonPoints = 1000;



        public FormGameMain(WindowsMediaPlayer player, FormGameStart start)
        {

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
              ControlStyles.AllPaintingInWmPaint |
              ControlStyles.UserPaint, true);
            this.UpdateStyles();

            InitializeComponent();
            startPlayer = player;
            startForm = start;

            pictureBoxWand.Click += PictureBoxWand_Click;

            // Timery
            timerPower.Interval = 33;
            timerPower.Tick += TimerPower_Tick;
            timerPower.Start();

            timerPigeon.Interval = 33;
            timerPigeon.Tick += TimerPigeon_Tick;
            timerPigeon.Start();

            // Ustawienie labeli
            labelPoints.Text = $"{points}";
            labelPoints.ForeColor = Color.White;
            labelPoints.BackColor = Color.FromArgb(1, 0, 0, 0);

            labelHeart.Text = hearts.ToString();
            labelHeart.BackColor = Color.FromArgb(1, 0, 0, 0);
            labelHeart.ForeColor = Color.White;

            labelEnergyPower.Text = energyPowerPoints.ToString();
            labelEnergyPower.BackColor = Color.FromArgb(1, 0, 0, 0);
            labelEnergyPower.ForeColor = Color.White;
            labelEnergyPower.Size = new System.Drawing.Size(75, 30);

            labelWeapon.Text = weaponPoints.ToString();
            labelWeapon.BackColor = Color.FromArgb(1, 0, 0, 0);
            labelWeapon.ForeColor = Color.White;
            labelWeapon.Size = new System.Drawing.Size(75, 30);

            labelEarlyPigeon.Text = earlyPigeonPoints.ToString();
            labelEarlyPigeon.BackColor = Color.FromArgb(1, 0, 0, 0);
            labelEarlyPigeon.ForeColor = Color.White;
            labelEarlyPigeon.Size = new System.Drawing.Size(75, 30);

            this.BackgroundImage = Pigeon_Invaders.Properties.Resources.background;
            this.BackgroundImageLayout = ImageLayout.Stretch; // dopasowanie do okna

            pictureBoxEnergyPower.Image = Pigeon_Invaders.Properties.Resources.EnergyPower;
            pictureBoxEnergyPower.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxEnergyPower.BackColor = Color.FromArgb(1, 0, 0, 0);

            //pictureBoxEnergyPower.BackColor = Color.Transparent;

            pictureBoxWeapon.Image = Pigeon_Invaders.Properties.Resources.Weapon;
            pictureBoxWeapon.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxWeapon.BackColor = Color.FromArgb(1, 0, 0, 0);

            //pictureBoxWeapon.BackColor = Color.Transparent;

            pictureBoxEarlyPigeon.Image = Pigeon_Invaders.Properties.Resources.EarlyPigeon;
            pictureBoxEarlyPigeon.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxEarlyPigeon.BackColor = Color.FromArgb(1, 0, 0, 0);

            //pictureBoxEarlyPigeon.BackColor = Color.Transparent;

            pictureBoxWand.Image = Pigeon_Invaders.Properties.Resources.wand;
            pictureBoxWand.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxWand.BackColor = Color.FromArgb(1, 0, 0, 0);

            //pictureBoxWand.BackColor = Color.Transparent;

            pictureBoxHeart.Image = Pigeon_Invaders.Properties.Resources.heart;
            pictureBoxHeart.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxHeart.BackColor = Color.FromArgb(1, 0, 0, 0);
        }

        private void PictureBoxWand_Click(object sender, EventArgs e)
        {
            int startX = pictureBoxWand.Left + pictureBoxWand.Width / 2 - 5;
            int startY = pictureBoxWand.Top - 20;

            Power newBullet = new Power(this, startX, startY);
            bullets.Add(newBullet);
        }

        private void TimerPower_Tick(object sender, EventArgs e)
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                var b = bullets[i];
                b.Move();

                bool bulletRemoved = false;

                for (int j = pigeons.Count - 1; j >= 0; j--)
                {
                    if (b.Picture.Bounds.IntersectsWith(pigeons[j].Picture.Bounds))
                    {
                        pigeons[j].Destroy(this);
                        pigeons.RemoveAt(j);

                        b.Destroy(this);
                        bullets.RemoveAt(i);

                        points += 1000;
                        labelPoints.Text = $"{points}";
                        bulletRemoved = true;
                        break;
                    }
                }

                if (!bulletRemoved && b.IsOutOfBounds(this))
                {
                    b.Destroy(this);
                    bullets.RemoveAt(i);
                }
            }
        }

        private void TimerPigeon_Tick(object sender, EventArgs e)
        {
            rowTickCounter++;

            // Tworzenie nowego rzędu co rowTickInterval
            if (rowTickCounter >= rowTickInterval)
            {
                rowTickCounter = 0;

                int screenCenterX = this.ClientSize.Width / 2;
                int totalRowWidth = 4 * 57 + 3 * 10; // 4 gołębie + odstępy
                int startX = screenCenterX - totalRowWidth / 2;
                int startY = 10; // góra ekranu

                for (int i = 0; i < 4; i++)
                {
                    int pigeonX = startX + i * (57 + 10);
                    Pigeon pigeon = new Pigeon(this, pigeonX, startY);
                    pigeons.Add(pigeon);
                }
            }

            // Poruszanie gołębi i sprawdzanie kolizji z graczem
            for (int i = pigeons.Count - 1; i >= 0; i--)
            {
                pigeons[i].Move();

                // Kolizja Wand–Pigeon
                if (pigeons[i].Picture.Bounds.IntersectsWith(pictureBoxWand.Bounds))
                {
                    pigeons[i].Destroy(this);
                    pigeons.RemoveAt(i);

                    hearts--;
                    labelHeart.Text = hearts.ToString();

                    if (hearts <= 0)
                    {
                        // Koniec gry
                        timerPower.Stop();
                        timerPigeon.Stop();

                        FormGameEnd endForm = new FormGameEnd(startForm, startPlayer);
                        this.Hide();
                        endForm.ShowDialog();

                        startPlayer?.controls.stop();
                        startForm?.Close();
                        this.Close();
                        return;
                    }
                }

                // Usuwanie gołębi poza ekranem
                if (i < pigeons.Count && pigeons[i].IsOutOfBounds(this))
                {
                    pigeons[i].Destroy(this);
                    pigeons.RemoveAt(i);
                }
            }
        }

        private void buttonExitGame_Click(object sender, EventArgs e)
        {
            startPlayer?.controls.stop();
            startForm?.Close();
            Application.Exit();
        }

        private void FormGameMain_MouseMove(object sender, MouseEventArgs e)
        {
            int mouseX = e.X;
            int newX = mouseX - pictureBoxWand.Width / 2;

            if (newX < 0) newX = 0;
            if (newX > this.ClientSize.Width - pictureBoxWand.Width)
                newX = this.ClientSize.Width - pictureBoxWand.Width;

            pictureBoxWand.Left = newX;
        }

        
    }
}