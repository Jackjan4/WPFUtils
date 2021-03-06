﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace De.JanRoslan.WPFUtils.XAML.Converter {



    [ValueConversion(typeof(object), typeof(bool))]
    public class ObjectExistenceToBooleanConverter : IValueConverter {


        /// <inheritdoc />
        /// <summary>
        /// Object existence to boolean
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null;
        }


        /// <summary>
        /// Boolean to object existence, (which actually can't be done.)
        /// This is no-op!
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>null</returns>

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
}
