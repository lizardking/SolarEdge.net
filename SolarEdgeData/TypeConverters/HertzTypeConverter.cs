﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarEdgeData.TypeConverters
{
    public class HertzTypeConverter : DescribedNumberTypeConverter
    {
        public override string NumberSuffix
        {
            get
            {
               return "Hertz";
            }
        }
    }
}
