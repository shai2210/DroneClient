using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlNew.Data
{
    public enum Opcode
    {
        MOVE_REQUEST = 0,
        GPS_REQUEST = 1,
        PICTURE_REQUEST = 2,
        GPS_RESPONSE = 3,
        PICTURE_RESPONSE = 4,
        VIDEO_PACKAGE = 5
    }
}
