using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarEdgeData.TypeConverters
{

    [Flags]
    public enum SolarEdgeInverterStatusFlagEnum
    {
        [Description("Not Known")]
        NotKnown = 0,
        [Description("Off")]
        Off =1,
        [Description("Sleeping (auto-shutdown) – Night mode")]
        Sleeping = 2,
        [Description("Inverter is ON and producing power")]
        MPPT = 4
    }
}
