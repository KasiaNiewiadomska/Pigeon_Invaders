namespace Pigeon_Invaders
{
    partial class FormGameStart
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
            buttonPlay = new Button();
            buttonMusic = new Button();
            buttonExit = new Button();
            pictureBoxPigeonInvaders = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPigeonInvaders).BeginInit();
            SuspendLayout();
            // 
            // buttonPlay
            // 
            buttonPlay.Location = new Point(333, 277);
            buttonPlay.Name = "buttonPlay";
            buttonPlay.Size = new Size(137, 60);
            buttonPlay.TabIndex = 0;
            buttonPlay.Text = "Play";
            buttonPlay.UseVisualStyleBackColor = true;
            buttonPlay.Click += buttonPlay_Click;
            // 
            // buttonMusic
            // 
            buttonMusic.Location = new Point(38, 28);
            buttonMusic.Name = "buttonMusic";
            buttonMusic.Size = new Size(70, 48);
            buttonMusic.TabIndex = 1;
            buttonMusic.Text = "Music ON";
            buttonMusic.UseVisualStyleBackColor = true;
            buttonMusic.Click += buttonMusic_Click;
            // 
            // buttonExit
            // 
            buttonExit.Location = new Point(702, 28);
            buttonExit.Name = "buttonExit";
            buttonExit.Size = new Size(75, 23);
            buttonExit.TabIndex = 2;
            buttonExit.Text = "Exit";
            buttonExit.UseVisualStyleBackColor = true;
            buttonExit.Click += buttonExit_Click;
            // 
            // pictureBoxPigeonInvaders
            // 
            pictureBoxPigeonInvaders.Location = new Point(234, 66);
            pictureBoxPigeonInvaders.Name = "pictureBoxPigeonInvaders";
            pictureBoxPigeonInvaders.Size = new Size(336, 180);
            pictureBoxPigeonInvaders.TabIndex = 3;
            pictureBoxPigeonInvaders.TabStop = false;
            // 
            // FormGameStart
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pictureBoxPigeonInvaders);
            Controls.Add(buttonExit);
            Controls.Add(buttonMusic);
            Controls.Add(buttonPlay);
            Name = "FormGameStart";
            Text = "FormGameStart";
            ((System.ComponentModel.ISupportInitialize)pictureBoxPigeonInvaders).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button buttonPlay;
        private Button buttonMusic;
        private Button buttonExit;
        private PictureBox pictureBoxPigeonInvaders;
    }
}