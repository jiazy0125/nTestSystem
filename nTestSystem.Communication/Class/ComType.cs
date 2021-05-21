using System;
using System.Collections.Generic;
using System.Text;

namespace nTestSystem.Communication.Class
{
    public enum ComType
    {
        SerialPort,
        GPIB,
        TCP,
        USB,
        UDP,
        CAN,
        None,
    }
}
