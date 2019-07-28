using System;
using System.Globalization;
using System.Windows.Data;

namespace De.JanRoslan.WPFUtils.XAML.Converter.MultiValueConverter {


    /// <summary>
    /// 
    /// </summary>
    public class ObjectConverter : IMultiValueConverter {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            object obj = values.Clone();
            return obj;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return (object[])value;
        }
    }
}
