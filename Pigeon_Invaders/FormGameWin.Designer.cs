namespace Pigeon_Invaders
{
    partial class FormGameWin
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
            buttonPlayAgain = new Button();
            pictureBoxYouWin = new PictureBox();
<<<<<<< HEAD
            buttonExit = new Button();
=======
>>>>>>> fcf05b02568886f771c66614057ce19f3f2efe4b
            ((System.ComponentModel.ISupportInitialize)pictureBoxYouWin).BeginInit();
            SuspendLayout();
            // 
            // buttonPlayAgain
            // 
            buttonPlayAgain.Location = new Point(326, 313);
            buttonPlayAgain.Name = "buttonPlayAgain";
            buttonPlayAgain.Size = new Size(129, 23);
            buttonPlayAgain.TabIndex = 0;
            buttonPlayAgain.Text = "Once more?";
            buttonPlayAgain.UseVisualStyleBackColor = true;
            buttonPlayAgain.Click += buttonPlayAgain_Click;
            // 
            // pictureBoxYouWin
            // 
            pictureBoxYouWin.Location = new Point(228, 67);
            pictureBoxYouWin.Name = "pictureBoxYouWin";
            pictureBoxYouWin.Size = new Size(309, 181);
            pictureBoxYouWin.TabIndex = 1;
            pictureBoxYouWin.TabStop = false;
            // 
<<<<<<< HEAD
            // buttonExit
            // 
            buttonExit.Location = new Point(686, 31);
            buttonExit.Name = "buttonExit";
            buttonExit.Size = new Size(75, 23);
            buttonExit.TabIndex = 2;
            buttonExit.Text = "Exit";
            buttonExit.UseVisualStyleBackColor = true;
            buttonExit.Click += buttonExit_Click;
            // 
=======
>>>>>>> fcf05b02568886f771c66614057ce19f3f2efe4b
            // FormGameWin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
<<<<<<< HEAD
            Controls.Add(buttonExit);
=======
>>>>>>> fcf05b02568886f771c66614057ce19f3f2efe4b
            Controls.Add(pictureBoxYouWin);
            Controls.Add(buttonPlayAgain);
            Name = "FormGameWin";
            Text = "FormGameWin";
            ((System.ComponentModel.ISupportInitialize)pictureBoxYouWin).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button buttonPlayAgain;
        private PictureBox pictureBoxYouWin;
<<<<<<< HEAD
        private Button buttonExit;
=======
>>>>>>> fcf05b02568886f771c66614057ce19f3f2efe4b
    }
}