using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarEdgeData.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ModbusSourceAttribute : Attribute
    {
        public int ValueOffset { get; private set; }
        public int? ScaleFactorOffset { get; set; } 

        public ModbusSourceTypeEnum ValueSourceType { get; set; } = ModbusSourceTypeEnum.int16;
        public ModbusSourceTypeEnum ScaleFactorSourceType { get; set; } = ModbusSourceTypeEnum.int16;

        public ModbusSourceAttribute(int ValueOffset)
        {
            this.ValueOffset = ValueOffset;
        }

        public ModbusSourceAttribute(int ValueOffset, int ScaleFactorOffset) : this(ValueOffset)
        {
            this.ScaleFactorOffset = ScaleFactorOffset;
        }


    }

    public enum ModbusSourceTypeEnum
    {
        int16,
        uint32,
        uint16
    }

}
