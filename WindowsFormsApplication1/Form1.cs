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

        private RgbdReader _reader;
        private System.Timers.Timer _playbackTimer;
        private bool _paused = true;
        //private DepthCompress _dc;

        private void setFrames()
        {
            if(_reader != null)
            {
                Action act = () =>
                {
                    pictureDepth.Image = _reader.getDepthBitmap();
                    pictureRGB.Image = _reader.getRGBBitmap();

                    //_dc.addFrame(_reader.getDepthValues());

                    if(_reader.size() >= 1)
                    {
                        frameProgress.Maximum = (int)_reader.size() - 1;
                        frameProgress.Minimum = 0;
                        frameProgress.Value = (int)_reader.currentFrame();
                        if (frameProgress.Value < frameProgress.Maximum)
                        {
                            frameProgress.Value++;
                            frameProgress.Value--;
                        }

                    }

                    frameDate.Text = String.Format("{0:MMM d, yyyy HH:mm:ss:ff}", _reader.getTime());
                    this.Refresh();
                };
                pictureDepth.Invoke(act);
            }
        }
        private void playFrame()
        {
            if (_reader != null && !_paused)
            {
                Action act = () => {
                    _reader.advanceFrame();
                    if (_reader.atEnd())
                        setPause(true);
                };
                pictureDepth.Invoke(act);
            }
        }


        /// <summary>
        /// Initialize form
        /// </summary>
        public Playback()
        {
            InitializeComponent();
            _playbackTimer = new System.Timers.Timer(60);
            _playbackTimer.Elapsed += (sender, e) => playFrame();
            _reader = new RgbdReader();
            //_dc = new DepthCompress(512, 424);
            _reader.FrameAdvanced += (sender, e) => setFrames();
            _playbackTimer.Start();
            btnPlay.Enabled = false;

            frameProgress.MouseDown += frameProgress_Click;
        }

        /// <summary>
        /// Handle play/pause button click
        /// </summary>
        private async void btnPlay_Click(object sender, EventArgs e)
        {
            setPause(!_paused);
        }

        private void setPause(bool paused)
        {
            if (!paused && _reader.atEnd())
                _reader.seekFrame(0);
            _paused = paused;
            btnPlay.Text = _paused ? "Play" : "Pause";
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

        private void frameProgress_Click(object sender, MouseEventArgs e)
        {
            long pos = e.X*(frameProgress.Maximum)/frameProgress.Size.Width;
            System.Console.WriteLine("MousePos: {0}", pos);
            _playbackTimer.Stop();
            _reader.seekFrame(pos);
            _playbackTimer.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            // ofd.InitialDirectory
            ofd.Filter = "times file (*.bin)|*.bin";
            ofd.RestoreDirectory = true;
            _reader.close();
            videoDir.Text = "";
            btnPlay.Enabled = false;
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                string dirname = Directory.GetParent(ofd.FileName).FullName;
                videoDir.Text = dirname;
                _reader.load(dirname);
                //_dc.setDir(dirname);
                _reader.advanceFrame();
                btnPlay.Enabled = true;
            }
        }
    }
}
