﻿using System;
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
        
        private static MainWindow _mainWindow;
        static BackgroundWorker s3;

        public static void Init()
        {
            s3 = new BackgroundWorker();
            s3.DoWork += s3_DoWork;
            s3.RunWorkerCompleted += s3_RunWorkerCompleted;
        }

        //gives OperationManager accses to main window
        public static void SetMainWindow(MainWindow window)
        {
            _mainWindow = window;
        }

        public async static void HandleDroneData(dataFromDrone rcvData)
        {
            s3.RunWorkerAsync("'");
        }


        //uploads data to S3 and do post simulation mode
        private async static void s3_DoWork(object sender, DoWorkEventArgs e)
        {

            for (int i = 1; i < 9; i++)
            {


                System.Threading.Thread.Sleep(3000);

                DateTime time = DateTime.Now;

                DateTime curr = DateTime.Now;
                if (i == 1)
                {
                    
                    _mainWindow.myRoute(15.475412, -90.990448, curr, "https://s3-eu-west-1.amazonaws.com/drones-bucket2/n1.png");
                    _mainWindow.SetNewImageSim();
                    bool resu = await Proxyhandler.instance.SendDroneStatus(1, 15.475412 + i * 0.00029, -90.990448 - i * 0.00029, time, "https://s3-eu-west-1.amazonaws.com/drones-bucket2/n1.png");
                    System.Threading.Thread.Sleep(500);
                    bool res = await Proxyhandler.instance.SendDroneStatus(2, 15.475412 - i * 0.00029, -90.990448 + i * 0.00029, time, "https://s3-eu-west-1.amazonaws.com/drones-bucket2/IMG_0001.jpg");
                    System.Threading.Thread.Sleep(500);
                    bool re = await Proxyhandler.instance.SendDroneStatus(3, 15.475412 + i * 0.00029, -90.990448 + i * 0.00029, time, "https://s3-eu-west-1.amazonaws.com/drones-bucket2/IMG_0001.jpg");
                    System.Threading.Thread.Sleep(500);

                }

                if (i % 2 == 0)
                {

                    _mainWindow.myRoute(15.475412 + i * 0.00029, -90.990448 - i * 0.00029, curr, "https://s3-eu-west-1.amazonaws.com/drones-bucket2/n7.png");
                    _mainWindow.SetNewImageSim();

                    bool resu = await Proxyhandler.instance.SendDroneStatus(1, 15.475412 + i * 0.00029, -90.990448 - i * 0.00029, time, "https://s3-eu-west-1.amazonaws.com/drones-bucket2/n7.png");
                    System.Threading.Thread.Sleep(500);
                    bool res = await Proxyhandler.instance.SendDroneStatus(2, 15.475412 - i * 0.00029, -90.990448 + i * 0.00029, time, "https://s3-eu-west-1.amazonaws.com/drones-bucket/IMG_0001.jpg");
                    System.Threading.Thread.Sleep(500);
                    bool re = await Proxyhandler.instance.SendDroneStatus(3, 15.475412 + i * 0.00029, -90.990448 + i * 0.00029, time, "https://s3-eu-west-1.amazonaws.com/drones-bucket/IMG_0001.jpg");
                    System.Threading.Thread.Sleep(500);
                }
                if (i % 3 == 0)
                {
                    _mainWindow.myRoute(15.475412, -90.990448 - i * 0.00029, curr, null);
                    _mainWindow.SetNewImageSim();

                    bool resu = await Proxyhandler.instance.SendDroneStatus(1, 15.475412, -90.990448 - i * 0.00029, time, "");
                    System.Threading.Thread.Sleep(500);
                    bool res = await Proxyhandler.instance.SendDroneStatus(2, 15.475412, -90.990448 + i * 0.00029, time, "");
                    System.Threading.Thread.Sleep(500);
                    bool re = await Proxyhandler.instance.SendDroneStatus(3, 15.475412, -90.990448 + i * 0.00029, time, "");
                    System.Threading.Thread.Sleep(500);

                }
                if (i % 5 == 0)
                {
                    _mainWindow.myRoute(15.475412 + i * 0.00029, -90.990448, curr, null);
                    _mainWindow.SetNewImageSim();

                    bool resu = await Proxyhandler.instance.SendDroneStatus(1, 15.475412 + i * 0.00029, -90.990448, time, "");
                    System.Threading.Thread.Sleep(500);
                    bool res = await Proxyhandler.instance.SendDroneStatus(2, 15.475412 - i * 0.00029, -90.990448, time, "");
                    System.Threading.Thread.Sleep(500);
                    bool re = await Proxyhandler.instance.SendDroneStatus(3, 15.475412 + i * 0.00029, -90.990448, time, "");
                    System.Threading.Thread.Sleep(500);
                }
                if (i % 7 == 0)
                {
                    _mainWindow.myRoute(15.475412 + i * 0.00029, -90.990448 - i * 0.00029, curr, "https://s3-eu-west-1.amazonaws.com/drones-bucket2/n6.png");

                    _mainWindow.SetNewImageSim();

                    bool resu = await Proxyhandler.instance.SendDroneStatus(1, 15.475412 + i * 0.00029, -90.990448 - i * 0.00029, time, "https://s3-eu-west-1.amazonaws.com/drones-bucket2/n6.png");
                    System.Threading.Thread.Sleep(500);
                    bool res = await Proxyhandler.instance.SendDroneStatus(2, 15.475412 - i * 0.00029, -90.990448 + i * 0.00029, time, "https://s3-eu-west-1.amazonaws.com/drones-bucket/IMG_0001.jpg");
                    System.Threading.Thread.Sleep(500);
                    bool re = await Proxyhandler.instance.SendDroneStatus(3, 15.475412 + i * 0.00029, -90.990448 + i * 0.00029, time, "https://s3-eu-west-1.amazonaws.com/drones-bucket/IMG_0001.jpg");
                    System.Threading.Thread.Sleep(500);

                }

            }

            NetworkHandler.UploadImageToS3("IMG_0001.jpg");
        }

        //the worker finished the simulation 
        private static void s3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            _mainWindow.showLabel();
        }
    }
}

