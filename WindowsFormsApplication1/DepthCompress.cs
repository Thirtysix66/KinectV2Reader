using System;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO.Compression;

public class DepthCompress
{
    private int width, height;
    private Int16[] frame, lastFrame;
    private Byte[] lowerFrame, upperFrame;
    private int fn;
    private string _dirname;
    private System.IO.FileStream _fstream;
    private GZipStream _gzstream;

	public DepthCompress(int width, int height)
	{
        this.width = width;
        this.height = height;

        int n = width * height;
        frame = new Int16[n];
        lastFrame = new Int16[n];
        lowerFrame = new Byte[n];
        upperFrame = new Byte[n];
        fn = 0;

        for (int i = 0; i < n; i++)
            frame[i] = lastFrame[i] = 0;
	}
    ~DepthCompress()
    {
        close();
    }

    public void setDir(string dirname)
    {
        close();
        this._dirname = dirname;
        _fstream = new System.IO.FileStream(dirname + "\\test.png", System.IO.FileMode.Create);
        System.IO.FileStream fs2 = new System.IO.FileStream(dirname + "\\test2.gz", System.IO.FileMode.Create);
        _gzstream = new GZipStream(fs2, CompressionLevel.Fastest);
    }
    public void close()
    {
        if(_fstream != null )
            _fstream.Close();
        if (_gzstream != null)
            _gzstream.Close();
        _gzstream = null;
        _fstream = null;
    }

    public void addFrame(Int16[,] vals)
    {
        int idx = 0;
        UInt16[] vals2 = new UInt16[vals.Length];

        for (int i = 0; i < vals.GetLength(0); i++)
            for (int j = 0; j < vals.GetLength(1); j++)
            {
                vals2[idx] = (ushort)vals[i, j];
                idx++;
            }
        addFrame(vals2);
    }
    public void addFrame(UInt16[] vals)
    {
        fn++;
        bool isDiff = false;
        for (int i = 0; i < vals.Length; i++)
        {
            lastFrame[i] = (short)(frame[i]+lastFrame[i]);
            //frame[i] = (short)(vals[i]);
            frame[i] = (short)(vals[i] - lastFrame[i]);
            if (vals[i] != lastFrame[i])
                isDiff = true;
        }
        if (!isDiff)
            return;

        separateFrames();
        savePNG(upperFrame);
        savePNG(lowerFrame);
        joinFrames();
        int error=0;
        for(int i=0; i < vals.Length; i++)
        {
            short v = (short)(lastFrame[i] + frame[i]);
            if ( v != vals[i])
                error++;
        }
        System.Console.WriteLine(String.Format("frame {1} errors: {0}", error, fn));


    }
    public void savePNG(byte[] data)
    {
/*        PngBitmapEncoder encoder;
        encoder = new PngBitmapEncoder();
        encoder.Interlace = PngInterlaceOption.Off;

        WriteableBitmap depthBitmap = new WriteableBitmap(width, height, 96.0, 96.0, PixelFormats.Gray8, null);
        depthBitmap.WritePixels(
            new System.Windows.Int32Rect(0, 0, depthBitmap.PixelWidth, depthBitmap.PixelHeight),
            data,
            depthBitmap.PixelWidth,
            0);

        encoder.Frames.Add(BitmapFrame.Create((BitmapSource)depthBitmap));

        encoder.Save(_fstream);
        */
        _gzstream.Write(data, 0, data.Length);
    }


    public void joinFrames()
    {
        uint upperV, lowerV;
        for(int i=0; i < frame.Length; i++)
        {
            upperV = expandBits(upperFrame[i]);
            lowerV = expandBits(lowerFrame[i]);
            frame[i] = (Int16)((upperV << 1) | lowerV);
        }
    }
    public void separateFrames()
    {
        for(int i=0; i < frame.Length; i++)
        {
            lowerFrame[i] = (Byte)squeezeBits((UInt16)frame[i]);
            upperFrame[i] = (Byte)squeezeBits((uint)(frame[i] >> 1));
        }
    }

    private ushort squeezeBits(uint value)
    {
        value &= 0x55555555;
        value |= value >> 1;
        value &= 0x33333333;
        value |= value >> 2;
        value &= 0x0F0F0F0F;
        value |= value >> 4;
        value &= 0x00FF00FF;
        value |= value >> 8;
        value &= 0x0000FFFF;
        return (ushort)value;
    }
    private uint expandBits(ushort squeezed)
    {
        uint value = (uint)squeezed;
        value |= (value & 0x0000FF00) << 8;
        value &= 0x00FF00FF;
        value |= (value & 0x00F000F0) << 4;
        value &= 0x0F0F0F0F;
        value |= (value & 0x0C0C0C0C) << 2;
        value &= 0x33333333;
        value |= (value & 0x22222222) << 1;
        value &= 0x55555555;
        return value;
    }
}
