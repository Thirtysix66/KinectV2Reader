using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
using AForge.Video.FFMPEG;

namespace WindowsFormsApplication1
{
    public class RgbdReader
    {
        private string _filename = null;


        private BinaryReader _depthReader = null;
        private BinaryReader _timeReader = null;
        private VideoFileReader _rgbReader = null;

        private long _nFrames, _frameNumber;
        private DateTime _firstTime;
        private Int16[,] _curDepth;
        private DateTime _curTime;
        private Bitmap _curRGB;

        private const int depthWidth=512;
        private const int depthHeight=424;


        public RgbdReader()
        {
            _nFrames = 0;
            _frameNumber = -1;

            load(@"C:\Users\AC lab\Desktop\Depth\Kinect_filename_2017-04-09-10-10-57");
        }
        public void close()
        {
            _nFrames = 0;
            _frameNumber = -1;
            _curDepth = null;
            _filename = null;
            _curTime = new DateTime();

            if (_depthReader != null)
                _depthReader.Close();
            _depthReader = null;
            if (_timeReader != null)
                _timeReader.Close();
            _timeReader = null;
            if (_rgbReader != null)
                _rgbReader.Close();
            _rgbReader = null;
        }
        public bool reload()
        {
            return load(_filename);
        }
        public bool load(string path)
        {
            close();
            _filename = path;

            if (!Directory.Exists(path))
                return false;
            string timesPath = Path.Combine(path, "times.bin");
            string depthPath = Path.Combine(path, "depth.bin.gz");
            string rgbPath = Path.Combine(path, "rgb.avi");

            DirectoryInfo pathInfo = new DirectoryInfo(path);
            string pname = pathInfo.Name;
            Console.WriteLine(pname);
            string tinfo = pname.Split('_').Last();
            int[] timeValues = (from val in tinfo.Split('-') select Convert.ToInt32(val)).ToArray();
            if (timeValues.Length != 6)
                return false;
            _firstTime = new DateTime(timeValues[0], timeValues[1], timeValues[2]);

            // load times
            if (!File.Exists(timesPath))
                return false;
            _timeReader = new BinaryReader(File.Open(timesPath, FileMode.Open, FileAccess.Read));
            _nFrames = _timeReader.BaseStream.Length / 8;

            Console.WriteLine("Frames: {0}", size());

            // Load depth frames
            if(File.Exists(depthPath))
            {
                FileStream depthFile = File.Open(depthPath, FileMode.Open, FileAccess.Read);
                GZipStream gzReader = new GZipStream(depthFile, CompressionMode.Decompress);
                _depthReader = new BinaryReader(gzReader);
                _curDepth = new Int16[depthWidth, depthHeight];
            }

            // load rgb frames
            if(File.Exists(rgbPath))
            {
                AForge.Video.FFMPEG.VideoFileReader _rgbReader = new AForge.Video.FFMPEG.VideoFileReader();
                _rgbReader.Open(rgbPath);
            }

            advanceFrame();
            return true;
        }
        public long size()
        {
            return _nFrames;
        }
        public long currentFrame()
        {
            return _frameNumber;
        }
        public void advanceFrame(long n=1)
        {
            for (int i = 0; i < n; i++)
                advanceOneFrame();
        }
        private void advanceOneFrame()
        {
            if (!isLoaded())
                return;
            if(_frameNumber < (size()-1))
            {
                advanceTime();
                if(hasDepth())
                    advanceDepth();
                if (hasRGB())
                    advanceRGB();
                _frameNumber++;
            }
        }
        private void advanceTime()
        {
            int hour = _timeReader.ReadInt16();
            int minute = _timeReader.ReadInt16();
            int second = _timeReader.ReadInt16();
            int millisecond = _timeReader.ReadInt16();

            _curTime = new DateTime(_firstTime.Year, _firstTime.Month, _firstTime.Day, hour, minute, second, millisecond);
        }
        private void advanceDepth()
        {
            for (int i = 0; i < depthWidth; i++)
                for (int j = 0; j < depthHeight; j++)
                    _curDepth[i, j] += _depthReader.ReadInt16();
        }
        private void advanceRGB()
        {
            _curRGB = _rgbReader.ReadVideoFrame();
        }
        public void seekFrame(long frameNumber)
        {
            long cur = currentFrame();
            if(frameNumber < cur)
            {
                reload();
                advanceFrame(frameNumber);
            } else
            {
                advanceFrame(frameNumber - cur);
            }
        }

        public bool isLoaded()
        {
            return _timeReader != null;
        }
        public DateTime getTime()
        {
            return _curTime;
        }

        public bool hasDepth()
        {
            return _depthReader != null;
        }
        public bool hasRGB()
        {
            return _rgbReader != null;
        }
        public Bitmap getDepthBitmap()
        {
            byte[] arr = new byte[depthWidth*depthHeight];

            Int16 frameMin, frameMax;
            frameMin = frameMax = _curDepth[0, 0];
            for (int i = 0; i < depthWidth; i++)
            {
                for (int j = 0; j < depthHeight; j++)
                {
                    short v = _curDepth[i, j];
                    frameMin = Math.Min(v, frameMin);
                    frameMax = Math.Max(v, frameMax);
                }
            }

            int idx = 0;
            for(int i=0; i < depthWidth; i++)
            {
                for(int j=0; j < depthHeight; j++)
                {
                    int v = (_curDepth[i, j]-frameMin) * 255;
                    arr[idx++] = (byte)(v / (frameMax - frameMin));
                }
            }

            WriteableBitmap depthBitmap = new WriteableBitmap(depthWidth, depthHeight, 96.0, 96.0, PixelFormats.Gray8, null);
            depthBitmap.WritePixels(
                new Int32Rect(0, 0, depthBitmap.PixelWidth, depthBitmap.PixelHeight),
                arr,
                depthBitmap.PixelWidth,
                0);

            System.Drawing.Bitmap bmp;

            MemoryStream outStream = new MemoryStream();
            BitmapEncoder enc = new BmpBitmapEncoder();
            enc.Frames.Add(BitmapFrame.Create((BitmapSource)depthBitmap));
            enc.Save(outStream);
            bmp = new System.Drawing.Bitmap(outStream);

            return bmp;
        }
        public Bitmap getRGBBitmap()
        {
            return _curRGB;
        }
        // getDepthFrame
        // getRGBFrame()
        // drawDepthFrame
        // drawRGBFrame

    }
}
