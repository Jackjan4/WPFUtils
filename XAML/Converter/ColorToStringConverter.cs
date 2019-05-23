using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace De.JanRoslan.WPFUtils.XAML.Converter {

    [ValueConversion(typeof(Color),typeof(string))]
    public class ColorToStringConverter : IValueConverter {



        /// <summary>
        /// Color to string
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            switch (value)
            {
                case string _:
                    return ConvertBack(value, typeof(Color), null, null);
                case null:
                    return "#00FFFFFF";
            }

            return ((Color) value).ToString();
        }


        /// <summary>
        /// String to color
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            switch (value)
            {
                case Color _:
                    return Convert(value, typeof(string), null, null);
                case null:
                    return null;
            }

            return ColorConverter.ConvertFromString((string)value);
        }
    }
}
