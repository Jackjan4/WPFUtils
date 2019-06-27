using System;
using System.Globalization;
using System.Windows.Data;

namespace De.JanRoslan.WPFUtils.XAML.Converter {

    [ValueConversion(typeof(bool), (typeof(int)))]
    public class InverseBooleanConverter : IValueConverter {



        /// <summary>
        /// Boolean to inverse boolean
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is bool b) {
                return !b;
            }

            return false;
        }



        /// <summary>
        /// Boolean to inverse boolean
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is bool b) {
                return !b;
            }

            return false;
        }
    }
}
