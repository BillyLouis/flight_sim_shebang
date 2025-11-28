using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HomePage
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /*
        [STAThread]
        public static void Main()
        {
            App app = new App();
            MainWindow mainWindow = new MainWindow();
            app.Run(mainWindow);
        }


        */
        
        //=====================================================================
        //    public static bool launchMyCabin { get; internal set; }
        //    public bool LaunchCabin( bool launchCabin)
        //    {
        //        launchMyCabin = launchCabin;

        //        return launchCabin;
        //    }
        //====================================================================
        void App_Startup(object sender, StartupEventArgs e)
        {

            ////if (launchMyCabin == true)
            ////{
            ////    CabinWindow mainWindow = new CabinWindow();
            ////    mainWindow.Top = 100;
            ////    mainWindow.Left = 100;
            ////    mainWindow.Show();
            ////}
            //CabinWindow cbWin = new CabinWindow();
            //cbWin.Top = 100;
            //cbWin.Left = 100;
            //cbWin.Show();
            ////mainWindow.Close();
            //ControlsWindow ctrWin = new ControlsWindow();
            //ctrWin.Top = 500;
            //ctrWin.Left = 100;
            //ctrWin.Show();

        }
        

    }
}
