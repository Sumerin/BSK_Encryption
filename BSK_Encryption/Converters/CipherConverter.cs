using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Windows.Data;

namespace BSK_Encryption.Converters
{
    public class CipherConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((int)value)-1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (CipherMode) value+1;
        }
    }
}
