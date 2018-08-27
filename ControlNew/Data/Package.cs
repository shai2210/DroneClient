using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace ControlNew
{
    public class Package : IComparer
    {
        private byte packageId;
        private byte originatorAddress;
        private byte destinationAddress;
        private byte maxHops;
        private byte hopCount;
        private byte opcode;
        private byte priority;
        private byte propegation;
        private byte totalSegments;
        private byte segmentNumber;
        private String data;

        public Package() { }

        public Package(byte packageId, byte originatorAddress, byte destinationAddress, byte opcode, String data, byte maxHops = 5, byte hopCount = 0, byte priority = 1, byte propegation = 1, byte totalSegments = 0, byte segmentNumber = 0)
        {
            this.packageId = packageId;
            this.originatorAddress = originatorAddress;
            this.destinationAddress = destinationAddress;
            this.opcode = opcode;
            this.data = data;
            this.maxHops = maxHops;
            this.hopCount = hopCount;
            this.priority = priority;
            this.propegation = propegation;
            this.totalSegments = totalSegments;
            this.segmentNumber = segmentNumber;

        }

        public Package(byte destinationAddress, byte opcode, String data, byte maxHops = 5, byte hopCount = 0, byte priority = 1, byte propegation = 1, byte totalSegments = 0, byte segmentNumber = 0)
        {
            this.originatorAddress = 1;
            this.destinationAddress = destinationAddress;
            this.opcode = opcode;
            this.data = data;
            this.maxHops = maxHops;
            this.hopCount = hopCount;
            this.priority = priority;
            this.propegation = propegation;
            this.totalSegments = totalSegments;
            this.segmentNumber = segmentNumber;

        }

        public byte PackageId
        {
            get { return packageId; }
            set { packageId = value; }
        }

        public byte OriginatorAddress
        {
            get { return originatorAddress; }
            set { originatorAddress = value; }
        }

        public byte DestinationAddress
        {
            get { return destinationAddress; }
            set { destinationAddress = value; }
        }
        public byte Opcode
        {
            get { return opcode; }
            set { opcode = value; }
        }
        public byte MaxHops
        {
            get { return maxHops; }
            set { maxHops = value; }
        }
        public byte HopCount
        {
            get { return hopCount; }
            set { hopCount = value; }
        }
        public byte Priority
        {
            get { return priority; }
            set { priority = value; }
        }
        public byte Propegation
        {
            get { return propegation; }
            set { propegation = value; }
        }
        public byte TotalSegments
        {
            get { return totalSegments; }
            set { totalSegments = value; }
        }
        public byte SegmentNumber
        {
            get { return segmentNumber; }
            set { segmentNumber = value; }
        }
        public String Data
        {
            get { return data; }
            set { data = value; }
        }

        public override string ToString()
        {
            String s = originatorAddress + "," + DestinationAddress + "," + Opcode +
                "," + MaxHops + "," + HopCount + "," + Priority + ","
                + Propegation + "," + TotalSegments + "," + SegmentNumber + "," + Data + '\n';
            return s;
        }

        public int Compare(object x, object y)
        {
            Package p = (Package)x;
            Package p1 = (Package)y;
            if (p.segmentNumber > p1.segmentNumber)
                return 1;
            if (p.segmentNumber < p1.segmentNumber)
                return -1;
            return 0;
        }
    }
}
