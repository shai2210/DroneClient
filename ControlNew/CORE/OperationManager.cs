using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlNew.Network;

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
        public static void HandleDroneData(dataFromDrone data)
        {
            //What to do when receive drone data
            
            if(_mainWindow != null)
            {
                //_mainWindow.myRoute(data.Lat, data.Lng, data.CurrTime);//1 - draw
                //Demo
                _mainWindow.myRoute(13, 18, new DateTime(2011, 6, 10));
                _mainWindow.myRoute(13,19, new DateTime(2011, 6, 10));

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
