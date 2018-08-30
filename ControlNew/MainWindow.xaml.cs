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
        int photo = 0;//for simulation
        private SerialPort port;
        bool armBtnChecked = false;
        bool isConnected = false;

        public MainWindow()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;//disable sizing options
            DroneManager.SetMainWindow(this);

            //gives promition to HTML/pilot
            string curdir = Directory.GetCurrentDirectory();

            Gmaps.Navigate(String.Format("file:///{0}/HTML/pilot.html", curdir));


            refreshCom();
        }

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
            if (port != null && port.IsOpen)
            {
                Package package = new Package(2, (byte)Opcode.MOVE_REQUEST, "21300");
                port.Write(package.ToString());

                Thread.Sleep(100);
                Package package1 = new Package(2, (byte)Opcode.MOVE_REQUEST, "21500");
                port.Write(package1.ToString());
            }
            
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

        }

        //get lat long ant time and display it on map
        public void myRoute(double lat , double lng ,DateTime time,string url )
        {
            
            //invoke the main window thread 
            Dispatcher.Invoke(() =>
            {
                dynamic doc = Gmaps.Document;
                Gmaps.InvokeScript("drawRoute", new Object[] { lat, lng, time.ToString(),url });
            });
        }

        //changes pic 
        public void SetNewImage(BitmapImage pic)
        {
            Dispatcher.Invoke(() =>
            {
                CurrentImage.Source = pic;
              
            });

        }


        /*
         *this is the simulation section 
         * it will send hard coded data to aerver SQL and s3
         * you can check it and to see it realy works
         */

        //starting simulation mode
        private void sim_btn(object sender, RoutedEventArgs e)
        {
            OperationManager.Init();
            OperationManager.SetMainWindow(this);
            OperationManager.HandleDroneData(null);
            
        }

        //get image from images folder and change image at pilot screen in simulation mode
        public void SetNewImageSim()
        {
            photo++;
          
            Dispatcher.Invoke(() =>
            {
                CurrentImage.Source = new BitmapImage(new Uri("../images/n" + photo + ".png", UriKind.Relative));
                photo++;
            });
          
        }

        public void showLabel()
        {
            simLabel.Visibility = Visibility.Visible;
        } 

       
    }
}
