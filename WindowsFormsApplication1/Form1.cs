using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Kinect;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApplication1
{
    public partial class Playback : Form
    {
        //Declare variables
        private WriteableBitmap depthBitmap = null;
        private WriteableBitmap colorBitmap = null;

        //File/play parameters
        private static int freqS;
        private static double startTime;
        private static double endTime;

        //File flags and counters
        private static double count = 0;
        private static bool playing = false;
        private static bool oldFileFlag = false;

        //File readers and info
        private string chosenFile = "";
        private static BinaryReader reader;
        private static FileStream SourceStream;

        //Kinect frame info
        private static int depthWidth = 512;
        private static int depthHeight = 424;
        private static int colorWidth = 1920;
        private static int colorHeight = 1080;
        private static int minKinectDepth = 500;
        private static int maxKinectDepth = 4500;

        //File length info
        private static double length;
        private static double numFrames;
        private static int depthFrameLength = depthHeight * depthWidth;
        private static int colorFrameLength = colorHeight * colorWidth;
        private static int frameSize = depthFrameLength*2 + 4*2; //+4 for time fields

        //Margins
        private static int leftMarOff = 0;
        private static int rightMarOff = 0;
        private static int topMarOff = 0;
        private static int botMarOff = 0;

        //Temporary storage arrays
        private static ushort[] depthValuesCurr;
        private static ushort[] depthValuesPrev;
        private static int[] backgroundDepthLevel;
        private static int[] backgroundRGBLevel;
        private static byte[] colorPixels;

        /// <summary>
        /// Initialize form
        /// </summary>
        public Playback()
        {
            InitializeComponent();

            //Check if external directory exists, enable external save location
            if (Directory.Exists("D:\\Kinect Data"))
            {
                extRbtn.Enabled = true;
                extRbtn.Checked = true;
            }
        }

        /// <summary>
        /// Handle play/pause button click
        /// </summary>
        private async void btnPlay_Click(object sender, EventArgs e)
        {
            //Toggle playing
            playing = !playing;

            //If playing
            if (playing == true)
            {
                //Try if file exists, else return void with error message
                try
                {
                    reader.PeekChar();
                }
                catch
                {
                    System.Windows.MessageBox.Show("No File Chosen. Please choose a file");
                    return;
                }

                //Check file compatability with reader 
                if (depthCheckBox.Checked == false && depthCheckBox2.Checked == true || rgbCheckBox.Checked == false && rgbCheckBox2.Checked == true)
                {
                    System.Windows.MessageBox.Show("Not correct file type or play type. Please Check.");
                    return;
                }

                //Chcek if file or play type chosen
                if (depthCheckBox.Checked == false && rgbCheckBox.Checked == false || depthCheckBox2.Checked == false && rgbCheckBox2.Checked == false)
                {
                    System.Windows.MessageBox.Show("File or play type not chosen.");
                    return;
                }

                //Chnage status and disable margin boxes
                button1.Text = "Pause";
                this.button1.BackColor = System.Drawing.Color.Crimson;
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;

                //If first play
                if (count == 0)
                {
                    //Determine frame size based on file type
                    if (depthCheckBox.Checked && rgbCheckBox.Checked)
                    {
                        frameSize = depthFrameLength * 2 + colorFrameLength + 4 * 2;
                    }
                    else
                    {
                        if (depthCheckBox.Checked)
                        {
                            frameSize = depthFrameLength * 2 + 4 * 2;
                        }
                        if (rgbCheckBox.Checked)
                        {
                            frameSize = colorFrameLength + 4 * 2;
                        }
                    }
                    
                    //Get file length
                    length = (double)reader.BaseStream.Length;

                    //If file length is not a multiple of determined frame size,
                    //Show error, toggle play, change play button text, and re-enable margins
                    //Then return
                    if (length % frameSize != 0)
                    {
                        System.Windows.MessageBox.Show("Not correct file type chosen. Please check.");
                        playing = !playing;
                        button1.Text = "Play";
                        this.button1.BackColor = System.Drawing.Color.Chartreuse;
                        textBox1.Enabled = true;
                        textBox2.Enabled = true;
                        textBox3.Enabled = true;
                        textBox4.Enabled = true;
                        return;
                    }       
                    
                    //Find number of frames in file        
                    numFrames = length / frameSize;

                    //Initialize/reset depth value array
                    depthValuesPrev = new ushort[depthFrameLength];

                    //Get sampling frequency and start time
                    freqS = Int32.Parse(textBox5.Text);
                    startTime = Double.Parse(textBox6.Text);

                    //Convert start time to start frame
                    startTime = startTime * freqS;

                    //If not at start frame
                    if (startTime != 0)
                    {
                        //Read through file to get to start frame
                        while (count < startTime)
                        {
                            short hour = reader.ReadInt16();
                            short min = reader.ReadInt16();
                            short sec = reader.ReadInt16();
                            short msec = reader.ReadInt16();

                            //Play through frame depending on which file type checked
                            if (depthCheckBox.Checked)
                            {
                                for (int j = 0; j < depthFrameLength; j++)
                                {
                                    reader.ReadUInt16();
                                }
                            }
                            if (rgbCheckBox.Checked)
                            {
                                for (int k = 0; k < colorFrameLength; k++)
                                {
                                    reader.ReadByte();
                                }
                            }

                            //Increase counter by 1
                            count += 1;
                        }
                    }

                    //Get end time
                    endTime = Double.Parse(textBox7.Text);

                    //If end time is not 0 (0 is default end time)
                    if (endTime != 0)
                    {
                        //change frame limit of play file
                        numFrames = (double)(endTime * freqS) + 1;
                    }

                    //Disable start and end times until file changed
                    textBox6.Enabled = false;
                    textBox7.Enabled = false;
                }

                //while playing and count is not at end
                while (playing && count < numFrames)
                {
                    //Reset/Initialize value arrays
                    depthValuesCurr = new ushort[depthFrameLength];
                    colorPixels = new byte[colorFrameLength];
                    int[] noBack = new int[depthFrameLength];
                    int[] objTrack = new int[depthFrameLength];

                    //Reset frame level
                    double frameLevel = 0;

                    //Get time at beginning of each frame
                    short hour = reader.ReadInt16();
                    short min = reader.ReadInt16();
                    short sec = reader.ReadInt16();
                    short msec = reader.ReadInt16();

                    //If file type is depth
                    if (depthCheckBox.Checked)
                    {
                        for (int j = 0; j < depthFrameLength; j++)
                        {
                            //Read depth and store in current array
                            ushort depth = reader.ReadUInt16();
                            depthValuesCurr[j] = depth;

                            //If cancel background checked, subtract background level
                            if (noBackRBtn.Checked)
                            {
                                noBack[j] = backgroundDepthLevel[j] - depthValuesCurr[j];
                            }

                            //Don't get first frame
                            //If object tracking checked
                            if (count > 0 && objTrackRBtn.Checked)// && count % 20 == 0) //Can change how often object tracked
                            {
                                //Subtract from previous frame or from how long ago needed 
                                //objTrack[j] = depthValues[count - 20][j] - depthValues[count][j];
                                objTrack[j] = depthValuesPrev[j] - depthValuesCurr[j];

                                //Change sensitivty
                                /*if (objTrack[j] < 200)
                                {
                                    objTrack[j] = 0;
                                }
                                if (objTrack[j] > ) //If greater than threshold, saturate
                                {
                                    objTrack[j] = 255;
                                }*/

                                //Invert colors
                                objTrack[j] = 255 - objTrack[j];
                            }
                        }

                        //If depth play type
                        if (depthCheckBox2.Checked == true)
                        {
                            //Calculate frame level and display after first frame
                            frameLevel = calcLevel(depthHeight, depthWidth, depthValuesCurr);
                            if (count > 0)
                            {
                                plotKinectDepth(count, frameLevel);
                            }

                            //Determine frame to draw: normal, no background, object tracking
                            if (backRBtn.Checked)
                            {
                                this.RenderDepthPixels(normalizeDepth(depthValuesCurr, minKinectDepth, maxKinectDepth));
                            }
                            if (noBackRBtn.Checked)
                            {
                                this.RenderDepthPixels(normalizeDepthInt(noBack, noBack.Min(), noBack.Max()));
                            }
                            //if (count > 0 && objTrackCheck == true)// && count % 20 == 0) // Or chnage based on time interval
                            if (count > 0 && objTrackRBtn.Checked)
                            {
                                this.RenderDepthPixels(normalizeDepthInt(objTrack, objTrack.Min(), objTrack.Max()));
                                //this.RenderDepthPixels(normalizeDepthInt(objTrack, 0, 255));
                            }

                            //Refresh picture box to draw image
                            pictureBox1.Refresh();
                        }
                        else //Else, clear picture
                        {
                            this.pictureBox1.Image = null;
                        }
                    }

                    //if color file type
                    if (rgbCheckBox.Checked)
                    {
                        //read color frame
                        for (int k = 0; k < colorFrameLength; k++)
                        {
                            colorPixels[k] = reader.ReadByte();
                        }

                        //if color play checked
                        if (rgbCheckBox2.Checked == true)
                        {
                            //if no depth, draw 0 depth to keep track of time
                            if (!depthCheckBox2.Checked)
                            {
                                frameLevel = calcLevel(depthHeight, depthWidth, depthValuesCurr);
                                if (count > 0)
                                {
                                    plotKinectDepth(count, frameLevel);
                                }
                            }

                            //Draw color frame
                            this.RenderColorPixels(colorPixels);
                            pictureBox2.Refresh();
                        }
                        else //Else, clear picture
                        {
                            this.pictureBox2.Image = null;
                        }
                    }

                    //Increase frame counter and dive time to update
                    count += 1;
                    await Task.Delay(100);

                    //If done playing
                    if (count == numFrames)
                    {
                        //Toggle play, re-enable text boxes, and change button text
                        playing = !playing;
                        textBox1.Enabled = true;
                        textBox2.Enabled = true;
                        textBox3.Enabled = true;
                        textBox4.Enabled = true;
                        textBox6.Enabled = true;
                        textBox7.Enabled = true;
                        button1.Text = "Play";
                        this.button1.BackColor = System.Drawing.Color.Chartreuse;
                    }

                    //Copy current depth values to previous
                    depthValuesPrev = depthValuesCurr;
                }

                //if done playing, display completion messasge and close reader
                if (count == numFrames)
                {
                    System.Windows.MessageBox.Show("Finished!");
                    reader.Close();
                }
            }
            //Else not playing, re-enable text boxes and change button text
            else
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox6.Enabled = true;
                textBox7.Enabled = true;
                button1.Text = "Play";
                this.button1.BackColor = System.Drawing.Color.Chartreuse;
            }
        }

        /// <summary>
        /// Normalize depth data to min and max
        /// </summary>
        /// <param name="depth"> Depth Array to normalize </param>
        /// <param name="minDepth"> Minimum depth to normalize to </param>
        /// <param name="maxDepth"> Maximum depth to normalize to </param>
        /// <returns> Normalized depth value array </returns>
        byte[] normalizeDepth(ushort[] depth, int minDepth, int maxDepth)
        {
            byte[] arr = new byte[depthFrameLength];
            int temp;

            for (int i = 0; i < depthFrameLength; i++)
            {
                temp = 255 * (depth[i] - minDepth) / (maxDepth - minDepth);
                if (temp < 0)
                    temp = 0;
                else if (temp > 255)
                    temp = 255;

                arr[i] = (byte)temp;
            }
            return arr;
        }

        /// <summary>
        /// Same as NormalizeDepth but for integers (due to subtraction beforehand)
        /// </summary>
        /// <param name="depth"> Depth Array to normalize </param>
        /// <param name="minDepth"> Minimum depth to normalize to </param>
        /// <param name="maxDepth"> Maximum depth to normalize to </param>
        /// <returns> Normalized depth value array </returns>
        byte[] normalizeDepthInt(int[] depth, int minDepth, int maxDepth)
        {
            byte[] arr = new byte[depthFrameLength];
            int temp;

            for (int i = 0; i < depthFrameLength; i++)
            {
                temp = 255 * (depth[i] - minDepth) / (maxDepth - minDepth);
                if (temp < 0)
                    temp = 0;
                else if (temp > 255)
                    temp = 255;

                arr[i] = (byte)temp;
            }
            return arr;
        }

        /// <summary>
        /// Handle choose file click
        /// </summary>
        private void btn_ChooseFile_Click(object sender, EventArgs e)
        {
            //Initialize open file parameters

            //Initial Directory
            //openFileDialog1.InitialDirectory = "C:\\Airway_Resistance_2015\\Airway_Data_2015\\Kinect Data\\";
            //openFileDialog1.InitialDirectory = "C:\\Users\\AC lab\\Desktop\\Kinect Apps\\RecorderFinal\\Sample Kinect Data";
            //openFileDialog1.InitialDirectory = Directory.GetDirectories(Directory.GetDirectories(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString()).ToString()).ToString(), "Record*")[0], "Sample*")[0];
            //openFileDialog1.InitialDirectory = Directory.GetDirectories(Directory.GetDirectories(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString()).ToString()).ToString(), "Record*")[0], "Kinect*")[0];

            if (intRbtn.Checked == true)
            {
                openFileDialog1.InitialDirectory = Directory.GetDirectories(Directory.GetDirectories(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString()).ToString()).ToString(), "*D Recorder")[0], "Kinect*")[0];
            }
            if (extRbtn.Checked == true)
            {
                openFileDialog1.InitialDirectory = "D:\\Kinect Data\\";
            }

            openFileDialog1.Title = "Open a Cilp"; //Window Title
            openFileDialog1.FileName = ""; //Initialize null file
            //Filter for binary files
            openFileDialog1.Filter = "Binary Files (.BIN; .md6; .md7)| *.BIN; *.md6; *.md7";

            //Open choose file window
            openFileDialog1.ShowDialog();

            //Selected file is chosen file
            chosenFile = openFileDialog1.FileName;

            //Enable play type boxes for user to choose
            rgbCheckBox2.Enabled = true;
            depthCheckBox2.Enabled = true;

            //Try if file exists (mainly catches window cancel), returns
            try
            {
                SourceStream = File.Open(chosenFile, FileMode.OpenOrCreate, FileAccess.Read);
                reader = new BinaryReader(SourceStream);
                reader.PeekChar();
            }
            catch
            {
                return;
            }

            //If not sure if older file
            if (oldFileFlag == false)
            {
                //Split file name
                string[] strSplit = chosenFile.ToString().Split('_');

                //Try to see if file recorded with newer version of recorder
                try
                {
                    string[] strFileType = null;
                    //Get file type
                    if (intRbtn.Checked == true)
                    {
                        strFileType = strSplit[5].Split('.');
                    }
                    if (extRbtn.Checked == true)
                    {
                        strFileType = strSplit[4].Split('.');
                    }

                    //If both file type, allow both play types
                    if (strFileType[0] == "depthRGB")
                    {
                        depthCheckBox.Checked = true;
                        rgbCheckBox.Checked = true;
                        depthCheckBox2.Checked = true;
                        rgbCheckBox2.Checked = true;
                    }
                    //If depth file type, allow only depth play and disable other file/play types
                    if (strFileType[0] == "depth")
                    {
                        depthCheckBox.Checked = true;
                        depthCheckBox2.Checked = true;
                        rgbCheckBox2.Checked = false;
                        depthCheckBox2.Enabled = false;
                        rgbCheckBox2.Enabled = false;
                    }
                    //If color file type, allow only color play and disable other file/play types
                    if (strFileType[0] == "rgb")
                    {
                        rgbCheckBox.Checked = true;
                        rgbCheckBox2.Checked = true;
                        depthCheckBox2.Checked = false;
                        rgbCheckBox2.Enabled = false;
                        depthCheckBox2.Enabled = false;
                    }

                    //Try to get sampling frequency (based on recorder version)
                    try
                    {
                        string[] strSampFreq = null;
                        //Get file type
                        if (intRbtn.Checked == true)
                        {
                            strSampFreq = strSplit[4].Split('.');
                        }
                        if (extRbtn.Checked == true)
                        {
                            strSampFreq = strSplit[3].Split('.');
                        }
                        string strSampFreqNum = strSampFreq[0].Substring(0, 2);
                        Int32.Parse(strSampFreqNum);
                        textBox5.Text = strSampFreqNum;
                    }
                    //Else assume 10 Hz
                    catch
                    {
                        System.Windows.MessageBox.Show("Assumed 10 Hz Sampling Frequency.");
                        textBox5.Text = "10";
                    }
                }
                //Else is file from older recorder
                catch
                {
                    //Allow user to choose options
                    System.Windows.MessageBox.Show("Old File Version. Please check which file type. Then press play");
                    depthCheckBox.Enabled = true;
                    rgbCheckBox.Enabled = true;
                    depthCheckBox.Checked = false;
                    depthCheckBox2.Checked = false;
                    rgbCheckBox.Checked = false;
                    rgbCheckBox2.Checked = false;
                    oldFileFlag = true;

                    //Try to get sampling frequency (based on recorder version)
                    try
                    {
                        string[] strSampFreq = strSplit[3].Split('.');
                        string strSampFreqNum = strSampFreq[0].Substring(0,2);
                        Int32.Parse(strSampFreqNum);
                        textBox5.Text = strSampFreqNum;
                    }
                    //Else is file from older recorder
                    catch
                    {
                        System.Windows.MessageBox.Show("Assumed 10 Hz Sampling Frequency.");
                        textBox5.Text = "10";
                    }
                }
                backRBtn.Checked = true;
            }
            //If older file chosen
            else
            {
                //Reset flag and enable play types
                oldFileFlag = false;
                depthCheckBox.Enabled = false;
                rgbCheckBox.Enabled = false;
            }

            //If file already playing
            if (count > 0)
            {
                //Reset counter
                count = 0;

                //Reset images and plot
                this.chart1.Series["Series1"].Points.Clear();
                this.pictureBox1.Image = null;
                this.pictureBox2.Image = null;

                //Reset margins
                textBox1.Text = "0";
                textBox2.Text = "0";
                textBox3.Text = "0";
                textBox4.Text = "0";

                //Reset sampling frequency, start, and end time
                textBox5.Text = "10";
                textBox6.Text = "0";
                textBox7.Text = "0";
            }
        }

        /// <summary>
        /// Convert writeable bitmap to bitmap to draw
        /// </summary>
        /// <param name="writeBmp"> Writeable bitmap to convert</param>
        /// <returns></returns>
        private System.Drawing.Bitmap BitmapFromWriteableBitmap(WriteableBitmap writeBmp)
        {
            System.Drawing.Bitmap bmp;

            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create((BitmapSource)writeBmp));
                enc.Save(outStream);
                bmp = new System.Drawing.Bitmap(outStream);
            }
            return bmp;
        }

        delegate void RenderDepthPixelsDelegate(byte[] arr);

        /// <summary>
        /// Renders depth pixels into the image to display
        /// </summary>
        /// <param name="arr"> Depth array to be drawn </param>
        private void RenderDepthPixels(byte[] arr)
        {
            depthBitmap = new WriteableBitmap(512, 424, 96.0, 96.0, PixelFormats.Gray8, null);

            if (this.InvokeRequired)
            {
                RenderDepthPixelsDelegate render_delegate = RenderDepthPixels;
                this.Invoke(render_delegate);
                return;
            }
            else
            {
                depthBitmap.WritePixels(
                   new Int32Rect(0, 0, depthBitmap.PixelWidth, depthBitmap.PixelHeight),
                   arr,
                   depthBitmap.PixelWidth,
                   0);

                this.pictureBox1.Image = BitmapFromWriteableBitmap(depthBitmap);
            }
        }

        /// <summary>
        /// Calculate average level within margins 
        /// </summary>
        /// <param name="height"> Frame height </param>
        /// <param name="width"> Frame width </param>
        /// <param name="arr"> Array to calculate from </param>
        /// <returns></returns>
        private double calcLevel(int height, int width, ushort[] arr)
        {
            double level, val;
            int x, y, idx;

            level = 0;
            idx = 0;
            for (y = 0; y < height; y++)
            {
                //If within y margins
                if (y < topMarOff || y > (height - botMarOff))
                    continue;

                for (x = 0; x < width; x++)
                {
                    //If within x margins
                    if (x < leftMarOff || x > (width - rightMarOff))
                        continue;

                    //Determine index
                    idx = x + y * width;

                    //Sum value
                    val = arr[idx];
                    level += val;
                }
            }

            //Average level over area of interest
            level = level / (width - leftMarOff - rightMarOff) / (height - topMarOff - botMarOff);

            return level;
        }

        private delegate void plotKinectDepthDelegate(double x, double y);

        /// <summary>
        /// Plot depth distance vs time based on sampling frequency
        /// </summary>
        /// <param name="x"> Time </param>
        /// <param name="y"> Depth Distance </param>
        void plotKinectDepth(double x, double y)
        {
            if (this.InvokeRequired)
            {
                object[] args = new object[] { x, y };
                plotKinectDepthDelegate plot_kinect_delegate = plotKinectDepth;
                this.Invoke(plot_kinect_delegate, args);
                return;
            }
            else
            {
                freqS = Int32.Parse(textBox5.Text);
                x = x / freqS;
                chart1.Series["Series1"].Points.AddXY(x, y);
                chart1.Series["Series1"].ChartType = SeriesChartType.Spline;
                chart1.ChartAreas[0].AxisY.IsStartedFromZero = false;
            }

        }

        /// <summary>
        /// If margin changes, draw new margins at specified locations
        /// </summary>
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (playing == true)
            {
                if (
                    Int32.Parse(textBox2.Text) != leftMarOff
                    || Int32.Parse(textBox3.Text) != rightMarOff
                    || Int32.Parse(textBox1.Text) != topMarOff
                    || Int32.Parse(textBox4.Text) != botMarOff)
                {
                    this.chart1.Series["Series1"].Points.Clear();
                }

                leftMarOff = Int32.Parse(textBox2.Text);
                rightMarOff = Int32.Parse(textBox3.Text);
                topMarOff = Int32.Parse(textBox1.Text);
                botMarOff = Int32.Parse(textBox4.Text);

                Rectangle leftMar = new Rectangle(leftMarOff, 0, 5, 424);
                Rectangle rightMar = new Rectangle(512 - rightMarOff, 0, 5, 428);
                Rectangle topMar = new Rectangle(0, topMarOff, 512, 5);
                Rectangle botMar = new Rectangle(0, 424 - botMarOff, 516, 5);
                using (System.Drawing.Brush brush = new System.Drawing.SolidBrush(System.Drawing.Color.Red))
                {
                    e.Graphics.FillRectangle(brush, leftMar);
                    e.Graphics.FillRectangle(brush, rightMar);
                    e.Graphics.FillRectangle(brush, topMar);
                    e.Graphics.FillRectangle(brush, botMar);
                }
            }
        }

        /// <summary>
        /// Average individual pixels over time of a frame
        /// </summary>
        /// <param name="arr"> Array to process </param>
        /// <returns> Averaged frame </returns>
        public int[] averageBackground(int[][] arr)
        {
            //Get frame size and number of frames
            int L = arr[0].Length; //Frame Size
            int N = arr.Length; //Number of Frames

            int[] avg = new int[L];
            for (int i = 0; i < L; i++)
            {
                int sum = 0;
                for (int j = 0; j < N; j++)
                {
                    sum += arr[j][i];
                }
                avg[i] = sum / N;
            }
            return avg;
        }

        /// <summary>
        /// Handle background file to use for removal, similar to choose file
        /// Not fully debugged for color background removal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelBgdBtn_Click(object sender, EventArgs e)
        {
            //Save check box status
            bool depth1 = depthCheckBox.Checked;
            bool depth2 = depthCheckBox2.Checked;
            bool rgb1 = rgbCheckBox.Checked;
            bool rgb2 = rgbCheckBox2.Checked;

            //Initial directory
            //openFileDialog2.InitialDirectory = "C:\\Airway_Resistance_2015\\Airway_Data_2015\\Kinect Data\\";
            //openFileDialog2.InitialDirectory = "C:\\Users\\AC lab\\Desktop\\Kinect Apps\\RecorderFinal\\Sample Kinect Data";
            //openFileDialog2.InitialDirectory = Directory.GetDirectories(Directory.GetDirectories(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString()).ToString()).ToString(), "Record*")[0], "Sample*")[0];
            if (intRbtn.Checked == true)
            {
                openFileDialog2.InitialDirectory = Directory.GetDirectories(Directory.GetDirectories(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString()).ToString()).ToString(), "Record*")[0], "Kinect*")[0];
            }
            if (extRbtn.Checked == true)
            {
                openFileDialog2.InitialDirectory = "D:\\Kinect Data\\";
            }

            openFileDialog2.Title = "Open a Background Cilp"; //Window Title
            openFileDialog2.FileName = ""; //Null file to initialize
            //Binary file filter
            openFileDialog2.Filter = "Binary Files (.BIN; .md6; .md7)| *.BIN; *.md6; *.md7";

            //Open choose file window
            openFileDialog2.ShowDialog();

            //Set filename to selected file
            string chosenBgFile = openFileDialog2.FileName;

            //Initialize reader
            BinaryReader bgReader;

            //Try if file exists (mainly catches window cancel), returns
            try
            {
                FileStream SourceStream2 = File.Open(chosenBgFile, FileMode.OpenOrCreate, FileAccess.Read);
                bgReader = new BinaryReader(SourceStream2);
                bgReader.PeekChar();
            }
            catch
            {
                return;
            }

            //If not sure if older file
            if (oldFileFlag == false)
            {
                //Split file name
                string[] strSplit = chosenBgFile.ToString().Split('_');

                //Try to see if file recorded with newer version of recorder
                try
                {
                    //Get file type
                    string[] strFileType = null;
                    if (intRbtn.Checked == true)
                    {
                        strFileType = strSplit[5].Split('.');
                    }
                    if (extRbtn.Checked == true)
                    {
                        strFileType = strSplit[4].Split('.');
                    }

                    //If both file type, allow both play types
                    if (strFileType[0] == "depthRGB")
                    {
                        depthCheckBox.Checked = true;
                        rgbCheckBox.Checked = true;
                        depthCheckBox2.Checked = false;
                        rgbCheckBox2.Checked = false;
                    }
                    //If depth file type, allow only depth play and disable other file/play types
                    if (strFileType[0] == "depth")
                    {
                        depthCheckBox.Checked = true;
                        rgbCheckBox.Checked = false;
                        depthCheckBox2.Checked = false;
                        rgbCheckBox2.Checked = false;
                    }
                    //If color file type, allow only color play and disable other file/play types
                    if (strFileType[0] == "rgb")
                    {
                        depthCheckBox.Checked = false;
                        rgbCheckBox.Checked = true;
                        depthCheckBox2.Checked = false;
                        rgbCheckBox2.Checked = false;
                    }

                    //Try to get sampling frequency (based on recorder version)
                    try
                    {
                        string[] strSampFreq = strSplit[3].Split('.');
                        string strSampFreqNum = strSampFreq[0].Substring(0, 2);
                        Int32.Parse(strSampFreqNum);
                        textBox5.Text = strSampFreqNum;
                    }
                    //Else assume 10 Hz
                    catch
                    {
                        System.Windows.MessageBox.Show("Assumed 10 Hz Sampling Frequency.");
                        textBox5.Text = "10";
                    }
                }
                //Else is file from older recorder
                catch
                {
                    //Allow user to choose options
                    System.Windows.MessageBox.Show("Old File Version. Please check which file type. Then press play");
                    depthCheckBox.Enabled = true;
                    rgbCheckBox.Enabled = true;
                    rgbCheckBox.Checked = false;
                    oldFileFlag = true;

                    //Try to get sampling frequency (based on recorder version)
                    try
                    {
                        string[] strSampFreq = null;
                        //Get file type
                        if (intRbtn.Checked == true)
                        {
                            strSampFreq = strSplit[4].Split('.');
                        }
                        if (extRbtn.Checked == true)
                        {
                            strSampFreq = strSplit[3].Split('.');
                        }
                        string strSampFreqNum = strSampFreq[0].Substring(0, 2);
                        Int32.Parse(strSampFreqNum);
                        textBox5.Text = strSampFreqNum;
                    }
                    //Else is file from older recorder
                    catch
                    {
                        System.Windows.MessageBox.Show("Assumed 10 Hz Sampling Frequency.");
                        textBox5.Text = "10";
                    }
                }
            }
            //If older file chosen
            else
            {
                //Reset flag and enable play types
                oldFileFlag = false;
                depthCheckBox.Enabled = false;
                rgbCheckBox.Enabled = false;
            }

            //Get file length
            int bgLength = (int)bgReader.BaseStream.Length;

            //Determine frame size based on file type checked
            if (depthCheckBox.Checked && rgbCheckBox.Checked)
            {
                frameSize = depthFrameLength * 2 + colorFrameLength + 4 * 2;
            }
            else
            {
                if (depthCheckBox.Checked)
                {
                    frameSize = depthFrameLength * 2 + 4 * 2;
                }
                if (rgbCheckBox.Checked)
                {
                    frameSize = colorFrameLength + 4 * 2;
                }
            }

            int bgNumFrames = bgLength / frameSize;

            //If file length is not a multiple of determined frame size,
            //Show error, toggle play, change play button text, and re-enable margins
            //Then return
            if (bgLength % frameSize != 0)
            {
                System.Windows.MessageBox.Show("Not correct file type chosen. Please check.");
                return;
            }

            System.Windows.MessageBox.Show("Wait Until Completion Window...");

            //Initialize data storage array
            int[][] depthValues = new int[bgNumFrames][];
            int[][] colorValues = new int[bgNumFrames][];

            //Until file done
            for (int i = 0; i < bgNumFrames; i++)
            {
                depthValues[i] = new int[depthFrameLength];
                colorValues[i] = new int[colorFrameLength];

                //Get time at beginning of each frame
                short hour = bgReader.ReadInt16();
                short min = bgReader.ReadInt16();
                short sec = bgReader.ReadInt16();
                short msec = bgReader.ReadInt16();

                //Read and Store depth
                if (depthCheckBox.Checked)
                {
                    for (int j = 0; j < depthFrameLength; j++)
                    {
                        int depth = bgReader.ReadUInt16();
                        depthValues[i][j] = depth;
                    }
                }
                //Read through color
                if (rgbCheckBox.Checked)
                {
                    for (int k = 0; k < colorFrameLength; k++)
                    {
                        int color = bgReader.ReadByte();
                        colorValues[i][k] = color;
                    }
                }
            }

            //Store background level
            backgroundDepthLevel = averageBackground(depthValues);
            backgroundRGBLevel = averageBackground(colorValues);

            //Show Background
            this.RenderDepthPixels(normalizeDepthInt(backgroundDepthLevel, backgroundDepthLevel.Min(), backgroundDepthLevel.Max()));
            //this.RenderColorPixels(normalizeDepthInt(backgroundRGBLevel, backgroundRGBLevel.Min(), backgroundRGBLevel.Max()));

            //Show completion, re-enable text boxes, and restore check box status
            System.Windows.MessageBox.Show("Background Acquired");
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox6.Enabled = true;
            textBox7.Enabled = true;

            depthCheckBox.Checked = depth1;
            depthCheckBox2.Checked = depth2;
            rgbCheckBox.Checked = rgb1;
            rgbCheckBox2.Checked = rgb2;

            bgReader.Close();

            //Enable remove background mode
            noBackRBtn.Enabled = true;
        }

        delegate void RenderColorPixelsDelegate(byte[] arr);

        /// <summary>
        /// Renders color pixels into the writeableBitmap to draw
        /// </summary>
        /// <param name="arr"> Color array to draw </param>
        private void RenderColorPixels(byte[] arr)
        {
            colorBitmap = new WriteableBitmap(colorWidth, colorHeight, 96.0, 96.0, PixelFormats.Gray8, null);

            if (this.InvokeRequired)
            {
                RenderColorPixelsDelegate render_delegate = RenderColorPixels;
                this.Invoke(render_delegate);
                return;
            }
            else
            {
                colorBitmap.WritePixels(
                   new Int32Rect(0, 0, colorBitmap.PixelWidth, colorBitmap.PixelHeight),
                   arr,
                   colorBitmap.PixelWidth,
                   0);

                this.pictureBox2.Image = BitmapFromWriteableBitmap(colorBitmap);
            }
        }

        /// <summary>
        /// Initial window properties
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            //this.CenterToScreen();
            this.WindowState = FormWindowState.Minimized;
            this.WindowState = FormWindowState.Maximized;
        }
    }
}
