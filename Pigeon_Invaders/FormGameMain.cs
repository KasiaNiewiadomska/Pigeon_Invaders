using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using WMPLib;

namespace Pigeon_Invaders
{
    public partial class FormGameMain : Form
    {
        private WindowsMediaPlayer startPlayer;
        private FormGameStart startForm;

        private readonly List<Power> bullets = new List<Power>();
        private readonly List<Pigeon> pigeons = new List<Pigeon>();

        private int rowTickCounter = 0;
        private int rowTickInterval = 50; // co ile ticków tworzymy nowy rząd

        private int points = 0; // licznik punktów
        private int hearts = 5; // liczba serc na początku gry

        private int energyPowerPoints = 1000;
        private int weaponPoints = 1000;
        private int earlyPigeonPoints = 1000;

        // Pre-scaled sprites used in painting
        private Image backgroundImage;
        private Image pigeonSprite;
        private Image powerSprite;

        public FormGameMain(WindowsMediaPlayer player, FormGameStart start)
        {
            // Double buffering
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint, true);
            this.UpdateStyles();
            this.DoubleBuffered = true;

            InitializeComponent();

            startPlayer = player;
            startForm = start;

            InitSprites();   // <<< pre-scale all images here
            InitTimers();
            InitHud();

            this.MouseDown += FormGameMain_MouseDown;
        }

        private void InitSprites()
        {
            // GAME BACKGROUND – pre-scale once to form size
            backgroundImage = new Bitmap(Properties.Resources.background, this.ClientSize);
            this.BackgroundImage = null;           // don’t use BackgroundImage + Stretch
            this.BackColor = Color.Black;

            // GAME SPRITES – scaled once, use as-is in DrawImage
            pigeonSprite = new Bitmap(Properties.Resources.pigeon, new Size(Pigeon.Width, Pigeon.Height));
            powerSprite = new Bitmap(Properties.Resources.power, new Size(Power.Size, Power.Size));

            // HUD ICONS – pre-scale to pictureBox sizes and disable runtime scaling
            if (pictureBoxEnergyPower.Width > 0 && pictureBoxEnergyPower.Height > 0)
            {
                pictureBoxEnergyPower.Image = new Bitmap(
                    Properties.Resources.EnergyPower,
                    pictureBoxEnergyPower.Size);
                pictureBoxEnergyPower.SizeMode = PictureBoxSizeMode.Normal;
                pictureBoxEnergyPower.BackColor = Color.Black;
            }

            if (pictureBoxWeapon.Width > 0 && pictureBoxWeapon.Height > 0)
            {
                pictureBoxWeapon.Image = new Bitmap(
                    Properties.Resources.Weapon,
                    pictureBoxWeapon.Size);
                pictureBoxWeapon.SizeMode = PictureBoxSizeMode.Normal;
                pictureBoxWeapon.BackColor = Color.Black;
            }

            if (pictureBoxEarlyPigeon.Width > 0 && pictureBoxEarlyPigeon.Height > 0)
            {
                pictureBoxEarlyPigeon.Image = new Bitmap(
                    Properties.Resources.EarlyPigeon,
                    pictureBoxEarlyPigeon.Size);
                pictureBoxEarlyPigeon.SizeMode = PictureBoxSizeMode.Normal;
                pictureBoxEarlyPigeon.BackColor = Color.Black;
            }

            if (pictureBoxWand.Width > 0 && pictureBoxWand.Height > 0)
            {
                pictureBoxWand.Image = new Bitmap(
                    Properties.Resources.wand,
                    pictureBoxWand.Size);
                pictureBoxWand.SizeMode = PictureBoxSizeMode.Normal;
                pictureBoxWand.BackColor = Color.Black;
            }

            if (pictureBoxHeart.Width > 0 && pictureBoxHeart.Height > 0)
            {
                pictureBoxHeart.Image = new Bitmap(
                    Properties.Resources.heart,
                    pictureBoxHeart.Size);
                pictureBoxHeart.SizeMode = PictureBoxSizeMode.Normal;
                pictureBoxHeart.BackColor = Color.Black;
            }
        }

        private void InitTimers()
        {
            // Use timerPower from designer as game loop
            if (timerPower != null)
            {
                timerPower.Stop();
                timerPower.Interval = 20;              // ~50 FPS, smooth and light
                timerPower.Tick -= TimerPower_Tick;    // in case designer already wired it
                timerPower.Tick += TimerPower_Tick;
                timerPower.Start();
            }

            // Disable timerPigeon from designer
            if (timerPigeon != null)
            {
                timerPigeon.Stop();
                timerPigeon.Enabled = false;
            }

            //pictureBoxWand.Click += PictureBoxWand_Click;
        }

        private void InitHud()
        {
            labelPoints.Text = $"{points}";
            labelPoints.ForeColor = Color.White;
            labelPoints.BackColor = Color.Black;

            labelHeart.Text = hearts.ToString();
            labelHeart.BackColor = Color.Black;
            labelHeart.ForeColor = Color.White;

            labelEnergyPower.Text = energyPowerPoints.ToString();
            labelEnergyPower.BackColor = Color.Black;
            labelEnergyPower.ForeColor = Color.White;
            labelEnergyPower.Size = new Size(75, 30);

            labelWeapon.Text = weaponPoints.ToString();
            labelWeapon.BackColor = Color.Black;
            labelWeapon.ForeColor = Color.White;
            labelWeapon.Size = new Size(75, 30);

            labelEarlyPigeon.Text = earlyPigeonPoints.ToString();
            labelEarlyPigeon.BackColor = Color.Black;
            labelEarlyPigeon.ForeColor = Color.White;
            labelEarlyPigeon.Size = new Size(75, 30);
        }

        // Don’t let WinForms repaint background separately – we draw it in OnPaint
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // do nothing – background is drawn in OnPaint
        }

        // MAIN GAME LOOP
        private void TimerPower_Tick(object sender, EventArgs e)
        {
            UpdateBullets();
            UpdatePigeons();
            Invalidate(); // triggers OnPaint
        }

        // Shooting
        //private void PictureBoxWand_Click(object sender, EventArgs e)
        //{
        //    Shoot();
        //}

        private void UpdateBullets()
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                var b = bullets[i];
                b.Move();

                bool bulletRemoved = false;
                RectangleF bRect = b.Bounds;

                // Bullet–Pigeon collisions
                for (int j = pigeons.Count - 1; j >= 0; j--)
                {
                    if (bRect.IntersectsWith(pigeons[j].Bounds))
                    {
                        pigeons.RemoveAt(j);
                        bullets.RemoveAt(i);

                        points += 1000;
                        labelPoints.Text = points.ToString();
                        bulletRemoved = true;
                        break;
                    }
                }

                // Bullet off-screen
                if (!bulletRemoved)
                {
                    if (b.Y + Power.Size < 0 ||
                        b.X + Power.Size < 0 ||
                        b.X > this.ClientSize.Width)
                    {
                        bullets.RemoveAt(i);
                    }
                }
            }
        }

        private void UpdatePigeons()
        {
            rowTickCounter++;

            // Spawn a new row every rowTickInterval ticks
            if (rowTickCounter >= rowTickInterval)
            {
                rowTickCounter = 0;
                SpawnPigeonRow();
            }

            Rectangle wandRectInt = pictureBoxWand.Bounds;
            RectangleF wandRect = new RectangleF(
                wandRectInt.X,
                wandRectInt.Y,
                wandRectInt.Width,
                wandRectInt.Height);

            for (int i = pigeons.Count - 1; i >= 0; i--)
            {
                var p = pigeons[i];
                p.Move();

                // Wand–Pigeon collision
                if (p.Bounds.IntersectsWith(wandRect))
                {
                    pigeons.RemoveAt(i);

                    hearts--;
                    labelHeart.Text = hearts.ToString();

                    if (hearts <= 0)
                    {
                        GameOver();
                        return;
                    }

                    continue;
                }

                // Pigeon off-screen
                if (p.Y > this.ClientSize.Height)
                {
                    pigeons.RemoveAt(i);
                }
            }
        }

        private void SpawnPigeonRow()
        {
            int screenCenterX = this.ClientSize.Width / 2;
            int spacing = 10;
            int totalRowWidth = 4 * Pigeon.Width + 3 * spacing;
            int startX = screenCenterX - totalRowWidth / 2;
            int startY = 10;

            for (int i = 0; i < 4; i++)
            {
                int pigeonX = startX + i * (Pigeon.Width + spacing);
                pigeons.Add(new Pigeon(pigeonX, startY));
            }
        }
        private void Shoot()
        {
            float startX = pictureBoxWand.Left + pictureBoxWand.Width / 2f - Power.Size / 2f;
            float startY = pictureBoxWand.Top - Power.Size;

            bullets.Add(new Power(startX, startY));
        }
        private void GameOver()
        {
            if (timerPower != null)
            {
                timerPower.Stop();
            }

            FormGameEnd endForm = new FormGameEnd(startForm, startPlayer);
            this.Hide();
            endForm.ShowDialog();

            startPlayer?.controls.stop();
            startForm?.Close();
            this.Close();
        }

        private void buttonExitGame_Click(object sender, EventArgs e)
        {
            if (timerPower != null)
            {
                timerPower.Stop();
            }

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

        private void FormGameMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Shoot();
            }
        }

        // Single place where we draw everything
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;

            // Speed over quality
            g.SmoothingMode = SmoothingMode.None;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.CompositingQuality = CompositingQuality.HighSpeed;
            g.PixelOffsetMode = PixelOffsetMode.HighSpeed;

            // BACKGROUND
            if (backgroundImage != null)
            {
                g.DrawImage(backgroundImage, 0, 0);
            }
            else
            {
                g.Clear(Color.Black);
            }

            // PIGEONS – already scaled, no dst rect needed
            foreach (var p in pigeons)
            {
                g.DrawImage(pigeonSprite, p.X, p.Y);
            }

            // BULLETS – already scaled
            foreach (var b in bullets)
            {
                g.DrawImage(powerSprite, b.X, b.Y);
            }

            // allow WinForms to draw HUD controls on top
            base.OnPaint(e);
        }
    }
}
