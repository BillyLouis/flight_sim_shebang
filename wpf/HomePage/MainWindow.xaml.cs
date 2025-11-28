using System;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using JoystickLibrarySharp;

namespace HomePage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>


    public partial class MainWindow : Window
    {
        CancellationTokenSource ts;
        private static Thread joystickThread;

        private CabinWindow cabinWindow;
        private ControlsWindow controlsWindow;
        //private DashboardWindow dashboardWindow;
        //private Thread dashboardThread;
        private Thread cabinThread;
        public class JoystickData
        {
            public int xAxis { get; set; }
            public int yAxis { get; set; }
            public int zAxis { get; set; }
            public int sliderAxis { get; set; }
            public int errorReturn_0 { get; set; }

        }

        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {
            DashboardWindow objDashboardWindow = new DashboardWindow();
            objDashboardWindow.Show();
            /*
            dashboardWindow = new DashboardWindow();
            dashboardThread = new Thread(displayImagesFromPi);
            dashboardThread.SetApartmentState(ApartmentState.STA);
            dashboardThread.Start();
            dashboardWindow.Show();
            */
        }
        private void displayImagesFromPi()
        {
            //dashboardWindow.StartImageTransfer();
        }
        private void btnCabin_Click(object sender, RoutedEventArgs e)
        {
            cabinWindow = new CabinWindow();
            cabinThread = new Thread(DisplayCabinData);
            cabinThread.SetApartmentState(ApartmentState.STA);
            cabinThread.Start();
            cabinWindow.Show();
        }
        private void DisplayCabinData()
        {
            // Simulate data from DLL library (joystick input)
            Random random = new Random();
            double dataX = 301.01;
            double dataY = 302.01;
            double dataZ = 303.01;
            double dataSlider = 304.01;

            int[] CatchXyAxesAndSlider = new int[4];

            Thread.Sleep(50); //100 is ideal

            while (true)
            //for (int i=0; i< 100; i++)
            {
                //-- Simulate data update
                //data = (i * 2 * 100 * 0.375)/30;
                //dataX = (double)CatchJoystickData(0); // With return int
                //CatchXyAxesAndSlider = CatchJoystickData(0); //With return array

                for(int j=0; j < 4; j++)
                {
                    if (j == 0)
                    {
                        dataX = (double)CatchJoystickData(j); // getting x
                    }
                    if (j == 1)
                    {
                        dataY = (double)CatchJoystickData(j); // getting y
                    }
                    if (j == 2)
                    {
                        dataZ = (double)CatchJoystickData(j); // getting z
                    }
                    if (j == 3)
                    {
                        dataSlider = (double)CatchJoystickData(j); // getting slider : Throttle
                    }
                }


                //-- Update CabinWindow text block
                //cabinWindow.UpdateCabinData(data);
                //cabinWindow.UpdateCabinData(CatchXyAxesAndSlider[0]);
                cabinWindow.UpdateCabinData(dataX, dataY, dataZ, dataSlider);



                // Share data with ControlWindow
                //controlWindow.UpdateControlData(data);

                Thread.Sleep(100); // Simulate data update every 100 milisecond
            }
        }
        private void btnControls_Click(object sender, RoutedEventArgs e)
        {
            ControlsWindow objControlsWindow = new ControlsWindow();
            objControlsWindow.Show();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }



        //============================ Joystic ======================================================

        //--------------    JOYSTICK FUNCTIONS (testing for now: Before creating its own class)   ----------------

        static readonly Dictionary<POV, string> povNameMap = new Dictionary<POV, string>
        {
            { POV.POV_NONE, "POV_NONE" },
            { POV.POV_WEST, "POV_WEST" },
            { POV.POV_EAST, "POV_EAST" },
            { POV.POV_NORTH, "POV_NORTH" },
            { POV.POV_SOUTH, "POV_SOUTH" },
            { POV.POV_NORTHWEST, "POV_NORTHWEST" },
            { POV.POV_NORTHEAST, "POV_NORTHEAST" },
            { POV.POV_SOUTHWEST, "POV_SOUTHWEST" },
            { POV.POV_SOUTHEAST, "POV_SOUTHEAST" }
        };
        //[STAThread]
        private static int[] PrintAbsoluteAxes(Extreme3DProService s, int id) // Static void function
        {
            int x = 0;
            int y = 0, z = 0, slider = 0;
            int[] xyAxesAndSlider = new int[4];

            if (!s.GetX(id, ref x))
                x = 0;
            if (!s.GetY(id, ref y))
                y = 0;
            if (!s.GetZRot(id, ref z))
                z = 0;
            if (!s.GetSlider(id, ref slider))
                slider = 0;


            xyAxesAndSlider[0] = x;
            xyAxesAndSlider[1] = y;
            xyAxesAndSlider[2] = z;
            xyAxesAndSlider[3] = slider;

            return xyAxesAndSlider;
        }

        static void PrintButtons(Extreme3DProService s, int id)
        {
            bool[] buttons = new bool[12];

            if (!s.GetButtons(id, ref buttons))
                for (int i = 0; i < 12; i++)
                    buttons[i] = false;

            for (int i = 0; i < 12; i++)
            Console.Write("{0} {1} ", i, buttons[i]);
            Console.WriteLine();
        }

        static void PrintPOV(Extreme3DProService s, int id)
        {
            POV pov = POV.POV_NONE;
            if (!s.GetPOV(id, ref pov))
                pov = POV.POV_NONE;

            Console.WriteLine("{0}", povNameMap[pov]);
        }

        static void PrintAbsoluteAxes(Xbox360Service s, int id)
        {
            int lx = 0, ly = 0, rx = 0, ry = 0;

            if (!s.GetLeftX(id, ref lx))
                lx = 0;
            if (!s.GetLeftY(id, ref ly))
                ly = 0;
            if (!s.GetRightX(id, ref rx))
                rx = 0;
            if (!s.GetRightY(id, ref ry))
                ry = 0;

            Console.WriteLine("LX: {0} | LY: {1} | RX: {2} | RY: {3}", lx, ly, rx, ry);
        }

        static void PrintButtons(Xbox360Service s, int id)
        {
            bool[] buttons = new bool[11];

            if (!s.GetButtons(id, ref buttons))
                for (int i = 0; i < 11; i++)
                    buttons[i] = false;

            for (int i = 0; i < 11; i++)
                Console.Write("{0} {1} ", i, buttons[i]);
            Console.WriteLine();
        }

        static void PrintPOV(Xbox360Service s, int id)
        {
            POV pov = POV.POV_NONE;
            if (!s.GetDpad(id, ref pov))
                pov = POV.POV_NONE;

            Console.WriteLine("{0}", povNameMap[pov]);
        }

        [STAThread]
        //private static void function() Originally:
        //private async void CatchJoystickData(Extreme3DProService es, Xbox360Service xs) //static void function
        //private static int[] CatchJoystickData(int getX)
        //private static int CatchJoystickData(int getX) //Original 2
        private static int CatchJoystickData(int getIndex)
        {

            Extreme3DProService es = new Extreme3DProService();
            Xbox360Service xs = new Xbox360Service();

            var joystickData = new JoystickData();
            joystickData.errorReturn_0 = 0;

            CabinWindow objCabinWindow = new CabinWindow();

            int[] xyzSlider = new int[4];

            int getX = 500;
            int getY = 501;
            int getZ = 502;
            int getSlider = 503;
            int getValue = 1004;


            if (!xs.Initialize())
            {
                MessageBox.Show("Failed to initialize Xbox!");
                return joystickData.errorReturn_0;
            }

            if (!es.Initialize())
            {
                MessageBox.Show("Failed to initialize Logitech!");
                return 0;
            }
            //else
            // {
            //    MessageBox.Show("Waiting for a joystick to be plugged in...");
            //}


            while (es.GetNumberConnected() < 1) ;
            // MessageBox.Show("Found one - starting main loop.");
            int j = 0;
            //while (true)
            while (j < 2)
            {
                foreach (int i in es.GetIDs())
                {
                    //Console.Write("[{0}] ", i);
                    //objCabinWindow.tblZjoystick.Text = i.ToString(); //This Cause STAThread Problem

                    //getX = PrintAbsoluteAxes(es, i);
                    xyzSlider = PrintAbsoluteAxes(es, i);
                    if(getIndex == 0)
                    {
                        getX = xyzSlider[0]; //x-axis
                        getValue = xyzSlider[0];
                    }
                    if (getIndex == 1)
                    {
                        getY = xyzSlider[1]; //y-axis
                        getValue = xyzSlider[1];
                    }
                    if (getIndex == 2)
                    {
                        getZ = xyzSlider[2]; //z-axis
                        getValue = xyzSlider[2];
                    }
                    if (getIndex == 3)
                    {
                        getSlider = xyzSlider[3]; //slider-axis
                        getValue = xyzSlider[3];
                    }

                    //-- Future Stuff
                    joystickData.xAxis = xyzSlider[0];
                    joystickData.yAxis = xyzSlider[1];
                    joystickData.zAxis = xyzSlider[2];
                    joystickData.sliderAxis = xyzSlider[3];


                    //PrintButtons(s, i);
                    //PrintPOV(s, i); 
                    //Thread.Sleep(200);
                    break;
                }
                //objCabinWindow.tblZjoystick.Text = getX.ToString(); //This Cause STAThread Problem
                // MessageBox.Show("X: ", getX.ToString()); // Pass value to textBlock

                j++;
            }

            //return xyzSlider;
            return getValue;
        }



    }
}
