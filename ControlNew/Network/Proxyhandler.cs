﻿using System.Text;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System;

namespace ControlNew.Network
{
    public class Proxyhandler
    {
        private static Proxyhandler Instance;
        private const string baseURL = "";
        private Proxyhandler()
        {

        }

        public static Proxyhandler instance
        {
            get
            {
                if (Instance == null)
                {
                    Instance = new Proxyhandler();
                }
                return Instance;
            }
        }

        public Task<bool> SendDroneStatus(int id, double lat, double lng, DateTime time, string uRL)
        {
            
            var request = (HttpWebRequest)WebRequest.Create("http://18.218.63.225/api.php");

            var postData = "action=insertById";
            postData += "&id=" + id; // drone id 
            postData += "&lat=" + lat;
            postData += "&long=" + lng;
            postData += "&time=" + time;
            postData += "&image=" + uRL;// is url to image that saved on s3 bucket
            var data = Encoding.ASCII.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
             return Task.FromResult(true); ;
        }

    }
}
