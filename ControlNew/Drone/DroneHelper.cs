using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ControlNew.CORE;

namespace ControlNew.Drone
{
    public static class DroneHelper
    {
        private static DroneDriver droneDriver;
        static Timer executeCommandTimer = new Timer(OnExecuteTick);
        private static Stack<string> commandsStack = new Stack<string>();
        
        //if arduino connected get connected state from DroneDriver 
        public static bool IsConnected
        {
            get
            {
                if(droneDriver !=null)
                {
                    return DroneDriver.isConnected;
                }
                return false;
            }
        }

        
        //write one line commend to the commandstack
        public static void WriteCommand(string cmd)
        {
            commandsStack.Push(cmd);
            executeCommandTimer.Change(-1, 0);
        }

        //write multi lines commend to the commandstack
        public static void WriteCommand(List<string> cmds)
        {
            //writeThread.Start();
            for (int i = cmds.Count - 1; i >= 0; i--)
            {
                commandsStack.Push(cmds[i]);
            }
            executeCommandTimer.Change(-1, 0);
        }

        //after finish getting all commands into commandstack send to dronedriver in order to execute 
        private static void OnExecuteTick(object state)
        {
            if (commandsStack.Count > 0)
            {
                droneDriver.WriteCommand(commandsStack.Pop());
                executeCommandTimer.Change(0, 100);//white 100 ms
            }
        }

        //dronedriver send data to the handler in order to send to s3 proxy and main window 
        internal static void RecievedData(string data)
        {
            //Parse to Drone data obj
            dataFromDrone dataDrone = new dataFromDrone();


            OperationManager.HandleDroneData(dataDrone);
        }

        //first connect to drone
        public static void ConnectToDrone(string serialPort)
        {
            if(droneDriver == null)
            {
                droneDriver = new DroneDriver();
            }
            droneDriver.Connect(serialPort);
        }

        //closes the arduino connection 
        public static void Close()
        {
           if(droneDriver != null)
            {
                droneDriver.Close();
            }
        }
    }
}