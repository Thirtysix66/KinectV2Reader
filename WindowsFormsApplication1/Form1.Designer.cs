namespace WindowsFormsApplication1
{
    partial class Playback
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
            this.btnPlay = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.chooseDir = new System.Windows.Forms.Button();
            this.pictureDepth = new System.Windows.Forms.PictureBox();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.pictureRGB = new System.Windows.Forms.PictureBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.videoDir = new System.Windows.Forms.TextBox();
            this.frameProgress = new System.Windows.Forms.ProgressBar();
            this.frameDate = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureRGB)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPlay
            // 
            this.btnPlay.AutoSize = true;
            this.btnPlay.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnPlay.BackColor = System.Drawing.Color.Chartreuse;
            this.btnPlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlay.Location = new System.Drawing.Point(513, 22);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Padding = new System.Windows.Forms.Padding(10);
            this.btnPlay.Size = new System.Drawing.Size(80, 55);
            this.btnPlay.TabIndex = 0;
            this.btnPlay.Text = "Play";
            this.btnPlay.UseVisualStyleBackColor = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // chooseDir
            // 
            this.chooseDir.AutoSize = true;
            this.chooseDir.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.chooseDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.chooseDir.Location = new System.Drawing.Point(32, 21);
            this.chooseDir.Name = "chooseDir";
            this.chooseDir.Padding = new System.Windows.Forms.Padding(10);
            this.chooseDir.Size = new System.Drawing.Size(147, 55);
            this.chooseDir.TabIndex = 1;
            this.chooseDir.Text = "Choose File";
            this.chooseDir.UseVisualStyleBackColor = true;
            this.chooseDir.Click += new System.EventHandler(this.button2_Click);
            // 
            // pictureDepth
            // 
            this.pictureDepth.AccessibleDescription = "";
            this.pictureDepth.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureDepth.Location = new System.Drawing.Point(51, 115);
            this.pictureDepth.MinimumSize = new System.Drawing.Size(600, 450);
            this.pictureDepth.Name = "pictureDepth";
            this.pictureDepth.Size = new System.Drawing.Size(800, 450);
            this.pictureDepth.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureDepth.TabIndex = 2;
            this.pictureDepth.TabStop = false;
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            // 
            // pictureRGB
            // 
            this.pictureRGB.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureRGB.Location = new System.Drawing.Point(925, 114);
            this.pictureRGB.MaximumSize = new System.Drawing.Size(1000, 800);
            this.pictureRGB.MinimumSize = new System.Drawing.Size(600, 600);
            this.pictureRGB.Name = "pictureRGB";
            this.pictureRGB.Size = new System.Drawing.Size(929, 600);
            this.pictureRGB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureRGB.TabIndex = 25;
            this.pictureRGB.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label11.Location = new System.Drawing.Point(1295, 734);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(165, 25);
            this.label11.TabIndex = 33;
            this.label11.Text = "1920x1080 pixels";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label12.Location = new System.Drawing.Point(536, 746);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(143, 25);
            this.label12.TabIndex = 34;
            this.label12.Text = "512x424 pixels";
            // 
            // videoDir
            // 
            this.videoDir.Location = new System.Drawing.Point(194, 38);
            this.videoDir.Name = "videoDir";
            this.videoDir.ReadOnly = true;
            this.videoDir.Size = new System.Drawing.Size(271, 26);
            this.videoDir.TabIndex = 35;
            // 
            // frameProgress
            // 
            this.frameProgress.Location = new System.Drawing.Point(51, 781);
            this.frameProgress.MarqueeAnimationSpeed = 1;
            this.frameProgress.Name = "frameProgress";
            this.frameProgress.Size = new System.Drawing.Size(1803, 23);
            this.frameProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.frameProgress.TabIndex = 36;
            // 
            // frameDate
            // 
            this.frameDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frameDate.Location = new System.Drawing.Point(51, 810);
            this.frameDate.Name = "frameDate";
            this.frameDate.ReadOnly = true;
            this.frameDate.Size = new System.Drawing.Size(566, 35);
            this.frameDate.TabIndex = 37;
            // 
            // Playback
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1902, 1028);
            this.Controls.Add(this.frameDate);
            this.Controls.Add(this.frameProgress);
            this.Controls.Add(this.videoDir);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.pictureRGB);
            this.Controls.Add(this.pictureDepth);
            this.Controls.Add(this.chooseDir);
            this.Controls.Add(this.btnPlay);
            this.Location = new System.Drawing.Point(250, 250);
            this.MaximumSize = new System.Drawing.Size(5000, 5000);
            this.MinimumSize = new System.Drawing.Size(1918, 1018);
            this.Name = "Playback";
            this.Padding = new System.Windows.Forms.Padding(25);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Playback";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureRGB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button chooseDir;
        private System.Windows.Forms.PictureBox pictureDepth;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.PictureBox pictureRGB;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox videoDir;
        private System.Windows.Forms.ProgressBar frameProgress;
        private System.Windows.Forms.TextBox frameDate;
    }
}

