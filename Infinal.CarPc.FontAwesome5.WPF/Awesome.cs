using System.Windows;
using System.Windows.Controls;
using Infinal.CarPc.FontAwesome5.WPF.Extensions;

namespace Infinal.CarPc.FontAwesome5.WPF
{
    /// <summary>
    /// Provides attached properties to set FontAwesome icons on controls.
    /// </summary>
    public static class Awesome
    {
        /// <summary>
        /// Identifies the FontAwesome.WPF.Awesome.Content attached dependency property.
        /// </summary>
        public static readonly DependencyProperty ContentProperty = DependencyProperty.RegisterAttached("Content", typeof(EFontAwesomeIcon), typeof(Awesome), new PropertyMetadata(EFontAwesomeIcon.None, ContentChanged));
        private const EFontAwesomeIcon DEFAULT_CONTENT = EFontAwesomeIcon.None;

        /// <summary>
        /// Gets the content of a ContentControl, expressed as a FontAwesome icon.
        /// </summary>
        /// <param name="target">The ContentControl subject of the query</param>
        /// <returns>FontAwesome icon found as content</returns>
        public static EFontAwesomeIcon GetContent(DependencyObject target)
        {
            return (EFontAwesomeIcon)target.GetValue(ContentProperty);
        }

        /// <summary>
        /// Sets the content of a ContentControl expressed as a FontAwesome icon. This will cause the content to be redrawn.
        /// </summary>
        /// <param name="target">The ContentControl where to set the content</param>
        /// <param name="value">FontAwesome icon to set as content</param>
        public static void SetContent(DependencyObject target, EFontAwesomeIcon value)
        {
            target.SetValue(ContentProperty, value);
        }

        private static void ContentChanged(DependencyObject sender, DependencyPropertyChangedEventArgs evt)
        {
            if (!(sender is ContentControl))
            {
                return;
            }

            ContentControl contentControl = (ContentControl)sender;
            if (!(evt.NewValue is EFontAwesomeIcon))
            {
                return;
            }

            EFontAwesomeIcon newValue = (EFontAwesomeIcon)evt.NewValue;
            contentControl.FontFamily = newValue.GetFontFamily();
            contentControl.Content = newValue.GetUnicode();
        }
    }
}
