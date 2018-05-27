using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Infinal.CarPc.FontAwesome5.WPF.Extensions;

namespace Infinal.CarPc.FontAwesome5.WPF
{
    /// <summary>
    /// Provides a lightweight control for displaying a FontAwesome icon as text.
    /// </summary>
    public class FontAwesome : TextBlock, ISpinable, IRotatable, IFlippable
    {
        /// <summary>
        /// Identifies the FontAwesome.WPF.FontAwesome.Icon dependency property.
        /// </summary>
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(nameof(Icon), typeof(EFontAwesomeIcon), typeof(FontAwesome), new PropertyMetadata(EFontAwesomeIcon.None, OnIconPropertyChanged));
        /// <summary>
        /// Identifies the FontAwesome.WPF.FontAwesome.Spin dependency property.
        /// </summary>
        public static readonly DependencyProperty SpinProperty = DependencyProperty.Register(nameof(Spin), typeof(bool), typeof(FontAwesome), new PropertyMetadata(false, OnSpinPropertyChanged, SpinCoerceValue));
        /// <summary>
        /// Identifies the FontAwesome.WPF.FontAwesome.SpinDuration dependency property.
        /// </summary>
        public static readonly DependencyProperty SpinDurationProperty = DependencyProperty.Register(nameof(SpinDuration), typeof(double), typeof(FontAwesome), new PropertyMetadata(1.0, SpinDurationChanged, SpinDurationCoerceValue));
        /// <summary>
        /// Identifies the FontAwesome.WPF.FontAwesome.Rotation dependency property.
        /// </summary>
        public static readonly DependencyProperty RotationProperty = DependencyProperty.Register(nameof(Rotation), typeof(double), typeof(FontAwesome), new PropertyMetadata(0.0, RotationChanged, RotationCoerceValue));
        /// <summary>
        /// Identifies the FontAwesome.WPF.FontAwesome.FlipOrientation dependency property.
        /// </summary>
        public static readonly DependencyProperty FlipOrientationProperty = DependencyProperty.Register(nameof(FlipOrientation), typeof(EFlipOrientation), typeof(FontAwesome), new PropertyMetadata(EFlipOrientation.Normal, FlipOrientationChanged));

        static FontAwesome()
        {
            OpacityProperty.OverrideMetadata(typeof(FontAwesome), (PropertyMetadata)new UIPropertyMetadata(defaultValue: (object)1.0, new PropertyChangedCallback(OpacityChanged)));
        }

        public FontAwesome()
        {
            IsVisibleChanged += (DependencyPropertyChangedEventHandler)((s, a) => CoerceValue(SpinProperty));
        }

        private static void OpacityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.CoerceValue(SpinProperty);
        }

        /// <summary>
        /// Gets or sets the FontAwesome icon. Changing this property will cause the icon to be redrawn.
        /// </summary>
        public EFontAwesomeIcon Icon
        {
            get => (EFontAwesomeIcon)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        /// <summary>
        /// Gets or sets the current spin (angle) animation of the icon.
        /// </summary>
        public bool Spin
        {
            get => (bool)GetValue(SpinProperty);
            set => SetValue(SpinProperty, value);
        }

        private static void OnIconPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(TextOptions.TextRenderingModeProperty, TextRenderingMode.ClearType);
            d.SetValue(FontFamilyProperty, ((EFontAwesomeIcon)e.NewValue).GetFontFamily());
            d.SetValue(TextAlignmentProperty, TextAlignment.Center);
            d.SetValue(TextProperty, ((EFontAwesomeIcon)e.NewValue).GetUnicode());
        }

        private static void OnSpinPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is FontAwesome control))
            {
                return;
            }

            if ((bool)e.NewValue)
            {
                control.BeginSpin();
            }
            else
            {
                control.StopSpin();
                control.SetRotation();
            }
        }

        private static object SpinCoerceValue(DependencyObject d, object basevalue)
        {
            FontAwesome fontAwesome = (FontAwesome)d;
            if (!fontAwesome.IsVisible || fontAwesome.Opacity == 0.0 || fontAwesome.SpinDuration == 0.0)
            {
                return false;
            }

            return basevalue;
        }

        /// <summary>
        /// Gets or sets the duration of the spinning animation (in seconds). This will stop and start the spin animation.
        /// </summary>
        public double SpinDuration
        {
            get => (double)GetValue(SpinDurationProperty);
            set => SetValue(SpinDurationProperty, value);
        }

        private static void SpinDurationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is FontAwesome control) || !control.Spin || (!(e.NewValue is double) || e.NewValue.Equals(e.OldValue)))
            {
                return;
            }

            control.StopSpin();
            control.BeginSpin();
        }

        private static object SpinDurationCoerceValue(DependencyObject d, object value)
        {
            if ((double)value >= 0.0)
            {
                return value;
            }

            return 0.0;
        }

        /// <summary>Gets or sets the current rotation (angle).</summary>
        public double Rotation
        {
            get => (double)GetValue(RotationProperty);
            set => SetValue(RotationProperty, value);
        }

        private static void RotationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is FontAwesome control) || control.Spin || (!(e.NewValue is double) || e.NewValue.Equals(e.OldValue)))
            {
                return;
            }

            control.SetRotation();
        }

        private static object RotationCoerceValue(DependencyObject d, object value)
        {
            double num = (double)value;
            if (num < 0.0)
            {
                return 0.0;
            }

            if (num <= 360.0)
            {
                return value;
            }

            return 360.0;
        }

        /// <summary>
        /// Gets or sets the current orientation (horizontal, vertical).
        /// </summary>
        public EFlipOrientation FlipOrientation
        {
            get => (EFlipOrientation)GetValue(FlipOrientationProperty);
            set => SetValue(FlipOrientationProperty, value);
        }

        private static void FlipOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is FontAwesome control) || !(e.NewValue is EFlipOrientation) || e.NewValue.Equals(e.OldValue))
            {
                return;
            }

            control.SetFlipOrientation();
        }
    }
}
