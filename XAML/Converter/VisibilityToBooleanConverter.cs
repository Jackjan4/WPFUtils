using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace De.JanRoslan.WPFUtils.XAML.Converter {

    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class VisibilityToBooleanConverter : IValueConverter {



        /// <summary>
        /// Converts a Boolean to a Visibility representation. "true" implies Visibility.Visible and "false" Visibility.Hidden
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            switch (value) {
                case null:
                    return Visibility.Hidden;
                case bool b:
                    return b ? Visibility.Visible : Visibility.Hidden;
                case Visibility v:
                    return ConvertBack(v, typeof(bool), null, null);
                default:
                    throw new ConversionException("Conversion could not be executed since the given value is of wrong type.");
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            switch (value) {
                case null:
                    return false;
                case Visibility v:
                    return v == Visibility.Visible ? true : false;
                case bool b:
                    return Convert(b, typeof(Visibility), null, null);
                default:
                    throw new ConversionException("Conversion could not be executed since the given value is of wrong type.");
            }
        }
    }
}
