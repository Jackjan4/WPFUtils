using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace De.JanRoslan.WPFUtils.XAML.Converter {

    [ValueConversion(typeof(bool), typeof(string))]
    public class BooleanToStringConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            switch (value) {
                case null:
                    return "False";
                case bool b:
                    return b.ToString();
                case string s:
                    return ConvertBack(s, typeof(bool), null, null);
                default:
                    throw new ConversionException("Conversion could not be executed since the given value is of wrong type.");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            switch (value) {
                case null:
                    return false;
                case string s:
                    return bool.Parse(s);
                case bool b:
                    return Convert(b, typeof(string), null, null);
                default:
                    throw new ConversionException("Conversion could not be executed since the given value is of wrong type.");
            }
        }
    }
}
