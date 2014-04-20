using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OpenCvSharp;

namespace MapBuilder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string WINDOW_NAME = "Capture camera";

        const int MAX_POROG = 30000;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            CvCapture capture = Cv.CreateCameraCapture(CaptureDevice.Any);

            IplImage src = capture.QueryFrame();

            

            CvWindow original = new CvWindow("original", WindowMode.AutoSize);

            CvWindow binary = new CvWindow("binary", WindowMode.AutoSize);

            CvWindow cont = new CvWindow("Contours", WindowMode.AutoSize);

            int upperPorog = 2 * MAX_POROG / 5;
            int lowerPorog = 3 * MAX_POROG / 5;

            CvMemStorage storage;
            CvSeq<CvPoint> contours;

            while (true)
            {
                src = capture.QueryFrame();
                IplImage gray = Cv.CreateImage(src.Size, BitDepth.U8, 1);
                IplImage bin = Cv.CreateImage(src.Size, BitDepth.U8, 1);
                IplImage c = new IplImage(src.Size, src.Depth, src.NChannels);

                IplImage dst = Cv.CloneImage(gray);

                Cv.CvtColor(src, gray, ColorConversion.RgbToGray);
                Cv.InRangeS(src, Cv.ScalarAll(40), Cv.ScalarAll(150), bin);
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

                for (int i = 0; i < bmp.Width; i++)
                    for (int j = bmp.Height - 1; j != 0; j--)
                    {
                        System.Drawing.Color col = bmp.GetPixel(i, j);
                        if (col.R == 255)
                        {
                            point.X = i;
                            point.Y = j;
                            im.DrawLine(point, point, Cv.RGB(255, 0, 0));
                            break;
                        }
                    }
       
                original.Image = src;
                binary.Image = im;
                cont.Image = c;
           //     txtCoord.Text = "";

                int key = Cv.WaitKey(1);
                if (key == 27)
                    break;
                if( key == 13 )
                {
                    src.SaveImage( "original1.jpg" );
                    im.SaveImage( "binary1.jpg" );
                    c.SaveImage( "cont1.jpg" );
                }
                
                Cv.ReleaseImage(im);
                Cv.ReleaseImage(gray);
                Cv.ReleaseImage(bin);
                Cv.ReleaseImage(dst);
                Cv.ReleaseImage(src);
                Cv.ReleaseImage(c);
                Cv.ReleaseMemStorage(storage);
            }
        }

            /*
	while(1)
	{
		int i = 0;
		for (CvSeq* seq0 = contours; seq0 != 0; seq0 = seq0->h_next)
		{
			cvDrawContours(dst1, seq0, CV_RGB(255,0,0), CV_RGB(0,0,255), 0, 1, 4);	
			i++;
		}
		i;
		cvShowImage ("source", src);
		cvShowImage ("result", dst1);
        char c = cvWaitKey(1);
		if (c == 27) break;
		cvReleaseMemStorage (&storage); 
		cvReleaseImage (&dst1);
	}
	cvReleaseImage (&dst1);
	cvReleaseMemStorage (&storage);
	cvReleaseImage (&dst);
	cvReleaseImage (&gray);
	cvReleaseCapture (&capture);
	cvDestroyAllWindows();	
        }

        */
    }
}
