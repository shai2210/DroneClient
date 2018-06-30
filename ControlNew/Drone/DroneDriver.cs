using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ControlNew.Drone
{
    public class DroneDriver
    {
        private static SerialPort port;//port to arduino
        static Timer readingTimer = new Timer(ReadingTimer_Tick); //timer to read data from arduino
        public static  bool isConnected = false;//is the arduino connected to me
        static string data = "";//data from arduino

        //connect to arduino
        public void Connect(string selectedPort)
        {
            try
            {
                port = new SerialPort(selectedPort, 9600, Parity.None, 8, StopBits.One);
                port.Open();
                isConnected = true;
                readingTimer.Change(-1, 0);//start lisitnig loop ReadingTimer_Tick
               
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("arduino connect faild", e.Source);
                throw;
            }
        }

        //reading data from arduino
        private static void ReadingTimer_Tick(object state)
        {
            data = port.ReadExisting();
            if (!string.IsNullOrEmpty(data))
            {
                //SEND TO WHO NEEDED
                DroneHelper.RecievedData(data);
            }
            data = "";
            readingTimer.Change(-1, 0);
        }

        //write to arduino
        public void WriteCommand(string command)
        {
            try
            {
                port.Write(command);
            }
            catch (Exception ex)
            {
                if (ex.Source != null)
                    Console.WriteLine("faild to write command to arduino", ex.Source);
                throw;
            }
        }

        //cloe the connection
        internal void Close()
        {
            port.Close();
            isConnected = false;
        }

        //reading func
        //write func'
    }
}
