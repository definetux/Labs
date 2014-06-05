using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using OpenCvSharp;

namespace MapBuilder
{
    class MapCreator
    {
        const string WINDOW_NAME = "Capture camera";

        const int MAX_POROG = 30000;

        public Bitmap GetBitmap( Bitmap currentBmp, int currentX, int currentY, int currentScale, int currentAngle )
        {
            CvCapture capture = Cv.CreateCameraCapture(CaptureDevice.Any);

            IplImage src = capture.QueryFrame();

            CvWindow binary = new CvWindow("binary", WindowMode.AutoSize);
            CvWindow result = new CvWindow( "result", WindowMode.AutoSize );

            int upperPorog = 2 * MAX_POROG / 5;
            int lowerPorog = 3 * MAX_POROG / 5;

            CvMemStorage storage;
            CvSeq<CvPoint> contours;

            int coordX = currentX;
            int coordY = currentY;
            int angle = currentAngle;
            int scale = currentScale;            

            while (true)
            {
                src = capture.QueryFrame();
                IplImage gray = Cv.CreateImage(src.Size, BitDepth.U8, 1);
                IplImage c = new IplImage(src.Size, src.Depth, src.NChannels);

                IplImage dst = Cv.CloneImage(gray);

                Cv.CvtColor(src, gray, ColorConversion.RgbToGray);
                Cv.Canny(gray, dst, upperPorog, lowerPorog, ApertureSize.Size7);
                storage = Cv.CreateMemStorage();
                int contoursInt = Cv.FindContours(dst, storage, out contours, CvContour.SizeOf, ContourRetrieval.External, ContourChain.ApproxSimple);


                for (CvSeq<CvPoint> seq = contours; seq != null; seq = seq.HNext)
                {
                    Cv.DrawContours(c, seq, Cv.RGB(255, 0, 0), Cv.RGB(0, 0, 255), 0, 1, LineType.Link4);
                }
                
                System.Drawing.Bitmap bmp = c.ToBitmap();

                IplImage im = new IplImage(src.Size, src.Depth, src.NChannels);

                CvPoint point;

                Bitmap res = new Bitmap( currentBmp );

                Graphics e = Graphics.FromImage( res );
                e.DrawRectangle( Pens.Black, new Rectangle( coordX - 10, coordY - 10, 20, 20 ) );

                for (int i = 0; i < bmp.Width; i++)
                    for (int j = bmp.Height - 1; j != 0; j--)
                    {
                        System.Drawing.Color col = bmp.GetPixel(i, j);
                        if (col.R == 255)
                        {
                            point.X = i;
                            point.Y = j;
                            im.DrawLine(point, point, Cv.RGB(255, 0, 0));
                            switch( angle )
                            {
                                case 0:
                                    res.SetPixel( coordX - scale * 2 + i / scale, ( coordY - scale ) - ( bmp.Height - j ) / scale, Color.Black );
                                    break;
                                case 90:
                                    res.SetPixel( coordX + scale + ( bmp.Height - j ) / scale,  coordY - scale * 2 + i  / scale, Color.Black );
                                    break;
                                case 180:
                                    res.SetPixel( coordX - scale * 2 + i / scale, ( coordY + scale * 2 ) + ( bmp.Height - j ) / scale, Color.Black );
                                    break;
                                case 270:
                                    res.SetPixel( coordX - scale - ( bmp.Height - j ) / scale, coordY - scale * 2 + i / scale, Color.Black );
                                    break;
                            }
                            break;
                        }
                    }
       
                binary.Image = im;
                result.Image = BitmapConverter.ToIplImage( res );

                int key = Cv.WaitKey(1);
                if( key == 27 )
                {
                    binary.Close( );
                    result.Close( );
                    Cv.ReleaseImage( im );
                    Cv.ReleaseImage( gray );
                    Cv.ReleaseImage( dst );
                    Cv.ReleaseImage( src );
                    Cv.ReleaseImage( c );
                    Cv.ReleaseMemStorage( storage );
                    Cv.ReleaseCapture( capture );

                    Graphics g = Graphics.FromImage( res );
                    g.DrawRectangle( Pens.White, new Rectangle( coordX - 10, coordY - 10, 20, 20 ) );
                    return res;
                }
                if( key == 13 )
                {
                    src.SaveImage( "original1.jpg" );
                    im.SaveImage( "binary1.jpg" );
                    c.SaveImage( "cont1.jpg" );
                }
                
                Cv.ReleaseImage(im);
                Cv.ReleaseImage(gray);
                Cv.ReleaseImage(dst);
                Cv.ReleaseImage(src);
                Cv.ReleaseImage(c);
                Cv.ReleaseMemStorage(storage);
            }
        }
    }
}
