using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace BSK_Encryption.Converters
{
    public class BlockConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int result;
            var item = value as ComboBoxItem;
            Int32.TryParse(item.Content?.ToString(), out result);
            return result;
        }
    }
}
