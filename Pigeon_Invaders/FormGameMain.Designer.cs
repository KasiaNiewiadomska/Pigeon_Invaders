namespace Pigeon_Invaders
{
    partial class FormGameMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            buttonExitGame = new Button();
            labelPoints = new Label();
            labelHeart = new Label();
            labelEnergyPower = new Label();
            labelWeapon = new Label();
            labelEarlyPigeon = new Label();
            labelNoLevel = new Label();
            pictureBoxWand = new PictureBox();
            pictureBoxEnergyPower = new PictureBox();
            pictureBoxWeapon = new PictureBox();
            pictureBoxEarlyPigeon = new PictureBox();
            timerPower = new System.Windows.Forms.Timer(components);
            timerPigeon = new System.Windows.Forms.Timer(components);
            pictureBoxHeart = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxWand).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxEnergyPower).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxWeapon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxEarlyPigeon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxHeart).BeginInit();
            SuspendLayout();
            // 
            // buttonExitGame
            // 
            buttonExitGame.Location = new Point(834, 24);
            buttonExitGame.Name = "buttonExitGame";
            buttonExitGame.Size = new Size(76, 21);
            buttonExitGame.TabIndex = 0;
            buttonExitGame.Text = "Exit Game";
            buttonExitGame.UseVisualStyleBackColor = true;
            buttonExitGame.Click += buttonExitGame_Click;
            // 
            // labelPoints
            // 
            labelPoints.AutoSize = true;
            labelPoints.BackColor = SystemColors.MenuHighlight;
            labelPoints.Location = new Point(25, 33);
            labelPoints.Name = "labelPoints";
            labelPoints.Size = new Size(0, 15);
            labelPoints.TabIndex = 1;
            // 
            // labelHeart
            // 
            labelHeart.AutoSize = true;
            labelHeart.Location = new Point(107, 27);
            labelHeart.Name = "labelHeart";
            labelHeart.Size = new Size(0, 15);
            labelHeart.TabIndex = 2;
            // 
            // labelEnergyPower
            // 
            labelEnergyPower.AutoSize = true;
            labelEnergyPower.Location = new Point(79, 65);
            labelEnergyPower.Name = "labelEnergyPower";
            labelEnergyPower.Size = new Size(0, 15);
            labelEnergyPower.TabIndex = 3;
            // 
            // labelWeapon
            // 
            labelWeapon.AutoSize = true;
            labelWeapon.Location = new Point(79, 91);
            labelWeapon.Name = "labelWeapon";
            labelWeapon.Size = new Size(0, 15);
            labelWeapon.TabIndex = 4;
            // 
            // labelEarlyPigeon
            // 
            labelEarlyPigeon.AutoSize = true;
            labelEarlyPigeon.Location = new Point(79, 117);
            labelEarlyPigeon.Name = "labelEarlyPigeon";
            labelEarlyPigeon.Size = new Size(0, 15);
            labelEarlyPigeon.TabIndex = 5;
            // 
            // labelNoLevel
            // 
            labelNoLevel.AutoSize = true;
            labelNoLevel.Location = new Point(25, 514);
            labelNoLevel.Name = "labelNoLevel";
            labelNoLevel.Size = new Size(0, 15);
            labelNoLevel.TabIndex = 6;
            // 
            // pictureBoxWand
            // 
            pictureBoxWand.BackColor = Color.Pink;
            pictureBoxWand.Location = new Point(479, 447);
            pictureBoxWand.Name = "pictureBoxWand";
            pictureBoxWand.Size = new Size(13, 84);
            pictureBoxWand.TabIndex = 11;
            pictureBoxWand.TabStop = false;
            // 
            // pictureBoxEnergyPower
            // 
            pictureBoxEnergyPower.Location = new Point(12, 51);
            pictureBoxEnergyPower.Name = "pictureBoxEnergyPower";
            pictureBoxEnergyPower.Size = new Size(47, 27);
            pictureBoxEnergyPower.TabIndex = 14;
            pictureBoxEnergyPower.TabStop = false;
            // 
            // pictureBoxWeapon
            // 
            pictureBoxWeapon.Location = new Point(12, 84);
            pictureBoxWeapon.Name = "pictureBoxWeapon";
            pictureBoxWeapon.Size = new Size(47, 26);
            pictureBoxWeapon.TabIndex = 15;
            pictureBoxWeapon.TabStop = false;
            // 
            // pictureBoxEarlyPigeon
            // 
            pictureBoxEarlyPigeon.Location = new Point(12, 116);
            pictureBoxEarlyPigeon.Name = "pictureBoxEarlyPigeon";
            pictureBoxEarlyPigeon.Size = new Size(47, 29);
            pictureBoxEarlyPigeon.TabIndex = 16;
            pictureBoxEarlyPigeon.TabStop = false;
            // 
            // pictureBoxHeart
            // 
            pictureBoxHeart.Location = new Point(79, 23);
            pictureBoxHeart.Name = "pictureBoxHeart";
            pictureBoxHeart.Size = new Size(22, 22);
            pictureBoxHeart.TabIndex = 17;
            pictureBoxHeart.TabStop = false;
            // 
            // FormGameMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(939, 551);
            Controls.Add(pictureBoxHeart);
            Controls.Add(pictureBoxEarlyPigeon);
            Controls.Add(pictureBoxWeapon);
            Controls.Add(pictureBoxEnergyPower);
            Controls.Add(pictureBoxWand);
            Controls.Add(labelNoLevel);
            Controls.Add(labelEarlyPigeon);
            Controls.Add(labelWeapon);
            Controls.Add(labelEnergyPower);
            Controls.Add(labelHeart);
            Controls.Add(labelPoints);
            Controls.Add(buttonExitGame);
            Name = "FormGameMain";
            Text = "FormGameMain";
            MouseMove += FormGameMain_MouseMove;
            ((System.ComponentModel.ISupportInitialize)pictureBoxWand).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxEnergyPower).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxWeapon).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxEarlyPigeon).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxHeart).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonExitGame;
        private Label labelPoints;
        private Label labelHeart;
        private Label labelEnergyPower;
        private Label labelWeapon;
        private Label labelEarlyPigeon;
        private Label labelNoLevel;
        private PictureBox pictureBoxWand;
        private PictureBox pictureBoxPower;
        private PictureBox pictureBoxEnergyPower;
        private PictureBox pictureBoxWeapon;
        private PictureBox pictureBoxEarlyPigeon;
        private System.Windows.Forms.Timer timerPower;
        private System.Windows.Forms.Timer timerPigeon;
        private PictureBox pictureBoxHeart;
    }
}