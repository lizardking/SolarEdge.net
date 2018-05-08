using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarEdgeData.TypeConverters
{
    public abstract class DescribedNumberTypeConverter : TypeConverter
    {

        public abstract string NumberSuffix { get; }

       


        public override bool CanConvertFrom(ITypeDescriptorContext context,
                                        Type sourceType)
        {
            //if (sourceType == typeof(string))
            //{
            //    return true;
            //}
            return base.CanConvertFrom(context, sourceType);
        }


        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            //if (value is string)
            //{
            //    string s = (string)value;
            //    return Int32.Parse(s, NumberStyles.AllowThousands, culture);
            //}

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
                return $"{value} {NumberSuffix}";

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
