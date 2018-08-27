using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using ControlNew.Network;
using System.ComponentModel;
using ControlNew.Drone;
using ControlNew.CORE;
using ControlNew.Data;

namespace ControlNew
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //   private static readonly HttpClient client = new HttpClient();
        int photo;//for presntation
        private SerialPort port;
        bool armBtnChecked = false;
        bool isConnected = false;
        BackgroundWorker s3;
        BackgroundWorker changeWorker;
        string readFromStream;
        dataFromDrone data = new dataFromDrone();
        public MainWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;//disable sizing options
            OperationManager.Init();
            //gives OperationManager accses to main window
            OperationManager.SetMainWindow(this);
           // s3 = new BackgroundWorker();
           // changeWorker = new BackgroundWorker();

            //gives promition to HTML/pilot
            string curdir = Directory.GetCurrentDirectory();
            Gmaps.Navigate(String.Format("file:///{0}/HTML/pilot.html", curdir));

            
            refreshCom();

            //s3 upload start worker
     //       s3.DoWork += s3_DoWork;
       //     s3.RunWorkerCompleted += s3_RunWorkerCompleted;

            //get info start worker
         //   changeWorker.DoWork += changeWorker_DoWork;
           // changeWorker.RunWorkerCompleted += changeWorker_RunWorkerCompleted;   
        }
        ////data have been accepted parse data send to s3 change pic send to sever 
        //private void changeWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    DateTime thisDate1 = new DateTime(2011, 6, 10);

        //    //readFromStream = (string)e.Result;

        //    //parse info and call setters 
        //    data.DroneID = 9;
           
        //    //change pic + update route
        //    myRoute(13, 18 ,thisDate1);
        //    myRoute(13, 19, thisDate1);

        //    //upload to s3 will get pic to send will return true sucess or false fail
        //    s3.RunWorkerAsync();
        //   // NetworkHandler.UploadImageToS3("IMG_0001.jpg");
        //    //post to server will return true sucess or false fail
        //}

        ////reads data from arduino
        //private void changeWorker_DoWork(object sender, DoWorkEventArgs e)
        //{
        //        //e.Result = port.ReadExisting();
        //}

        ////upload to s3
        //private void s3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    //pic saved on s3
            
        //    uploadLbl.Content = "uploaded";

        //}

        //private void s3_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    NetworkHandler.UploadImageToS3("IMG_0001.jpg");
        //}

        //get coms atteched to the computer
        private void refreshCom(){

            comboBox1.Items.Clear();
            var ports = SerialPort.GetPortNames();
            for (int i = 0; i < ports.Length; i++)
            {
                comboBox1.Items.Add(ports[i]);
            }
            //comboBox1.Items.Add(SerialPort.GetPortNames());
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedItem = comboBox1.Items[0];
                //ConnectBtn.Content="Disconnect";
            }

        }

        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string inData = sp.ReadExisting();
            DroneManager.GetInstance().DataReceived(inData);


        }

        private void connectToArduino()
        {

            try
            {
                string selectedPort = comboBox1.SelectedItem.ToString();
                port = new SerialPort(selectedPort, 9600, Parity.None, 8, StopBits.One);
                port.Open();
           }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("arduino connect faild", e.Source);
                throw;
            }
        }

        private void forBtn_Click(object sender, EventArgs e)
        {
            if (port != null && port.IsOpen)
            {
                Package package = new Package(2, (byte)Opcode.MOVE_REQUEST, "21700");
                port.Write(package.ToString());

                Thread.Sleep(100);
                Package package1 = new Package(2, (byte)Opcode.MOVE_REQUEST, "21500");
                port.Write(package1.ToString());
           //     List<Package> commandsList = new List<Package>();
           //     commandsList.Add(package);
           //     commandsList.Add(package1);
            } 
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            Package package = new Package(2, (byte)Opcode.MOVE_REQUEST, "21300");
            port.Write(package.ToString());

            Thread.Sleep(100);
            Package package1 = new Package(2, (byte)Opcode.MOVE_REQUEST, "21500");
            port.Write(package1.ToString());
            
            //List<Package> commandsList = new List<Package>();
            //commandsList.Add(package);
            //commandsList.Add(package1);
        }

        private void rightBtn_Click(object sender, EventArgs e)
        {
            if (port != null && port.IsOpen)
            {
                Package package = new Package(2, (byte)Opcode.MOVE_REQUEST, "11700");
                port.Write(package.ToString());

                Thread.Sleep(100);
                Package package1 = new Package(2, (byte)Opcode.MOVE_REQUEST, "11500");
                port.Write(package.ToString());
                //port.Write("11700");
                //Thread.Sleep(100);
                //port.Write("11500");
                //List<Package> commandsList = new List<Package>();
                //commandsList.Add(package);
                //commandsList.Add(package1);   
            }
        }

        private void leftBtn_Click(object sender, EventArgs e)
        {
            if (port != null && port.IsOpen)
            {
                Package package = new Package(2, (byte)Opcode.MOVE_REQUEST, "11300");
                port.Write(package.ToString());

                Thread.Sleep(100);
                Package package1 = new Package(2, (byte)Opcode.MOVE_REQUEST, "11500");
                port.Write(package.ToString());
              //  List<Package> commandsList = new List<Package>();
              //  commandsList.Add(package);
              //  commandsList.Add(package1);
              
            }   
        }

        //throutle value
        private void thrBar_Scroll(object sender, EventArgs e)
        {
            if (port != null && port.IsOpen)
            {
                Package package = new Package(2, (byte)Opcode.MOVE_REQUEST, "0" + thrSlider.Value);
                port.Write(package.ToString());

            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            isConnected = !isConnected;
            if (isConnected)
            {
                string selectedPort = comboBox1.SelectedItem.ToString();
                connectToArduino();
                ConnectBtn.Content = "Disconnect";
            }
            else
            {
               // DroneHelper.Close();
                //port.Close();
                ConnectBtn.Content = "Connect";
            }
        }

        //arm and disarm motors the drone will not flight when it disarm
        private void armButton_CheckedChanged(object sender, EventArgs e)
        {
            armBtnChecked = !armBtnChecked;
            if (armBtnChecked)
            {
                armButton.Content = "DISARM Motors";
                if (port != null && port.IsOpen)
                {
                    Package package = new Package(2, (byte)Opcode.MOVE_REQUEST, "41900");
                    port.Write(package.ToString());
                }
            }
            else
            {
                armButton.Content = "ARM Motors";
     
                if (port != null && port.IsOpen)
                {
                    Package package = new Package(2, (byte)Opcode.MOVE_REQUEST, "41300");
                    port.Write(package.ToString());
                }
                    
            }
        }

        //gets all coms connected to the computer
        private void refreshBtn_Click(object sender, EventArgs e)
        {
            refreshCom();
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromSeconds(5);

            //changeWorker.RunWorkerAsync();
            OperationManager.HandleDroneData(null);
            

        }

        //get lat long ant time and display it on map
        public void myRoute(double lat , double lng ,DateTime time,string url )
        {
            
            //invoke the main window thread 
            Dispatcher.Invoke(() =>
            {
                photo++;
                CurrentImage.Source = new BitmapImage(new Uri("../images/n" + photo + ".png", UriKind.Relative));
 
                dynamic doc = Gmaps.Document;
                Gmaps.InvokeScript("drawRoute", new Object[] { lat, lng, time.ToString(),url });
            });
        }

        //get image and change image at pilot screen
        //public void SetNewImage(ImageSource img)
        public void SetNewImage()
        {
            photo++;
          
            Dispatcher.Invoke(() =>
            {
                CurrentImage.Source = new BitmapImage(new Uri("../images/n" + photo + ".png", UriKind.Relative));
                photo++;
            });
          
        }
    }
}
