﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace De.JanRoslan.WPFUtils.XAML.Converter {
    public class ConversionException : Exception {

        public ConversionException(string msg) : base(msg){
        }
    }
}
