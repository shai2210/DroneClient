using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using ControlNew.Data;
using ControlNew.Network;

namespace ControlNew.Drone
{
    public class DroneManager
    {
        enum DataLocation { PackageId = 0, OriginatorAddress = 1, DestinationAddress = 2, Opcode = 3, MaxHops = 4, HopCount = 5, Priority = 6, Propegation = 7, TotalSegments = 8, SegmentNumber = 9, Data = 10 }
        private static DroneManager instance = null;
        private const int NUMBER_OF_FIELDS = 11;
        private HashSet<Package> processedPackageSet;
        private Hashtable partialMessages;
        private static MainWindow _mainWindow;

        public static void SetMainWindow(MainWindow window)
        {
            _mainWindow = window;
        }

        private DroneManager()
        {
            processedPackageSet = new HashSet<Package>();
            partialMessages = new Hashtable();
        }
        public static DroneManager GetInstance()
        {
            if (instance == null)
                instance = new DroneManager();
            return instance;
        }

        public void DataReceived(String data)
        {

            HashSet<Package> packageSet = ParseStringData(data);
            packageSet.ExceptWith(processedPackageSet);
            processedPackageSet.UnionWith(packageSet);
            List<Package> packageList = packageSet.ToList();

            PartialPackagesProcess(ref packageList);
            foreach (Package p in packageList)
            {
                ProcessPackage(p);
            }
        }

        private void PartialPackagesProcess(ref List<Package> package)
        {
            for (int i = 0; i < package.Count; ++i)
            {
                Package p = package[i];
                PartialPackagesProcess(ref p);
            }
        }

        private async static void ProcessPackage(Package p)
        {
            switch (p.Opcode)
            {
                case (byte)Opcode.GPS_RESPONSE:
                    {
                        String[] packageData = p.Data.Split(',');
                        dataFromDrone data = new dataFromDrone();
                        double d;
                        DateTime dt;
                        if (Double.TryParse(packageData[0], out d))
                            data.Lat = d;
                        else
                            return;
                        if (Double.TryParse(packageData[1], out d))
                            data.Lng = d;
                        else
                            return;
                        if (DateTime.TryParse(packageData[1], out dt))
                            data.CurrTime = dt;
                        else
                            return;
                        data.DroneID = p.OriginatorAddress;
                        bool resu = await Proxyhandler.instance.SendDroneStatus(data.DroneID, data.Lat, data.Lng, data.CurrTime, "");
                        _mainWindow.myRoute(data.Lat, data.Lng, data.CurrTime, "");


                        break;
                    }
                case (byte)Opcode.PICTURE_RESPONSE:
                    {

                        //p.Data contains the whole picture
                        break;
                    }
                case (byte)Opcode.VIDEO_PACKAGE:
                    {
                        //p.Data contains an entire video frame
                        //_mainWindow.SetNewImage();
                        break;
                    }
            }
            return;
        }


        private void PartialPackagesProcess(ref Package p)
        {
            if (p.TotalSegments != 0)
            {
                //Creating global unique Id according to the originatorAddress PackageId and Total Segments
                String hash = p.OriginatorAddress.ToString() + p.PackageId.ToString() + p.TotalSegments.ToString();
                //Get segments list if already exist
                ArrayList segmentList = (ArrayList)partialMessages[hash];
                //Create list of segments if it is not already exist
                if (segmentList == null)
                    segmentList = new ArrayList(p.TotalSegments);
                //If the list doesn't contain the current package
                if (!segmentList.Contains(hash))
                {
                    //Add the package to the list
                    segmentList.Add(p);
                    //If this is the missing part
                    if (segmentList.Count == p.TotalSegments)
                    {
                        //Sort the list according the the segment number
                        segmentList.Sort();
                        String data = "";
                        //Concatanate all data the one string
                        foreach (Package pack in segmentList)
                        {
                            data += pack.Data;
                        }
                        //update the package with the whole data
                        p.Data = data;
                        //remove from partial messages hashtable
                        partialMessages.Remove(hash);
                    }
                }
                else
                {
                    //Add package back to list
                    partialMessages[hash] = segmentList;
                }
            }
        }

        private HashSet<Package> ParseStringData(String data)
        {

            String[] messages = data.Split('\n');
            int validFiledsCounter = 0;
            HashSet<Package> packageSet = new HashSet<Package>();
            for (int i = 0; i < messages.Length; ++i)
            {
                String[] fields = messages[i].Split(',');
                if (fields.Length < NUMBER_OF_FIELDS)
                    continue;
                Package p = new Package();
                byte b;
                if (Byte.TryParse(fields[(int)DataLocation.PackageId], out b))
                {
                    p.PackageId = b;
                    validFiledsCounter++;
                }
                if (Byte.TryParse(fields[(int)DataLocation.OriginatorAddress], out b))
                {
                    p.OriginatorAddress = b;
                    validFiledsCounter++;
                }
                if (Byte.TryParse(fields[(int)DataLocation.DestinationAddress], out b))
                {
                    p.DestinationAddress = b;
                    validFiledsCounter++;
                }
                if (Byte.TryParse(fields[(int)DataLocation.Opcode], out b))
                {
                    p.Opcode = b;
                    validFiledsCounter++;
                }
                if (Byte.TryParse(fields[(int)DataLocation.MaxHops], out b))
                {
                    p.MaxHops = b;
                    validFiledsCounter++;
                }
                if (Byte.TryParse(fields[(int)DataLocation.HopCount], out b))
                {
                    p.HopCount = b;
                    validFiledsCounter++;
                }
                if (Byte.TryParse(fields[(int)DataLocation.Priority], out b))
                {
                    p.Priority = b;
                    validFiledsCounter++;
                }
                if (Byte.TryParse(fields[(int)DataLocation.Propegation], out b))
                {
                    p.Propegation = b;
                    validFiledsCounter++;
                }
                if (Byte.TryParse(fields[(int)DataLocation.TotalSegments], out b))
                {
                    p.TotalSegments = b;
                    validFiledsCounter++;
                }
                if (Byte.TryParse(fields[(int)DataLocation.SegmentNumber], out b))
                {
                    p.SegmentNumber = b;
                    validFiledsCounter++;
                }

                p.Data = fields[(int)DataLocation.Data];
                validFiledsCounter++;

                if (validFiledsCounter == NUMBER_OF_FIELDS && !packageSet.Contains(p))
                    packageSet.Add(p);
                validFiledsCounter = 0;
            }
            Console.WriteLine();
            return packageSet;
        }
    }
}
