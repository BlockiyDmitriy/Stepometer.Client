using System;
using System.Globalization;
using Xamarin.Forms;

namespace Stepometer.Converter
{
    public class EmbeddedResourceToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            value is string resourcePath && !string.IsNullOrWhiteSpace(resourcePath)
                ? ImageSource.FromResource(resourcePath)
                : value;
        

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}