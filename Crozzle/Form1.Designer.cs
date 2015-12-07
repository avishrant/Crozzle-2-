namespace Crozzle
{
    partial class Form1
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
            this.btnLoadWords = new System.Windows.Forms.Button();
            this.btnSolveGame = new System.Windows.Forms.Button();
            this.txtGameDisplay = new System.Windows.Forms.RichTextBox();
            this.lblScoreDisplay = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnLoadWords
            // 
            this.btnLoadWords.Location = new System.Drawing.Point(93, 325);
            this.btnLoadWords.Name = "btnLoadWords";
            this.btnLoadWords.Size = new System.Drawing.Size(75, 23);
            this.btnLoadWords.TabIndex = 0;
            this.btnLoadWords.Text = "LoadWords";
            this.btnLoadWords.UseVisualStyleBackColor = true;
            this.btnLoadWords.Click += new System.EventHandler(this.btnLoadWords_Click);
            // 
            // btnSolveGame
            // 
            this.btnSolveGame.Location = new System.Drawing.Point(275, 325);
            this.btnSolveGame.Name = "btnSolveGame";
            this.btnSolveGame.Size = new System.Drawing.Size(75, 23);
            this.btnSolveGame.TabIndex = 1;
            this.btnSolveGame.Text = "SolveGame";
            this.btnSolveGame.UseVisualStyleBackColor = true;
            this.btnSolveGame.Click += new System.EventHandler(this.btnSolveGame_Click);
            // 
            // txtGameDisplay
            // 
            this.txtGameDisplay.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGameDisplay.Location = new System.Drawing.Point(12, 36);
            this.txtGameDisplay.Name = "txtGameDisplay";
            this.txtGameDisplay.Size = new System.Drawing.Size(447, 270);
            this.txtGameDisplay.TabIndex = 2;
            this.txtGameDisplay.Text = "";
            // 
            // lblScoreDisplay
            // 
            this.lblScoreDisplay.AutoSize = true;
            this.lblScoreDisplay.Location = new System.Drawing.Point(228, 13);
            this.lblScoreDisplay.Name = "lblScoreDisplay";
            this.lblScoreDisplay.Size = new System.Drawing.Size(19, 13);
            this.lblScoreDisplay.TabIndex = 4;
            this.lblScoreDisplay.Text = "00";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(178, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Score:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 377);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblScoreDisplay);
            this.Controls.Add(this.txtGameDisplay);
            this.Controls.Add(this.btnSolveGame);
            this.Controls.Add(this.btnLoadWords);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "-Rek\'s Crozzle-";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLoadWords;
        private System.Windows.Forms.Button btnSolveGame;
        private System.Windows.Forms.RichTextBox txtGameDisplay;
        private System.Windows.Forms.Label lblScoreDisplay;
        private System.Windows.Forms.Label label2;
    }
}

