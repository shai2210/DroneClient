using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlNew.Network
{
    public static class NetworkHandler
    {
        //tries to upload to s3 server will get true if the upload sucsess and false if failed
        public static async void UploadImageToS3(string fileName)
        {
            //int id, double lat, double lng, string uRL
            DateTime time = new DateTime(1985,10,22,10,22,22);
            for (int i = 0; i < 10; i++)
            {
                if (i %4== 0) {
                    bool resu = await Proxyhandler.instance.SendDroneStatus(1, 15.475412 + i * 0.00029, -90.990448 - i * 0.00029, time, "https://s3-eu-west-1.amazonaws.com/drones-bucket/IMG_0001.jpg");
                    System.Threading.Thread.Sleep(3000);
                    bool res = await Proxyhandler.instance.SendDroneStatus(2, 15.475412 - i * 0.00029, -90.990448 + i * 0.00029, time, "https://s3-eu-west-1.amazonaws.com/drones-bucket/IMG_0001.jpg");
                    System.Threading.Thread.Sleep(3000);
                    bool re = await Proxyhandler.instance.SendDroneStatus(3, 15.475412 + i * 0.00029, -90.990448 + i * 0.00029, time, "https://s3-eu-west-1.amazonaws.com/drones-bucket/IMG_0001.jpg");
                }
                if (i%3==0)
                {
                    bool resu = await Proxyhandler.instance.SendDroneStatus(1, 15.475412  , -90.990448 - i * 0.00029, time, "");
                    System.Threading.Thread.Sleep(3000);
                    bool res = await Proxyhandler.instance.SendDroneStatus(2, 15.475412  , -90.990448 + i * 0.00029, time, "");
                    System.Threading.Thread.Sleep(3000);
                    bool re = await Proxyhandler.instance.SendDroneStatus(3, 15.475412  , -90.990448 + i * 0.00029, time, "");
                }
                if(i%5 == 0)
                {
                    bool resu = await Proxyhandler.instance.SendDroneStatus(1, 15.475412 + i * 0.00029, -90.990448, time, "");
                    System.Threading.Thread.Sleep(3000);
                    bool res = await Proxyhandler.instance.SendDroneStatus(2, 15.475412 - i * 0.00029, -90.990448, time, "");
                    System.Threading.Thread.Sleep(3000);
                    bool re = await Proxyhandler.instance.SendDroneStatus(3, 15.475412 + i * 0.00029, -90.990448 , time, "");

                }
                System.Threading.Thread.Sleep(3000);
            }
            //Console.WriteLine(resu);
            //try
            //{
            //    bool res = await S3Handler.instance.FileUpload(fileName);
            //    if (res)
            //    {
            // //       DateTime time = new DateTime(1985, 10, 22);
            //   //     bool resu = await Proxyhandler.instance.SendDroneStatus(2, 93, 23,time, "https://s3-eu-west-1.amazonaws.com/drones-bucket/IMG_0001.jpg");
            //     //   Console.WriteLine(resu);
            //        //Delete photo
            //    }
            //}
            //catch(Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
        }

        
    }
}
