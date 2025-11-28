using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HomePage
{
    /// <summary>
    /// Interaction logic for ControlsWindow.xaml
    /// </summary>
    public partial class ControlsWindow : Window
    {
        public ControlsWindow()
        {
            InitializeComponent();
        }

        public void UpdateControlData(double data)
        {
            // Ensure this method is called from the UI thread
            Application.Current.Dispatcher.Invoke(() =>
            {
                //controlDataTextBlock.Text = $"Control Data: {data}";
            });
        }
    }
}
