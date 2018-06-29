using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            
            if(_mainWindow != null)
            {
                
                //Demo
                for (int i = 1; i < 9; i++)
                {   
                    System.Threading.Thread.Sleep(1000);
                    DateTime curr = DateTime.Now;

                    if (i % 4 == 0)
                    {
                        
                        _mainWindow.myRoute(15.475412 + i * 0.00029, -90.990448 - i * 0.00029, curr, "https://s3-eu-west-1.amazonaws.com/drones-bucket/IMG_0001.jpg");

                    }
                    if (i % 3 == 0)
                    {
                    
                       _mainWindow.myRoute(15.475412, -90.990448 - i * 0.00029, curr,null);

                    }
                    if (i % 5 == 0)
                    {
                     
                        _mainWindow.myRoute(15.475412 + i * 0.00029, -90.990448, curr,null);

                    }
                    if (i % 7 == 0)
                    {
                        _mainWindow.myRoute(15.475412 + i * 0.00029, -90.990448 - i * 0.00029, curr, "https://s3-eu-west-1.amazonaws.com/drones-bucket/IMG_0001.jpg");

                    }

                }
                

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
        private  static void s3_DoWork(object sender, DoWorkEventArgs e)
        {
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
