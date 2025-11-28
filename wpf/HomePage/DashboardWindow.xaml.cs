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
using System.IO;
using System.Threading;

using System.Drawing;

using Microsoft.Win32;

using Image = System.Windows.Controls.Image;
using System.Windows.Threading;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Navigation;
using System.Net.Sockets;
using WinSCP;
using System.Diagnostics;


namespace HomePage
{
    /// <summary>
    /// Interaction logic for DashboardWindow.xaml
    /// </summary>
    public partial class DashboardWindow : Window
    {

        private Uri imageUri;
        //================================
        //private bool isCameraOn = false;
        //private BitmapImage currentImage;


        ////-- private DashboardWindow dashboardWindow;
        ////-- private Thread dashboardThread;

        //private TcpClient tcpClient;
        //private NetworkStream stream;

        //private const int ImageSizeLimit = 32 * 1024; // 32KB
        //private Boolean isSimulation = false;
        private bool isReceivingImages = false;

        private const string piIpAddress = "192.168.1.3";
        private const int piPort = 22;
        private const int serverPort = 5000;
        private const string piUsername = "pi";
        private const string piPassword = "Chance11"; // Replace with your actual Raspberry Pi password
        private string folderPath = @"C:/Users/Alienware/Desktop/FlightSimImageTest/Images";



        public DashboardWindow()
        {
            InitializeComponent();
        }
        private void btnErrorCleanUp_Click(object sender, RoutedEventArgs e)
        {
            tbxError0.Text = "";
            tbxError1.Text = "";
            tbxError2.Text = "";
            tbxError3.Text = "";
        }
        private async void btnStartDashboard_Click(object sender, RoutedEventArgs e)
        {
            isReceivingImages = true;
            await Task.Delay(5);
            try
            {
                if (isReceivingImages)
                {
                        // Turn on image reception
                        isReceivingImages = false;
                        btnStartDashboardButton.Content = "Start Receiving";
                    //await SendTcpMessage("WPF: Starts Images Download");
                    //Dispatcher.Invoke(()=> ReceiveFilesFromPi());
                    //ReceiveFilesFromPi();
                    await ReceiveFilesFromPiAsync();
                    //Thread.Sleep(1000);
                    await SendTcpMessage("WPF: Download Done!");
                    //string rcvMessage = "falseConnect";
                    //rcvMessage = ReceiveTcpMessage();
                    //tbxError1.Text = "WPF Receive Pi: " + rcvMessage;

                    await Task.Delay(5);
                    await DisplayImages();
                    //CloseFoldersAndKillProcesses();
                    //await DeleteImages(folderPath);
                }
                else
                {
                    // Turn of image reception
                    //isReceivingImages = true;
                    btnStartDashboardButton.Content = "Stop Receiving";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async Task SendTcpMessage(string message)
        {
            try
            {
                using (var client = new TcpClient(piIpAddress, serverPort)) // PI_IP_ADDRESS & Port
                using (var writer = new StreamWriter(client.GetStream()))
                {
                    writer.WriteLine(message);
                    writer.Flush();
                    await Task.Delay(10);
                    if (writer != null) { writer.Close(); }
                    if (client != null) { client.Close(); }
                }
            }catch (Exception ex)
            {
                //MessageBox.Show($"Error sending TCP message: {ex.Message}");
                tbxError0.Text = $"Error sending TCP message: {ex.Message}";
            }

        }
        private string ReceiveTcpMessage()
        {
            string receivedMessage = "";
            try
            {
                //TcpListener listener = new TcpListener(System.Net.IPAddress.Parse(piIpAddress), serverPort);
                TcpListener listener = new TcpListener(System.Net.IPAddress.Any, serverPort);

                listener.Start();

                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    NetworkStream stream = client.GetStream();
                    StreamReader reader = new StreamReader(stream);
                    receivedMessage = reader.ReadLine();
                    tbxError3.Text = "Inner Receive TCP: "+receivedMessage;
                    //Thread.Sleep(10);
                    // Handle received message as needed
                    // For example, display it in a TextBox
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Error receiving TCP message: {ex.Message}");
                tbxError0.Text = $"Error receiving TCP message: {ex.Message}";
            }
            return receivedMessage;
        }

        private async Task DisplayImages()
        {
            //string folderPath = @"C:/Users/Alienware/Desktop/FlightSimImageTest/Images";
            string[] imageFiles = Directory.GetFiles(folderPath, "*.PNG");
            //string[] imageFiles = Directory.GetFiles("C:\\Users\\Alienware\\Desktop\\FlightSimImageTest\\Images", "*.PNG");
            //string[] imageFiles = Directory.GetFiles(@"C:/Users/Alienware/Desktop/FlightSimImageTest/Images", "*.PNG");
            /*
            for (int i = 0; i < imageFiles.Length; i++)
            {
                //listImageBox.Source = new BitmapImage(new Uri(imageFiles[i], UriKind.Absolute));
                //imageListBox.Items.Add(imageFiles[i]);
                //Dispatcher.Invoke(() => UpdateImage(imageFiles[i]));

                UpdateImage(imageFiles[i]);
                //Thread.Sleep(1500);
                //--10 images/sec: 10 FPS
                //if (i == 10)
                //{
                    //wait 1 second
                    //await Task.Delay(1500);

               // }
                await Task.Delay(250);
                //imageFiles[i].Dispose();
                //System.IO.File.Delete(imageFiles[i]);

            }
            */
            foreach (var imagePath in imageFiles)
            {
                UpdateImage(imagePath);
                await Task.Delay(300);
                Process[] processes = Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(imagePath));
                foreach (var process in processes)
                {
                    process.Kill();
                }
            }
            //CloseFoldersAndKillProcesses();
            UpdateImage(@"C:/Users/Alienware/Desktop/FlightSimImageTest/testingImage1.PNG");// To release last image  processed      
            imageFiles = null;
            GC.Collect();
        }

        private bool UpdateImage(string imagePath)
        { 
            bool isDone = false;
            //imageListBox.Items.Add(imagePath);
            imageBox.Source = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
            /*
            //===
            //Application.Current.Dispatcher.Invoke(() =>
            //{
                var image = new System.Windows.Controls.Image();
                image.Source = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
                imageBox.Source = image.Source;
                //System.IO.File.Delete(imagePath);
                isDone = true;
            //});
            //===
            */
            //imagePath = null;
            //imageBox = null;
            //CloseFoldersAndKillProcesses();
            GC.Collect();

            
            isDone = true;
            return isDone; //is not use when return
        }

        private void TransferFilesToPi()
        {
            try
            {
                using (var session = new Session())
                {
                    // Connect to the Raspberry Pi
                    session.Open(new SessionOptions
                    {
                        Protocol = Protocol.Sftp,
                        HostName = piIpAddress,
                        UserName = piUsername,
                        Password = piPassword,
                        PortNumber = piPort,
                        //GiveUpSecurityAndAcceptAnySshHostKey = true // Should improve security by checking host key
                        SshHostKeyPolicy = SshHostKeyPolicy.GiveUpSecurityAndAcceptAny

                    });

                    // Specify the local and remote paths
                    string localPath = @"C:\Users\Alienware\Desktop\FlightSimImageTest\Images";
                    string remotePath = "/home/pi/go/code_go/go_executables/p1_tcp/images/newImage0.png";

                    // Transfer files
                    TransferOptions transferOptions = new TransferOptions();
                    transferOptions.TransferMode = TransferMode.Binary;

                    TransferOperationResult transferResult = session.PutFiles(localPath, remotePath, false, transferOptions);

                    // Throw on any error
                    transferResult.Check();

                    //MessageBox.Show("Files transferred successfully.");
                    tbxError2.Text = "Files transferred successfully.";

                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Error transferring files to Raspberry Pi: {ex.Message}");
                tbxError2.Text= $"Error transferring files to Raspberry Pi: {ex.Message}";
            }
        }
        private async Task ReceiveFilesFromPiAsync()
        {
            try
            {
                ReceiveFilesFromPi();
                CloseFoldersAndKillProcesses();
                await Task.Delay(5);
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Error receiving files from Pi: {ex.Message}");
                tbxError2.Text = $"Error receiving files from Pi: {ex.Message}";

            }
        }
        private void ReceiveFilesFromPi()
        {
            string localPath = @"C:\Users\Alienware\Desktop\FlightSimImageTest\Images\";
            string remotePath = "/home/pi/go/code_go/go_executables/p1_tcp/images/*";
            string sndMessage = "0xF210A";
            try
            {

                using (var session = new Session())
                {
                    //-- Connect to the Raspberry Pi
                    //session.ExecutablePath = "C:\\Program Files (x86)\\WinSCP\\WinSCP.exe";
                    //session.DebugLogPath = "winscp.log";
                    session.Timeout = TimeSpan.FromSeconds(5);
                    session.Open(new SessionOptions
                    {
                        Protocol = Protocol.Sftp,
                        HostName = piIpAddress,
                        UserName = piUsername,
                        Password = piPassword,
                        PortNumber = piPort,
                        GiveUpSecurityAndAcceptAnySshHostKey = true // You should improve security by checking host key
                                                                    //SshHostKeyPolicy = SshHostKeyPolicy.GiveUpSecurityAndAcceptAny,
                    });


                    TransferOptions transferOptions = new TransferOptions
                    {
                        TransferMode = TransferMode.Binary,
                        FilePermissions = null,
                        PreserveTimestamp = false,
                    };
                    // Receive files
                    //TransferOptions transferOptions = new TransferOptions();
                    //transferOptions.TransferMode = TransferMode.Binary;
                    TransferOperationResult transferResult = session.GetFiles(remotePath, localPath, false, transferOptions);

                    //-- Throw on any error
                    transferResult.Check();
                    /*
                    foreach (TransferEventArgs transfer in transferResult.Transfers)
                    {
                        session.RemoveFiles(transfer.FileName);
                    }
                    */
                    //sndMessage = toString(transferResult.GetHashCode());
                    //MessageBox.Show("Files received successfully.");
                    tbxError2.Text = "Files received successfully.";
                    //------->>>>>>
                    //CabinWindow objCabinWindow = new CabinWindow();
                    //sndMessage = objCabinWindow.tblXjoystick.Text;
                    //SendTcpMessage("WPF: Await images w/code: "+sndMessage);
                    session.Dispose();
                    //session.Close();
                    //Thread.Sleep(5);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Error 2 receiving files from Pi: {ex.Message}");
                tbxError2.Text = $"Error 2 receiving files from Pi: {ex.Message}";
            }
        }
        private void CloseFoldersAndKillProcesses()
        {
            // Implement code to close folders and kill processes here
            string[] imageFiles = Directory.GetFiles(@"C:\Users\Alienware\Desktop\FlightSimImageTest\Images\", "*.PNG");

            foreach (var imagePath in imageFiles)
            {
                Process[] processes = Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(imagePath));
                foreach (var process in processes)
                {
                    process.Kill();
                }
            }
        }
        private async Task DeleteImages(string folderPath)
        {
            try
            {
                DirectoryInfo directory = new DirectoryInfo(folderPath);

                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }
            }catch(Exception ex)
            {
                //MessageBox.Show($"Error deleting Images: {ex.Message}");
                tbxError3.Text = $"Error 3: deleting Images: {ex.Message}";

            }

        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Clean up resources
            //if (stream != null) { stream.Close(); }
            //if (tcpClient != null) { tcpClient.Close(); }

         }



        //===========================================
  





        //============================================
    }
}