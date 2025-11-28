using System;
using System.Runtime;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.Windows;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Linq;

using System.Windows.Navigation;
using Microsoft.Win32;
using System.Globalization;
using JoystickLibrarySharp;
                          


namespace HomePage
{
    /// <summary>
    /// Interaction logic for CabinWindow.xaml
    /// </summary>
    public partial class CabinWindow : Window
    {
        private Thread windowThread;
        private Thread libraryThread;
        private bool isRunning = true;

        Point posjoy;
        Point pointcurrent;
        bool DrapthJoy = false;
        double MovX_Val = 0;
        double MovY_Val = 0;
        double MovYZoom_Val = 0;
        bool rotate = false;

        int inverseVal = -1;

        public CabinWindow()
        {
            InitializeComponent();

            //UpdateCompassData();

            ////BitmapImage spriteSheetBitmap = new BitmapImage(new Uri("/Images/Sprite_AircraftHealth.png", UriKind.Relative));
            //spriteSheetBitmap.CreateOptions = BitmapCreateOptions.None;
            ////spriteSheetBitmap.ImageOpened += new EventHandler<RoutedEventArgs>(image_ImageOpened);

            // image_ImageOpened();

            //Extreme3DProService es = new Extreme3DProService();
            //Xbox360Service xs = new Xbox360Service();

            //Thread.Sleep(100);
            // Thread backgroundThread = new Thread(UpdateWindow);
            // backgroundThread.Start();


        }
        [STAThread]
        public void UpdateCabinData(double dataX, double dataY, double dataZ, double dataSlider)
        {
           // Compass compass = new Compass();
            // Ensure this method is called from the UI thread
            Application.Current.Dispatcher.Invoke(() =>
            {
                tblXjoystick.Text = $"ROLL  : {dataX}"; //Roll
                tblYjoystick.Text = $"PITCH : {dataY}"; //Pitch
                tblZjoystick.Text = $"YAW   : {dataZ}"; //Yaw
                tblSliderjoystick.Text = $"THRUST: {dataSlider}"; //Throttle



                //-- Updating the Compass
                Compass.Heading = Compass.Heading + Convert.ToDouble(dataZ) / 10;

                //-- Updating anemomtre: Airspeed
                anemometre.Vitesse = dataSlider*1.75;

                //-- Updating horizon
                //To Be Calculated based on aircraft feedback:
                horizon.Tanguage = dataY/5.37;
                horizon.Roulis = dataX/2.7;


                //-- Updating variometre: Vertical Speed: To be calculated
                variometre.DecifeetPm = dataSlider/7.30;


            });
        }
        private void UpdateHorizonData()
        {

        }



        //============================= Sprites | Bitmap ============================================
        private void LoadMySpritesHealth()
        {
            ////---------------------------------
            //Image myImage = new Image();
            //BitmapImage bi = new BitmapImage();

            //// Begin initialization.
            //bi.BeginInit();

            //// Set properties.
            //bi.CacheOption = BitmapCacheOption.OnDemand;
            //bi.CreateOptions = BitmapCreateOptions.DelayCreation;
            //bi.DecodePixelHeight = 125;
            //bi.DecodePixelWidth = 125;
            //bi.Rotation = Rotation.Rotate90;
            //MessageBox.Show(bi.IsDownloading.ToString());
            //bi.UriSource = new Uri("Home_Page.png", UriKind.Relative);

            //// End initialization.
            //bi.EndInit();
            //myImage.Source = bi;
            //myImage.Stretch = Stretch.None;
            //myImage.Margin = new Thickness(5);
            //////-------------------------------------------------
            /////

        }
        private void LoadMySpriteshealth2()
        {
            //Image myImage = new Image();
            //FormattedText text = new FormattedText("ABC", new CultureInfo("en-us"),
            //        FlowDirection.LeftToRight,
            //        new Typeface(this.FontFamily, FontStyles.Normal, FontWeights.Normal, new FontStretch()),
            //        this.FontSize,
            //        this.Foreground);

            //DrawingVisual drawingVisual = new DrawingVisual();
            //DrawingContext drawingContext = drawingVisual.RenderOpen();
            //drawingContext.DrawText(text, new Point(2, 2));
            //drawingContext.Close();

            //RenderTargetBitmap bmp = new RenderTargetBitmap(180, 180, 120, 96, PixelFormats.Pbgra32);
            //bmp.Render(drawingVisual);
            //myImage.Source = bmp;

            //// Add Image to the UI
            //StackPanel myStackPanel = new StackPanel();
            //myStackPanel.Children.Add(myImage);
            //this.Content = myStackPanel;

        }

        /*
        ////Part 1 Starts ------------------------------------------------------------------------------------------------
        void image_ImageOpened()
        {
            BitmapImage spriteSheetBitmap = new BitmapImage(new Uri("C:\\Development\\1_DirectX12\\A1_Personal\\FlightSim_V1_DirectX_Test\\DX12\\DX12_ProjectHome\\HomePage1\\HomePage\\Sprites\\Sprite_AircraftHealth.png", UriKind.Relative));
            MessageBox.Show(spriteSheetBitmap.IsDownloading.ToString());
            //spriteSheetBitmap.CreateOptions = BitmapCreateOptions.None;
            //////spriteSheetBitmap.ImageOpened += new EventHandler<RoutedEventArgs>(image_ImageOpened);

            //---------------

            //---------------
            spriteSheetBitmap.CacheOption = BitmapCacheOption.OnDemand;

            spriteSheetBitmap.CreateOptions = BitmapCreateOptions.None;
            //spriteSheetBitmap.ImageOpened += new EventHandler<RoutedEventArgs>(image_ImageOpened);


            //BitmapImage spriteSheetBitmap = sender as BitmapImage;

            SpriteSheet spriteSheet = new SpriteSheet(spriteSheetBitmap);

            // Set the source of the mySprite1 to an image extracted from the SpriteSheet
            // mySprite1.Source = spriteSheet.GetBitmap(0, 0, 224, 240); //x=320 px, y=32 px

            mySprite1.Source = spriteSheet.GetBitmap(64, 0, 32, 32); //x=320 px, y=32 px

            // Set the source of the mySprite2 to an image extracted from the SpriteSheet
            //mySprite2.Source = spriteSheet.GetBitmap(2240, 0, 224, 240);
           // mySprite2.Source = spriteSheet.GetBitmap(0, 0, 320, 32); //x=320 px, y=32 px


        }


        //void image_ImageOpened(object sender, RoutedEventArgs e)
        //{
        //    BitmapImage spriteSheetBitmap = sender as BitmapImage;

        //    SpriteSheet spriteSheet = new SpriteSheet(spriteSheetBitmap);

        //    // Set the source of the mySprite1 to an image extracted from the SpriteSheet
        //    // mySprite1.Source = spriteSheet.GetBitmap(0, 0, 224, 240); //x=320 px, y=32 px

        //    mySprite1.Source = spriteSheet.GetBitmap(0, 0, 320, 32); //x=320 px, y=32 px

        //    // Set the source of the mySprite2 to an image extracted from the SpriteSheet
        //    mySprite2.Source = spriteSheet.GetBitmap(2240, 0, 224, 240);
        //}
        public class SpriteSheet
        {
            private BitmapSource _spriteSheetSource;
            private WriteableBitmap _spriteSheetBitmap;
            private int _sheetWidth;
            private int _sheetHeight;

            public SpriteSheet(BitmapSource spriteSheetSource)
            {
                if (spriteSheetSource == null) throw new ArgumentNullException("spriteSheetSource");

                _spriteSheetSource = spriteSheetSource;
                _spriteSheetBitmap = new WriteableBitmap(_spriteSheetSource);
                _sheetWidth = _spriteSheetBitmap.PixelWidth;
                _sheetHeight = _spriteSheetBitmap.PixelHeight;
            }

            public WriteableBitmap GetBitmap(int x, int y, int width, int height)
            {
                WriteableBitmap destination = new WriteableBitmap(width, height, 32, 32, PixelFormats.Bgr32, null);
                GetBitmap(destination, x, y, width, height);
                return destination;
            }

            public void GetBitmap(WriteableBitmap targetBitmap, int x, int y, int width, int height)
            {
                // Validate incomming data
                if (targetBitmap == null) throw new ArgumentNullException("targetBitmap");
                if (x < 0 || x >= _sheetWidth) throw new ArgumentOutOfRangeException("x");
                if (y < 0 || y >= _sheetHeight) throw new ArgumentOutOfRangeException("y");
                if (width < 0 || (x + width > _sheetWidth)) throw new ArgumentOutOfRangeException("width");
                if (height < 0 || (y + height > _sheetHeight)) throw new ArgumentOutOfRangeException("height");

                // Get pixel buffers for the sprite sheet and the target bitmap
                //------------------
                // int[] sourcePixels = _spriteSheetBitmap.Pixels;
                // int[] targetPixels = targetBitmap.Pixels;
                //-------------------

                // Calculate starting offsets into the pixel buffers      
                int sourceOffset = x + (y * _sheetWidth);
                int targetOffset = 0;

                // Note that the offsets and widths are multiplied by 4, this is because Buffer.BlockCopy requires
                // byte offsets into the buffers and our buffers are integer buffers. To optimize this I have 
                // premultiplied to values so that the multiplication is removed from the loop
                int sourceByteOffset = sourceOffset << 2;
                int sheetByteWidth = _sheetWidth << 2;
                int targetByteWidth = width << 2;
                for (int row = 0; row < height; ++row)
                {
                    //-----------------------
                    // Buffer.BlockCopy(sourcePixels, sourceByteOffset, targetPixels, targetOffset, targetByteWidth);
                    sourceByteOffset += sheetByteWidth;
                    targetOffset += targetByteWidth;
                    //-----------------------
                }
            }

            public int Width
            {
                get { return _sheetWidth; }
            }

            public int Height
            {
                get { return _sheetHeight; }
            }
        }
       //// Part 1 ends ---------------------------------------------------------------------------------------------------

        */
        //////Part 2 Starts ---------------------------------------------------------------------------------
        ////// The DrawPixel method updates the WriteableBitmap by using
        ////// unsafe code to write a pixel into the back buffer.
        ////static void DrawPixel(MouseEventArgs e)
        ////{
        ////    WriteableBitmap writeableBitmap;
        ////    Window w;
        ////    Image i;

        ////    i = new Image();
        ////    RenderOptions.SetBitmapScalingMode(i, BitmapScalingMode.NearestNeighbor);
        ////    RenderOptions.SetEdgeMode(i, EdgeMode.Aliased);

        ////    w = new Window();
        ////    w.Content = i;
        ////    w.Show();

        ////    writeableBitmap = new WriteableBitmap(
        ////        (int)w.ActualWidth,
        ////        (int)w.ActualHeight,
        ////        96,
        ////        96,
        ////        PixelFormats.Bgr32,
        ////        null);

        ////    i.Source = writeableBitmap;

        ////    i.Stretch = Stretch.None;
        ////    i.HorizontalAlignment = HorizontalAlignment.Left;
        ////    i.VerticalAlignment = VerticalAlignment.Top;

        ////    i.MouseMove += new MouseEventHandler(i_MouseMove);
        ////    i.MouseLeftButtonDown +=
        ////            new MouseButtonEventHandler(i_MouseLeftButtonDown);
        ////    i.MouseRightButtonDown +=
        ////            new MouseButtonEventHandler(i_MouseRightButtonDown);

        ////    w.MouseWheel += new MouseWheelEventHandler(w_MouseWheel);
        ////    int column = (int)e.GetPosition(i).X;
        ////    int row = (int)e.GetPosition(i).Y;




        ////    try
        ////    {
        ////        // Reserve the back buffer for updates.
        ////        writeableBitmap.Lock();

        ////        unsafe
        ////        {
        ////            // Get a pointer to the back buffer.
        ////            IntPtr pBackBuffer = writeableBitmap.BackBuffer;

        ////            // Find the address of the pixel to draw.
        ////            pBackBuffer += row * writeableBitmap.BackBufferStride;
        ////            pBackBuffer += column * 4;

        ////            // Compute the pixel's color.
        ////            int color_data = 255 << 16; // R
        ////            color_data |= 128 << 8;   // G
        ////            color_data |= 255 << 0;   // B

        ////            // Assign the color data to the pixel.
        ////            *((int*)pBackBuffer) = color_data;
        ////        }

        ////        // Specify the area of the bitmap that changed.
        ////        writeableBitmap.AddDirtyRect(new Int32Rect(column, row, 1, 1));
        ////    }
        ////    finally
        ////    {
        ////        // Release the back buffer and make it available for display.
        ////        writeableBitmap.Unlock();
        ////    }
        ////}

        ////static void ErasePixel(MouseEventArgs e)
        ////{
        ////   // WriteableBitmap writeableBitmap;
        ////    Window w;
        ////    Image i;

        ////    i = new Image();
        ////    RenderOptions.SetBitmapScalingMode(i, BitmapScalingMode.NearestNeighbor);
        ////    RenderOptions.SetEdgeMode(i, EdgeMode.Aliased);

        ////    w = new Window();
        ////    w.Content = i;
        ////    w.Show();

        ////    byte[] ColorData = { 0, 0, 0, 0 }; // B G R

        ////    Int32Rect rect = new Int32Rect(
        ////            (int)(e.GetPosition(i).X),
        ////            (int)(e.GetPosition(i).Y),
        ////            1,
        ////            1);

        ////    //writeableBitmap.WritePixels(rect, ColorData, 4, 0);
        ////}

        ////static void i_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        ////{
        ////    ErasePixel(e);
        ////}

        ////static void i_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        ////{
        ////    DrawPixel(e);
        ////}

        ////static void i_MouseMove(object sender, MouseEventArgs e)
        ////{
        ////    if (e.LeftButton == MouseButtonState.Pressed)
        ////    {
        ////        DrawPixel(e);
        ////    }
        ////    else if (e.RightButton == MouseButtonState.Pressed)
        ////    {
        ////        ErasePixel(e);
        ////    }
        ////}

        ////static void w_MouseWheel(object sender, MouseWheelEventArgs e)
        ////{
        ////    //WriteableBitmap writeableBitmap;
        ////    Window w;
        ////    Image i;

        ////    i = new Image();
        ////    RenderOptions.SetBitmapScalingMode(i, BitmapScalingMode.NearestNeighbor);
        ////    RenderOptions.SetEdgeMode(i, EdgeMode.Aliased);

        ////    w = new Window();
        ////    w.Content = i;
        ////    w.Show();
        ////    System.Windows.Media.Matrix m = i.RenderTransform.Value;

        ////    if (e.Delta > 0)
        ////    {
        ////        m.ScaleAt(
        ////            1.5,
        ////            1.5,
        ////            e.GetPosition(w).X,
        ////            e.GetPosition(w).Y);
        ////    }
        ////    else
        ////    {
        ////        m.ScaleAt(
        ////            1.0 / 1.5,
        ////            1.0 / 1.5,
        ////            e.GetPosition(w).X,
        ////            e.GetPosition(w).Y);
        ////    }

        ////    i.RenderTransform = new MatrixTransform(m);
        ////}

        //////Part2 ends ---------------------------------------------------------------------------------






    }

}
