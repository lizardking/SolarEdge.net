using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarEdgeData.TypeConverters
{
    [Flags]
    public enum SolarEdgeMeterEventFlagEnum
    {
        [Description("Loss of power or phase")]
        PowerFailure = 0x4,
        [Description("Voltage below threshold (Phase Loss)")]
        UnderVoltage = 0x8,
        [Description("Power Factor below threshold(can indicate miss - associated voltage and current inputs in three phase systems)")]
        LowPF = 0x10,
        [Description("Current Input over threshold (out of measurement range)")]
        OverCurrent = 0x20,
        [Description("Voltage Input over threshold (out of measurement range)")]
        OverVoltage = 0x40,
        [Description("Sensor not connected")]
        MissingSensor = 0x80,

        Reserved1 = 0x100,
        Reserved2 = 0x200,
        Reserved3 = 0x400,
        Reserved4 = 0x800,
        Reserved5 = 0x1000,
        Reserved6 = 0x2000,
        Reserved7 = 0x4000,
        Reserved8 = 0x8000,



    }
}
