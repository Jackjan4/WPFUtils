using System;
using System.Globalization;
using System.Windows.Data;

namespace De.JanRoslan.WPFUtils.XAML.Converter {



    /// <summary>
    /// Converts a Boolean to its inverse representation
    /// </summary>
    [ValueConversion(typeof(bool), (typeof(bool)))]
    public class InverseBooleanConverter : IValueConverter {



        /// <summary>
        /// Convert Boolean to inverse boolean
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

            throw new ConversionException("Conversion could not be executed since the given value is of wrong type.");
        }



        /// <summary>
        /// Convert Boolean to inverse boolean
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

            throw new ConversionException("Conversion could not be executed since the given value is of wrong type.");
        }
    }
}
