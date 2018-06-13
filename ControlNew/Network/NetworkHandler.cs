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
            
            try
            {
                bool res = await S3Handler.instance.FileUpload(fileName);
                if (res)
                {
                    bool resu = await Proxyhandler.instance.SendDroneStatus(2, 93, 23, "ggg");
                    Console.WriteLine(resu);
                    //Delete photo
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        
    }
}
