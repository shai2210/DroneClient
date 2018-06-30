using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using ControlNew.Network;
using static System.Net.Mime.MediaTypeNames;

namespace ControlNew.CORE
{
    public static class OperationManager
    {
        //sec
        private static MainWindow _mainWindow;
        static BackgroundWorker s3;

        public static void Init()
        {
            s3 = new BackgroundWorker();
            s3.DoWork += s3_DoWork;
            s3.RunWorkerCompleted += s3_RunWorkerCompleted;
        }
        public static void HandleDroneData(dataFromDrone data)
        {
            //What to do when receive drone data

            if (_mainWindow != null)
            {
                for (int i = 1; i < 9; i++)
                {

                    
                }

                    //Demo 
                    //for (int i = 1; i < 9; i++)
                    //{
                    //    // Application.DoEvents(); //allow windows to execute all pending tasks including your image show...

                    //    System.Threading.Thread.Sleep(1000);
                    //    _mainWindow.CurrentImage.Source = new BitmapImage(new Uri("../images/1.png", UriKind.Relative));
                    //    //ImageViewer1.Source = new BitmapImage(new Uri(@"\myserver\folder1\Customer Data\sample.png"));
                    //}


                    //_mainWindow.SetNewImage(data.ImageSrc);//2- change image
                    //Demo
                    //_mainWindow.SetNewImage);
            }
            s3.RunWorkerAsync(); //3-upload image //4-upload data to server

        }

        //gives OperationManager accses to main window
        public static void SetMainWindow(MainWindow window)
        {
            _mainWindow = window;
        }

        //uploads data to S3 and do post
        private async static void s3_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 1; i < 9; i++)
            {
                // Application.DoEvents(); //allow windows to execute all pending tasks including your image show...
                
                System.Threading.Thread.Sleep(1000);
                // _mainWindow.CurrentImage.Source = new BitmapImage(new Uri("../images/1.png", UriKind.Relative));
                //ImageViewer1.Source = new BitmapImage(new Uri(@"\myserver\folder1\Customer Data\sample.png"));
                //  _mainWindow.SetNewImage();
                DateTime time =  DateTime.Now;

                DateTime curr = DateTime.Now;
                if(i==1)
                {
                    _mainWindow.myRoute(15.475412 + i * 0.00029, -90.990448 - i * 0.00029, curr, "https://s3-eu-west-1.amazonaws.com/drones-bucket/n1.png");
                    bool resu = await Proxyhandler.instance.SendDroneStatus(1, 15.475412 + i * 0.00029, -90.990448 - i * 0.00029, time, "https://s3-eu-west-1.amazonaws.com/drones-bucket/n1.png");
                    System.Threading.Thread.Sleep(500);
                    bool res = await Proxyhandler.instance.SendDroneStatus(2, 15.475412 - i * 0.00029, -90.990448 + i * 0.00029, time, "https://s3-eu-west-1.amazonaws.com/drones-bucket/IMG_0001.jpg");
                    System.Threading.Thread.Sleep(500);
                    bool re = await Proxyhandler.instance.SendDroneStatus(3, 15.475412 + i * 0.00029, -90.990448 + i * 0.00029, time, "https://s3-eu-west-1.amazonaws.com/drones-bucket/IMG_0001.jpg");
                    System.Threading.Thread.Sleep(500);

                }

                if (i % 4 == 0)
                {
                    
                    _mainWindow.myRoute(15.475412 + i * 0.00029, -90.990448 - i * 0.00029, curr, "https://s3-eu-west-1.amazonaws.com/drones-bucket/n7.png");
                    bool resu = await Proxyhandler.instance.SendDroneStatus(1, 15.475412 + i * 0.00029, -90.990448 - i * 0.00029, time, "https://s3-eu-west-1.amazonaws.com/drones-bucket/n7.png");
                    System.Threading.Thread.Sleep(500);
                    bool res = await Proxyhandler.instance.SendDroneStatus(2, 15.475412 - i * 0.00029, -90.990448 + i * 0.00029, time, "https://s3-eu-west-1.amazonaws.com/drones-bucket/IMG_0001.jpg");
                    System.Threading.Thread.Sleep(500);
                    bool re = await Proxyhandler.instance.SendDroneStatus(3, 15.475412 + i * 0.00029, -90.990448 + i * 0.00029, time, "https://s3-eu-west-1.amazonaws.com/drones-bucket/IMG_0001.jpg");
                    System.Threading.Thread.Sleep(500);
                }
                if (i % 3 == 0)
                {
                    //_mainWindow.SetNewImage();
                    _mainWindow.myRoute(15.475412, -90.990448 - i * 0.00029, curr, null);
                    bool resu = await Proxyhandler.instance.SendDroneStatus(1, 15.475412, -90.990448 - i * 0.00029, time, "");
                    //          _mainWindow.myRoute(15.475412 + i * 0.00029, -90.990448 - i * 0.00029, new DateTime(2011, 6, 10));
                    System.Threading.Thread.Sleep(500);
                    bool res = await Proxyhandler.instance.SendDroneStatus(2, 15.475412, -90.990448 + i * 0.00029, time, "");
                    System.Threading.Thread.Sleep(500);
                    bool re = await Proxyhandler.instance.SendDroneStatus(3, 15.475412, -90.990448 + i * 0.00029, time, "");
                    System.Threading.Thread.Sleep(500);

                }
                if (i % 5 == 0)
                {
                    //_mainWindow.SetNewImage();
                    _mainWindow.myRoute(15.475412 + i * 0.00029, -90.990448, curr, null);
                    bool resu = await Proxyhandler.instance.SendDroneStatus(1, 15.475412 + i * 0.00029, -90.990448, time, "");
                    //        _mainWindow.myRoute(15.475412 + i * 0.00029, -90.990448 - i * 0.00029, new DateTime(2011, 6, 10));
                    System.Threading.Thread.Sleep(500);
                    bool res = await Proxyhandler.instance.SendDroneStatus(2, 15.475412 - i * 0.00029, -90.990448, time, "");
                    System.Threading.Thread.Sleep(500);
                    bool re = await Proxyhandler.instance.SendDroneStatus(3, 15.475412 + i * 0.00029, -90.990448, time, "");
                    System.Threading.Thread.Sleep(500);

                }
                if (i % 7 == 0)
                {
                    //_mainWindow.SetNewImage();
                    _mainWindow.myRoute(15.475412 + i * 0.00029, -90.990448 - i * 0.00029, curr, "https://s3-eu-west-1.amazonaws.com/drones-bucket/n6.png");
                    bool resu = await Proxyhandler.instance.SendDroneStatus(1, 15.475412 + i * 0.00029, -90.990448 - i * 0.00029, time, "https://s3-eu-west-1.amazonaws.com/drones-bucket/n6.png");
                    //      _mainWindow.myRoute(15.475412 + i * 0.00029, -90.990448 - i * 0.00029, new DateTime(2011, 6, 10));

                    System.Threading.Thread.Sleep(500);
                    bool res = await Proxyhandler.instance.SendDroneStatus(2, 15.475412 - i * 0.00029, -90.990448 + i * 0.00029, time, "https://s3-eu-west-1.amazonaws.com/drones-bucket/IMG_0001.jpg");
                    System.Threading.Thread.Sleep(500);
                    bool re = await Proxyhandler.instance.SendDroneStatus(3, 15.475412 + i * 0.00029, -90.990448 + i * 0.00029, time, "https://s3-eu-west-1.amazonaws.com/drones-bucket/IMG_0001.jpg");
                    System.Threading.Thread.Sleep(500);

                }

            }
            NetworkHandler.UploadImageToS3("IMG_0001.jpg");

        }

        //ca be dipause
        private static void s3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //pic saved on s3

            _mainWindow.UpdateDoneUpload();

        }
    }
}
