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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.cancelBgdBtn = new System.Windows.Forms.Button();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.backRBtn = new System.Windows.Forms.RadioButton();
            this.noBackRBtn = new System.Windows.Forms.RadioButton();
            this.objTrackRBtn = new System.Windows.Forms.RadioButton();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.depthCheckBox = new System.Windows.Forms.CheckBox();
            this.rgbCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rgbCheckBox2 = new System.Windows.Forms.CheckBox();
            this.depthCheckBox2 = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.intRbtn = new System.Windows.Forms.RadioButton();
            this.extRbtn = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button1.BackColor = System.Drawing.Color.Chartreuse;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(513, 22);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(10);
            this.button1.Size = new System.Drawing.Size(80, 55);
            this.button1.TabIndex = 0;
            this.button1.Text = "Play";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button2
            // 
            this.button2.AutoSize = true;
            this.button2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button2.Location = new System.Drawing.Point(32, 21);
            this.button2.Name = "button2";
            this.button2.Padding = new System.Windows.Forms.Padding(10);
            this.button2.Size = new System.Drawing.Size(147, 55);
            this.button2.TabIndex = 1;
            this.button2.Text = "Choose File";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btn_ChooseFile_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.AccessibleDescription = "";
            this.pictureBox1.Location = new System.Drawing.Point(51, 115);
            this.pictureBox1.MinimumSize = new System.Drawing.Size(600, 450);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(800, 450);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // chart1
            // 
            chartArea1.AxisX.Title = "Time (s)";
            chartArea1.AxisY.Title = "Distance (mm)";
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Location = new System.Drawing.Point(32, 1025);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(1822, 348);
            this.chart1.TabIndex = 2;
            this.chart1.Text = "chart1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(171, 884);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 26);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = "0";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(48, 924);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 26);
            this.textBox2.TabIndex = 8;
            this.textBox2.Text = "0";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(292, 924);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 26);
            this.textBox3.TabIndex = 9;
            this.textBox3.Text = "0";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(171, 964);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 26);
            this.textBox4.TabIndex = 10;
            this.textBox4.Text = "0";
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cancelBgdBtn
            // 
            this.cancelBgdBtn.AutoSize = true;
            this.cancelBgdBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cancelBgdBtn.Location = new System.Drawing.Point(314, 70);
            this.cancelBgdBtn.Name = "cancelBgdBtn";
            this.cancelBgdBtn.Size = new System.Drawing.Size(170, 60);
            this.cancelBgdBtn.TabIndex = 11;
            this.cancelBgdBtn.Text = "Background";
            this.cancelBgdBtn.UseVisualStyleBackColor = true;
            this.cancelBgdBtn.Click += new System.EventHandler(this.cancelBgdBtn_Click);
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            // 
            // backRBtn
            // 
            this.backRBtn.Checked = true;
            this.backRBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.backRBtn.Location = new System.Drawing.Point(18, 25);
            this.backRBtn.Name = "backRBtn";
            this.backRBtn.Padding = new System.Windows.Forms.Padding(5);
            this.backRBtn.Size = new System.Drawing.Size(255, 55);
            this.backRBtn.TabIndex = 10;
            this.backRBtn.TabStop = true;
            this.backRBtn.Text = "With Background";
            this.backRBtn.UseVisualStyleBackColor = true;
            // 
            // noBackRBtn
            // 
            this.noBackRBtn.Enabled = false;
            this.noBackRBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.noBackRBtn.Location = new System.Drawing.Point(18, 75);
            this.noBackRBtn.Name = "noBackRBtn";
            this.noBackRBtn.Padding = new System.Windows.Forms.Padding(5);
            this.noBackRBtn.Size = new System.Drawing.Size(278, 50);
            this.noBackRBtn.TabIndex = 13;
            this.noBackRBtn.Text = "Remove Background";
            this.noBackRBtn.UseVisualStyleBackColor = true;
            // 
            // objTrackRBtn
            // 
            this.objTrackRBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.objTrackRBtn.Location = new System.Drawing.Point(18, 131);
            this.objTrackRBtn.Name = "objTrackRBtn";
            this.objTrackRBtn.Padding = new System.Windows.Forms.Padding(5);
            this.objTrackRBtn.Size = new System.Drawing.Size(213, 41);
            this.objTrackRBtn.TabIndex = 14;
            this.objTrackRBtn.Text = "Object Tracking";
            this.objTrackRBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.objTrackRBtn.UseVisualStyleBackColor = true;
            // 
            // textBox5
            // 
            this.textBox5.Enabled = false;
            this.textBox5.Location = new System.Drawing.Point(367, 37);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 26);
            this.textBox5.TabIndex = 15;
            this.textBox5.Text = "10";
            this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(195, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 20);
            this.label3.TabIndex = 16;
            this.label3.Text = "Sampling Frequency";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(757, 22);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(80, 26);
            this.textBox6.TabIndex = 17;
            this.textBox6.Text = "0";
            this.textBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(757, 69);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(80, 26);
            this.textBox7.TabIndex = 18;
            this.textBox7.Text = "0";
            this.textBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(177, 861);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 20);
            this.label4.TabIndex = 19;
            this.label4.Text = "Top Margin";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(53, 901);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 20);
            this.label5.TabIndex = 20;
            this.label5.Text = "Left Margin";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(292, 901);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 20);
            this.label6.TabIndex = 21;
            this.label6.Text = "Right Margin";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(166, 941);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 20);
            this.label7.TabIndex = 22;
            this.label7.Text = "Bottom Margin";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(657, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 20);
            this.label8.TabIndex = 23;
            this.label8.Text = "Start Time";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(663, 72);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 20);
            this.label9.TabIndex = 24;
            this.label9.Text = "End Time";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(925, 114);
            this.pictureBox2.MaximumSize = new System.Drawing.Size(1000, 800);
            this.pictureBox2.MinimumSize = new System.Drawing.Size(600, 600);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(929, 600);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 25;
            this.pictureBox2.TabStop = false;
            // 
            // depthCheckBox
            // 
            this.depthCheckBox.AutoSize = true;
            this.depthCheckBox.Enabled = false;
            this.depthCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.depthCheckBox.Location = new System.Drawing.Point(252, 797);
            this.depthCheckBox.Name = "depthCheckBox";
            this.depthCheckBox.Size = new System.Drawing.Size(90, 29);
            this.depthCheckBox.TabIndex = 26;
            this.depthCheckBox.Text = "Depth";
            this.depthCheckBox.UseVisualStyleBackColor = true;
            // 
            // rgbCheckBox
            // 
            this.rgbCheckBox.AutoSize = true;
            this.rgbCheckBox.Enabled = false;
            this.rgbCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.rgbCheckBox.Location = new System.Drawing.Point(388, 797);
            this.rgbCheckBox.Name = "rgbCheckBox";
            this.rgbCheckBox.Size = new System.Drawing.Size(79, 29);
            this.rgbCheckBox.TabIndex = 27;
            this.rgbCheckBox.Text = "RGB";
            this.rgbCheckBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(156, 798);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 25);
            this.label1.TabIndex = 28;
            this.label1.Text = "File:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(1263, 797);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 25);
            this.label2.TabIndex = 31;
            this.label2.Text = "Play:";
            // 
            // rgbCheckBox2
            // 
            this.rgbCheckBox2.AutoSize = true;
            this.rgbCheckBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.rgbCheckBox2.Location = new System.Drawing.Point(1495, 796);
            this.rgbCheckBox2.Name = "rgbCheckBox2";
            this.rgbCheckBox2.Size = new System.Drawing.Size(79, 29);
            this.rgbCheckBox2.TabIndex = 30;
            this.rgbCheckBox2.Text = "RGB";
            this.rgbCheckBox2.UseVisualStyleBackColor = true;
            // 
            // depthCheckBox2
            // 
            this.depthCheckBox2.AutoSize = true;
            this.depthCheckBox2.Checked = true;
            this.depthCheckBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.depthCheckBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.depthCheckBox2.Location = new System.Drawing.Point(1359, 796);
            this.depthCheckBox2.Name = "depthCheckBox2";
            this.depthCheckBox2.Size = new System.Drawing.Size(90, 29);
            this.depthCheckBox2.TabIndex = 29;
            this.depthCheckBox2.Text = "Depth";
            this.depthCheckBox2.UseVisualStyleBackColor = true;
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
            this.label12.Location = new System.Drawing.Point(554, 796);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(143, 25);
            this.label12.TabIndex = 34;
            this.label12.Text = "512x424 pixels";
            // 
            // intRbtn
            // 
            this.intRbtn.AutoSize = true;
            this.intRbtn.Checked = true;
            this.intRbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.intRbtn.Location = new System.Drawing.Point(21, 40);
            this.intRbtn.Name = "intRbtn";
            this.intRbtn.Size = new System.Drawing.Size(180, 29);
            this.intRbtn.TabIndex = 35;
            this.intRbtn.TabStop = true;
            this.intRbtn.Text = "Internal Location";
            this.intRbtn.UseVisualStyleBackColor = true;
            // 
            // extRbtn
            // 
            this.extRbtn.AutoSize = true;
            this.extRbtn.Enabled = false;
            this.extRbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.extRbtn.Location = new System.Drawing.Point(250, 40);
            this.extRbtn.Name = "extRbtn";
            this.extRbtn.Size = new System.Drawing.Size(187, 29);
            this.extRbtn.TabIndex = 36;
            this.extRbtn.Text = "External Location";
            this.extRbtn.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.intRbtn);
            this.groupBox1.Controls.Add(this.extRbtn);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox1.Location = new System.Drawing.Point(925, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(522, 100);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "File Load Location";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.backRBtn);
            this.groupBox2.Controls.Add(this.noBackRBtn);
            this.groupBox2.Controls.Add(this.objTrackRBtn);
            this.groupBox2.Controls.Add(this.cancelBgdBtn);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.groupBox2.Location = new System.Drawing.Point(473, 838);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(536, 181);
            this.groupBox2.TabIndex = 38;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "File Play Mode";
            // 
            // Playback
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1912, 1038);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rgbCheckBox2);
            this.Controls.Add(this.depthCheckBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rgbCheckBox);
            this.Controls.Add(this.depthCheckBox);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Location = new System.Drawing.Point(250, 250);
            this.MaximumSize = new System.Drawing.Size(5000, 5000);
            this.MinimumSize = new System.Drawing.Size(1918, 1022);
            this.Name = "Playback";
            this.Padding = new System.Windows.Forms.Padding(25);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Playback";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button cancelBgdBtn;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.RadioButton backRBtn;
        private System.Windows.Forms.RadioButton noBackRBtn;
        private System.Windows.Forms.RadioButton objTrackRBtn;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.CheckBox depthCheckBox;
        private System.Windows.Forms.CheckBox rgbCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox rgbCheckBox2;
        private System.Windows.Forms.CheckBox depthCheckBox2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RadioButton intRbtn;
        private System.Windows.Forms.RadioButton extRbtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}

