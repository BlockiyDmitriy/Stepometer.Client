using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace Stepometer.Converter
{
    public class Base64ToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string base64)
            {
                return ImageSource.FromStream(() => new MemoryStream(System.Convert.FromBase64String(base64)));
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
