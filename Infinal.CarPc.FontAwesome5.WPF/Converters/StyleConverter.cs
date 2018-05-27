using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using Infinal.CarPc.FontAwesome5.WPF.Extensions;

namespace Infinal.CarPc.FontAwesome5.WPF.Converters
{
    public class StyleConverter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            FontAwesomeInformationAttribute informationAttribute = (value as EFontAwesomeIcon?)?.GetInformationAttribute<FontAwesomeInformationAttribute>();
            return informationAttribute?.Style ?? EFontAwesomeStyle.None;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
