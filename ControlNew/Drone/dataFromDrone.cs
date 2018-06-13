using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media;
//manage the data that came from the arduino 

namespace ControlNew
{
    public class dataFromDrone
    {
        int droneId;
        double lat;
        double lng;
        DateTime currtime;
        ImageSource image;

        public int DroneID { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public DateTime CurrTime { get; set; }
        public ImageSource ImageSrc { get; set; }
    }
}
