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

        private readonly List<Feather> feathers = new List<Feather>();
        private Image featherSprite;
        private static readonly Random rand = new Random();

        private readonly List<MeatRoll> meatRolls = new List<MeatRoll>();
        private Image meatRollSprite;

        private const int MaxPigeonsInRow = 14;

        private int rowTickCounter = 0;
        private int rowTickInterval = 30; // co ile ticków tworzymy nowy rząd

        private int points = 0; // licznik punktów
        private float hearts = 5f; // liczba serc na początku gry, typu float bo można 0.5

        private int currentRow = 1;  // numer kolejnego rzędu gołębi (1..10)
        private int pigeonsInRow = 1;          // aktualna liczba gołębi w rzędzie
        private bool increasing = true;        // kierunek: true = od 1 do 10, false = od 10 do 1
        private int patternCycles = 0;          // ile pełnych cykli (1 do 10 lub 10 do 1)

        private int energyPowerPoints = 1000;
        private int weaponPoints = 1000;
        private int earlyPigeonPoints = 1000;

        private Image backgroundImage;
        private Image pigeonSprite;
        private Image powerSprite;

        // Level 2
        private bool level2Started = false;   // czy Level 2 się rozpoczął
        private bool showLevel2Graphic = false; // czy rysować Level2 na górze ekranu
        private int level2GraphicTicks = 0;
        private const int Level2GraphicDuration = 100; // 100 ticków * 20ms = 2s
        private int level2DelayTicks = 0;     // do opóźnienia tworzenia rzędów
        private const int Level2DelayDuration = 100; // 2 sekundy (100 ticków po 20ms)
        private Image level2Background;       // tło dla Level 2

        // BOSS
        private Boss boss = null;
        private Image bossSprite;
        private Image bossFightSprite;

        private bool bossFightStarted = false;
        private bool showBossIntro = false;
        private int bossIntroTicks = 0;
        private int bossDelayTicks = 0;

        private const int BossIntroDuration = 100; // 2s (100 * 20ms)
        private const int BossDelayDuration = 100;

        private bool bossMusicStarted = false; // czy muzyka Bossa została już włączona
        private Image bossBackgroundImage; // tło podczas boss fight
        private bool bossDefeated = false; // true, gdy boss został już pokonany


        public FormGameMain(WindowsMediaPlayer player, FormGameStart start)
        {
            // Double buffering - zapobiega migotaniu, gra rysuje się płynniej
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint, true);
            this.UpdateStyles();
            this.DoubleBuffered = true;

            InitializeComponent();

            startPlayer = player;
            startForm = start;

            // Przywróć domyślną muzykę menu / początkową gry
            if (startForm.SoundOn)
            {
                startPlayer.controls.stop();

                // ustaw ścieżkę do domyślnej muzyki poziomów 1–2
                string menuMusicPath = Path.Combine(Path.GetTempPath(), "doveBird.mp3");


                if (!File.Exists(menuMusicPath))
                {
                    using (var ms = Pigeon_Invaders.Properties.Resources.doveBird)
                    using (var fs = new FileStream(menuMusicPath, FileMode.Create, FileAccess.Write))
                    {
                        ms.CopyTo(fs);
                    }
                }

                startPlayer.controls.stop();
                startPlayer.URL = menuMusicPath;
                startPlayer.settings.volume = 50;
                startPlayer.settings.setMode("loop", true);
                startPlayer.controls.play();
            }


            InitSprites();   // wstępne skalowanie obrazów
            InitTimers();
            InitHud();

            pictureBoxWand.MouseDown += PictureBoxWand_MouseDown;
        }

        // wszystkie obrazy skalowane raz na początku gry, przyspiesza rendering
        private void InitSprites()
        {
            // tło gry skalowane raz do rozmiaru okna
            backgroundImage = new Bitmap(Properties.Resources.background, this.ClientSize);
            this.BackgroundImage = null;           // nie używać BackgroundImage + Stretch
            this.BackColor = Color.Black;

            // game sprites - skalowane raz, używane bezpośrednio w DrawImage
            pigeonSprite = new Bitmap(Properties.Resources.pigeon, new Size((int)Pigeon.Width, (int)Pigeon.Height));
            powerSprite = new Bitmap(Properties.Resources.power, new Size(Power.Size, Power.Size));
            featherSprite = new Bitmap(Properties.Resources.feather_violet, new Size(Feather.Width, Feather.Height));
            meatRollSprite = new Bitmap(Properties.Resources.meat_roll, new Size(MeatRoll.Width, MeatRoll.Height));
            level2Background = new Bitmap(Properties.Resources.level2, this.ClientSize);
            
            bossSprite = new Bitmap(Properties.Resources.boss, new Size(Boss.Width, Boss.Height));
            bossFightSprite = new Bitmap(Properties.Resources.BossFight, this.ClientSize);
            bossBackgroundImage = new Bitmap(Properties.Resources.Krakow_square, this.ClientSize);

            // hud icons - skalowane raz do rozmiaru pictureBoxów, wyłączone skalowanie w czasie gry
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

        // dwa timery z designera: timerPower i timerPigeon
        private void InitTimers()
        {
            //timerPower z disignera używamy jako głównej pętli gry
            if (timerPower != null)
            {
                timerPower.Stop();
                timerPower.Interval = 20;
                timerPower.Tick -= TimerPower_Tick;    
                timerPower.Tick += TimerPower_Tick;
                timerPower.Start();
            }

            // timerPigeon nie jest już potrzebny
            if (timerPigeon != null)
            {
                timerPigeon.Stop();
                timerPigeon.Enabled = false;
            }

        }

        //inicjalizacja labeli z designera
        private void InitHud()
        {
            labelPoints.Text = $"{points}";
            labelPoints.ForeColor = Color.White;
            labelPoints.BackColor = Color.Black;

            labelHeart.Text = hearts.ToString("0.0");
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

        // Główna pętla gry, wywoływana co tick timera (20ms)
        private void TimerPower_Tick(object sender, EventArgs e) //wywoływane co tick timera, 50 razy na sekundę
        {
            UpdateBullets();

            if (bossFightStarted)
            {
                UpdateBossFight();
            }
            else
            {
                UpdatePigeons();
            }

            TrySpawnFeather();
            UpdateFeathers();
            UpdateMeatRolls();
            CheckBossFight();
            Invalidate();  //Invalidate powoduje wywołanie OnPaint, co rysuje całą scenę gry na nowo

        }

        private void UpdateBullets()
        {
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                var b = bullets[i];
                b.Move();

                bool bulletRemoved = false;
                RectangleF bRect = b.Bounds;

                // kolizje pocisku z bossem
                if (boss != null && bRect.IntersectsWith(boss.Bounds))
                {
                    boss.Lives--;
                    bullets.RemoveAt(i);

                    continue;  // przechodzimy do następnego pocisku
                }

                //kolizje pocisku z gołębiami
                for (int j = pigeons.Count - 1; j >= 0; j--)
                {
                    if (bRect.IntersectsWith(pigeons[j].Bounds)) // jeśli prostokąty pocisku i gołębia ze sobą kolidują
                    {
                        var hitPigeon = pigeons[j];

                        // pozycja gołąbka = miejsce śmierci gołębia
                        float meatX = hitPigeon.X + Pigeon.Width / 2f - MeatRoll.Width / 2f;
                        float meatY = hitPigeon.Y + Pigeon.Height / 2f - MeatRoll.Height / 2f;

                        meatRolls.Add(new MeatRoll(meatX, meatY));
                        pigeons.RemoveAt(j);
                        bullets.RemoveAt(i);

                        points += 1000;
                        labelPoints.Text = points.ToString();
                        bulletRemoved = true;
                        break;
                    }
                }

                //pocisk poza ekranem
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

            // sprawdzanie LEVEL 2
            if (level2Started)
            {
                // Opóźnienie startu rzędów w Level2
                if (level2DelayTicks < Level2DelayDuration)
                {
                    level2DelayTicks++;
                    return; // jeszcze nie tworzymy rzędów
                }
            }

            // tworzenie nowego rzędu co określoną liczbę ticków
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

                // kolizje gołębia z różdżką
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

                // gołąb poza ekranem na dole
                if (p.Y > this.ClientSize.Height)
                {
                    pigeons.RemoveAt(i);
                }
            }
        }

        private void SpawnPigeonRow()
        {
            const int spacing = 15;

            // start zawsze u góry
            int startY = -(int)Pigeon.Height;

            // wycentrowanie rzędu
            int totalRowWidth =
                pigeonsInRow * (int)Pigeon.Width +
                (pigeonsInRow - 1) * spacing;

            int startX = (ClientSize.Width - totalRowWidth) / 2;

            for (int i = 0; i < pigeonsInRow; i++)
            {
                // LEVEL2: co drugi gołąb pomijamy
                if (level2Started && i % 2 != 0) continue;

                int x = startX + i * ((int)Pigeon.Width + spacing);

                if (level2Started)
                {
                    // granice rzędu
                    GetPigeonRowBounds(out float leftBound, out float rightBound);

                    // użycie konstruktora Level2
                    pigeons.Add(new Pigeon(x, startY, leftBound, rightBound)); //pigeon level 2
                }
                else
                {
                    pigeons.Add(new Pigeon(x, startY)); //pigeon level 1
                }
            }

            // Zmiana wzorca układania się gołębi w rzędach
            if (increasing)
            {
                pigeonsInRow++;

                if (pigeonsInRow > MaxPigeonsInRow)
                {
                    pigeonsInRow = MaxPigeonsInRow;
                    patternCycles++;

                    if (patternCycles >= 3)
                    {
                        increasing = false;   // zmiana na od 10 do 1 gołębia w rzędzie
                        patternCycles = 0;
                    }
                }
            }
            else
            {
                pigeonsInRow--;

                if (pigeonsInRow < 1)
                {
                    pigeonsInRow = 1;
                    patternCycles++;

                    if (patternCycles >= 3)
                    {
                        increasing = true;    // zmiana na od 1 do 10 gołębi w rzędzie
                        patternCycles = 0;
                    }
                }
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

        private void TrySpawnFeather()
        {
            if (pigeons.Count == 0)
                return;

            if (rand.NextDouble() < 0.05)
            {
                int index = rand.Next(pigeons.Count);
                var p = pigeons[index];

                float x = p.X + Pigeon.Width / 2f - Feather.Width / 2f;
                float y = p.Y + Pigeon.Height;

                GetPigeonRowBounds(out float leftBound, out float rightBound);
                feathers.Add(new Feather(x, y, leftBound, rightBound));
            }
        }

        private void UpdateFeathers()
        {
            RectangleF wandRect = pictureBoxWand.Bounds;

            for (int i = feathers.Count - 1; i >= 0; i--)
            {
                var f = feathers[i];
                f.Move();

                // kolizja z różdżką
                if (f.Bounds.IntersectsWith(wandRect))
                {
                    feathers.RemoveAt(i);

                    hearts -= 0.5f;
                    if (hearts < 0) hearts = 0;

                    labelHeart.Text = hearts.ToString("0.0");

                    if (hearts <= 0)
                    {
                        GameOver();
                        return;
                    }

                    continue;
                }

                // piórko poza ekranem
                if (f.Y > this.ClientSize.Height)
                {
                    feathers.RemoveAt(i);
                }
            }
        }

        private void UpdateMeatRolls()
        {
            RectangleF wandRect = pictureBoxWand.Bounds;

            for (int i = meatRolls.Count - 1; i >= 0; i--)
            {
                var m = meatRolls[i];
                m.Move();

                // kolizja z różdżką = leczenie
                if (m.Bounds.IntersectsWith(wandRect))
                {
                    meatRolls.RemoveAt(i);

                    hearts += 0.5f;
                    if (hearts > 5f) hearts = 5f;

                    labelHeart.Text = hearts.ToString("0.0");
                    continue;
                }

                // poza ekranem
                if (m.Y > this.ClientSize.Height)
                {
                    meatRolls.RemoveAt(i);
                }
            }
        }

        private void CheckBossFight()
        {

            if (!level2Started && points >= 30000)
            {
                level2Started = true;
                pigeons.Clear();
                feathers.Clear();

                // pokaż grafikę Level2 na 2s
                showLevel2Graphic = true;
                level2GraphicTicks = 0;

                rowTickCounter = 0;
                level2DelayTicks = 0;

                pigeonsInRow = MaxPigeonsInRow;
            }


            if (bossFightStarted || bossDefeated)
                return;

            if (points >= 70000)
            {
                bossFightStarted = true;
                showBossIntro = true;

                pigeons.Clear();   // usuń zwykłych wrogów
                feathers.Clear();

                // granice ruchu bossa
                float leftBound = 0f;
                float rightBound = ClientSize.Width;

                // pozycja startowa bossa
                float x = ClientSize.Width / 2f - Boss.Width / 2f;

                // UTWORZENIE BOSSA
                boss = new Boss(x, -Boss.Height, leftBound, rightBound)
                {
                    Lives = 30
                };

            }
        }

        private void UpdateBossFight()
        {
            if (bossDefeated || boss == null)
                return; // jeśli boss już pokonany, nic nie rób

            // intro "BOSS FIGHT"
            if (showBossIntro)
            {
                // jeśli muzyka bossa jeszcze nie startowała, zmień ją
                if (!bossMusicStarted)
                {
                    bossMusicStarted = true;

                    // zatrzymaj aktualną muzykę
                    startPlayer.controls.stop();

                    // przygotuj tymczasowy plik z zasobu StMarysbuglecall - Hejnał Mariacki
                    string bossMusicPath = Path.Combine(Path.GetTempPath(), "StMarysbuglecall.mp3");
                    using (var ms = Pigeon_Invaders.Properties.Resources.StMarysbuglecall)
                    using (var fs = new FileStream(bossMusicPath, FileMode.Create, FileAccess.Write))
                    {
                        ms.CopyTo(fs);
                    }

                    // ustaw nową muzykę tylko jeśli muzyka jest włączona w opcjach
                    if (startForm.SoundOn)
                    {
                        startPlayer.URL = bossMusicPath;
                        startPlayer.settings.volume = 50;
                        startPlayer.settings.setMode("loop", true);
                        startPlayer.controls.play();
                    }

                    // Zmień tło na tło Bossa
                    backgroundImage = bossBackgroundImage;

                }

                bossIntroTicks++;
                if (bossIntroTicks >= BossIntroDuration)
                {
                    showBossIntro = false;
                }
                return;
            }

            // ruch bossa
            boss.Move();

            if (boss.Y > ClientSize.Height || boss.Bounds.IntersectsWith(pictureBoxWand.Bounds))
            {
                GameOver();
                return;
            }

            TryBossShoot();

            if (boss.Lives <= 0)
            {
                boss = null;
                bossDefeated = true;

                points += 10000;
                labelPoints.Text = points.ToString();

                startPlayer.controls.stop();

                ShowWinScreen();
            }
        }

        private void TryBossShoot()
        {
            if (boss == null) return;

            // losowe pióra co tick z 5% szansą
            if (rand.NextDouble() < 0.05)
            {
                float x = boss.X + rand.Next(0, Boss.Width - Feather.Width);
                float y = boss.Y + Boss.Height;

                // podczas walki z bossem piórka odbijają się od całego ekranu
                float leftBound = 0f;
                float rightBound = ClientSize.Width;

                feathers.Add(new Feather(x, y, leftBound, rightBound));
            }
        }

        private void ShowWinScreen()
        {
            if (timerPower != null)
                timerPower.Stop();

            FormGameWin winForm = new FormGameWin(startForm, startPlayer);
            this.Hide();

            var result = winForm.ShowDialog();

            if (result == DialogResult.Retry)
            {
                // restart gry
                FormGameMain newGame = new FormGameMain(startPlayer, startForm);
                newGame.Show();
                this.Close(); // zamknij starą grę
            }
            else
            {
                this.Close();
            }
        }

        private void GetPigeonRowBounds(out float left, out float right)
        {
            int spacing = 15;
            int pigeonsInRow = MaxPigeonsInRow;

            int totalRowWidth =
                pigeonsInRow * (int)Pigeon.Width +
                (pigeonsInRow - 1) * spacing;

            left = (ClientSize.Width - totalRowWidth) / 2f;
            right = left + totalRowWidth;
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

            int minX, maxX;

            if (bossFightStarted && boss != null)
            {
                // podczas walki z bossem wand może iść całą szerokością ekranu
                minX = 0;
                maxX = ClientSize.Width - pictureBoxWand.Width;
            }
            else
            {
                // granice rzędu gołębi
                int spacing = 10;
                int pigeonsInRow = MaxPigeonsInRow;

                int totalRowWidth = pigeonsInRow * (int)Pigeon.Width + (pigeonsInRow - 1) * spacing;
                int rowLeft = (this.ClientSize.Width - totalRowWidth) / 2;
                int rowRight = rowLeft + totalRowWidth;

                minX = rowLeft;
                maxX = rowRight - pictureBoxWand.Width;
            }

            // ograniczenie do min/max
            if (newX < minX) newX = minX;
            if (newX > maxX) newX = maxX;

            pictureBoxWand.Left = newX;
        }

        private void PictureBoxWand_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Shoot();
            }
        }

        // miejsce gdzie wszystko jest rysowne w kolejności tło, gołębie, pociski, HUD (warstwa interfejsu: labele, picturebox hearts i wand)
        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;

            // szybsze rysowanie kosztem jakości
            g.SmoothingMode = SmoothingMode.None;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.CompositingQuality = CompositingQuality.HighSpeed;
            g.PixelOffsetMode = PixelOffsetMode.HighSpeed;

            // tło
            if (backgroundImage != null)
            {
                g.DrawImage(backgroundImage, 0, 0);
            }
            else
            {
                g.Clear(Color.Black);
            }

            // grafika Level2 pokazana na górze ekranu przez 2s
            if (showLevel2Graphic && level2Background != null)
            {
                g.DrawImage(level2Background, 0, 0);

                // odliczamy ticki
                level2GraphicTicks++;
                if (level2GraphicTicks >= Level2GraphicDuration)
                {
                    showLevel2Graphic = false; // po 2s znika
                }
            }

            // gołębie - rysowane w oryginalnym, przeskalowanym rozmiarze
            foreach (var p in pigeons)
            {
                g.DrawImage(pigeonSprite, p.X, p.Y);
            }

            // pociski rysowane w oryginalnym, przeskalowanym rozmiarze
            foreach (var b in bullets)
            {
                g.DrawImage(powerSprite, b.X, b.Y);
            }

            // pióra rysowane w oryginalnym, przeskalowanym rozmiarze
            foreach (var f in feathers)
            {
                g.DrawImage(featherSprite, f.X, f.Y);
            }

            // gołąbki rysowane w oryginalnym, przeskalowanym rozmiarze
            foreach (var m in meatRolls)
            {
                g.DrawImage(meatRollSprite, m.X, m.Y);
            }

            // intro bossa na pełnym ekranie
            if (showBossIntro && bossFightSprite != null)
            {
                g.DrawImage(bossFightSprite, 0, 0);
                return;
            }

            // BOSS
            if (boss != null)
            {
                g.DrawImage(bossSprite, boss.X, boss.Y);
            }

            // pozwól WinForms narysować kontrolki HUD na wierzchu
            base.OnPaint(e);
        }
    }
}
