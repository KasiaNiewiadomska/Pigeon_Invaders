namespace Pigeon_Invaders
{
    partial class FormGameEnd
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
            pictureBoxGameOver = new PictureBox();
            buttonPlayAgain = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBoxGameOver).BeginInit();
            SuspendLayout();
            // 
            // pictureBoxGameOver
            // 
            pictureBoxGameOver.BackColor = Color.Black;
            pictureBoxGameOver.Location = new Point(254, 84);
            pictureBoxGameOver.Name = "pictureBoxGameOver";
            pictureBoxGameOver.Size = new Size(277, 143);
            pictureBoxGameOver.TabIndex = 0;
            pictureBoxGameOver.TabStop = false;
            // 
            // buttonPlayAgain
            // 
            buttonPlayAgain.Location = new Point(358, 279);
            buttonPlayAgain.Name = "buttonPlayAgain";
            buttonPlayAgain.Size = new Size(75, 23);
            buttonPlayAgain.TabIndex = 1;
            buttonPlayAgain.Text = "Play";
            buttonPlayAgain.UseVisualStyleBackColor = true;
            buttonPlayAgain.Click += buttonPlayAgain_Click;
            // 
            // FormGameEnd
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonPlayAgain);
            Controls.Add(pictureBoxGameOver);
            Name = "FormGameEnd";
            Text = "FormGameEnd";
            ((System.ComponentModel.ISupportInitialize)pictureBoxGameOver).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBoxGameOver;
        private Button buttonPlayAgain;
    }
}