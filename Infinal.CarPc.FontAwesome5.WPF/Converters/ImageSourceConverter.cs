using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace Infinal.CarPc.FontAwesome5.WPF.Converters
{
    /// <summary>
    /// Converts a FontAwesomIcon to an ImageSource. Use the ConverterParameter to pass the Brush.
    /// </summary>
    public class ImageSourceConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is EFontAwesomeIcon))
            {
                return null;
            }

            Brush foregroundBrush = parameter as Brush ?? Brushes.Black;
            return ImageAwesome.CreateImageSource((EFontAwesomeIcon)value, foregroundBrush);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
