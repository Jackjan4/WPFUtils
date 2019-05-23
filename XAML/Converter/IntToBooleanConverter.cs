using System;
using System.Globalization;
using System.Windows.Data;

namespace De.JanRoslan.WPFUtils.XAML.Converter {
    [ValueConversion(typeof(int), typeof(bool))]
    public class IntToBooleanConverter : IValueConverter {
        /// <inheritdoc />
        /// <summary>
        /// Integer to boolean
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is int i) {
                // 1 is true, everything else is false
                return i == 1;
            }
            return ConvertBack(value, null, null, null);
        }


        /// <summary>
        /// Boolean to int
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>null</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is bool b) {
                return b ? 1 : 0;
            }
            return Convert(value, null, null, null);
        }
    }
}