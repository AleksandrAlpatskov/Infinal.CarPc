using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Infinal.CarPc.FontAwesome5.WPF.Extensions;

namespace Infinal.CarPc.FontAwesome5.WPF
{
    public class SvgAwesome : Viewbox, ISpinable, IRotatable, IFlippable
    {
        /// <summary>
        /// Identifies the FontAwesome.WPF.SvgAwesome.Foreground dependency property.
        /// </summary>
        public static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register(nameof(Foreground), typeof(Brush), typeof(SvgAwesome), new PropertyMetadata(Brushes.Black, OnIconPropertyChanged));
        /// <summary>
        /// Identifies the FontAwesome.WPF.SvgAwesome.Icon dependency property.
        /// </summary>
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(nameof(Icon), typeof(EFontAwesomeIcon), typeof(SvgAwesome), new PropertyMetadata(EFontAwesomeIcon.None, OnIconPropertyChanged));
        /// <summary>
        /// Identifies the FontAwesome.WPF.SvgAwesome.Spin dependency property.
        /// </summary>
        public static readonly DependencyProperty SpinProperty = DependencyProperty.Register(nameof(Spin), typeof(bool), typeof(SvgAwesome), new PropertyMetadata(false, OnSpinPropertyChanged, SpinCoerceValue));
        /// <summary>
        /// Identifies the FontAwesome.WPF.SvgAwesome.Spin dependency property.
        /// </summary>
        public static readonly DependencyProperty SpinDurationProperty = DependencyProperty.Register(nameof(SpinDuration), typeof(double), typeof(SvgAwesome), new PropertyMetadata(1.0, SpinDurationChanged, SpinDurationCoerceValue));
        /// <summary>
        /// Identifies the FontAwesome.WPF.SvgAwesome.Rotation dependency property.
        /// </summary>
        public static readonly DependencyProperty RotationProperty = DependencyProperty.Register(nameof(Rotation), typeof(double), typeof(SvgAwesome), new PropertyMetadata(0.0, RotationChanged, RotationCoerceValue));
        /// <summary>
        /// Identifies the FontAwesome.WPF.SvgAwesome.FlipOrientation dependency property.
        /// </summary>
        public static readonly DependencyProperty FlipOrientationProperty = DependencyProperty.Register(nameof(FlipOrientation), typeof(EFlipOrientation), typeof(SvgAwesome), new PropertyMetadata(EFlipOrientation.Normal, FlipOrientationChanged));

        static SvgAwesome()
        {
            OpacityProperty.OverrideMetadata(typeof(SvgAwesome), (PropertyMetadata)new UIPropertyMetadata((object)1.0, new PropertyChangedCallback(OpacityChanged)));
        }

        public SvgAwesome()
        {
            IsVisibleChanged += (DependencyPropertyChangedEventHandler)((s, a) => CoerceValue(SpinProperty));
        }

        private static void OpacityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.CoerceValue(SpinProperty);
        }

        /// <summary>
        /// Gets or sets the foreground brush of the icon. Changing this property will cause the icon to be redrawn.
        /// </summary>
        public Brush Foreground
        {
            get => (Brush)GetValue(ForegroundProperty);
            set => SetValue(ForegroundProperty, value);
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

        private static void OnSpinPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is SvgAwesome control))
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
            SvgAwesome svgAwesome = (SvgAwesome)d;
            if (!svgAwesome.IsVisible || svgAwesome.Opacity == 0.0 || svgAwesome.SpinDuration == 0.0)
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
            if (!(d is SvgAwesome control) || !control.Spin || (!(e.NewValue is double) || e.NewValue.Equals(e.OldValue)))
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
            if (!(d is SvgAwesome control) || control.Spin || (!(e.NewValue is double) || e.NewValue.Equals(e.OldValue)))
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
            if (!(d is SvgAwesome control) || !(e.NewValue is EFlipOrientation) || e.NewValue.Equals(e.OldValue))
            {
                return;
            }

            control.SetFlipOrientation();
        }

        private static void OnIconPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is SvgAwesome svgAwesome))
            {
                return;
            }

            if (svgAwesome.Icon == EFontAwesomeIcon.None)
            {
                svgAwesome.Visibility = Visibility.Collapsed;
            }
            else
            {
                svgAwesome.Visibility = Visibility.Visible;
                svgAwesome.Child = CreatePath(svgAwesome.Icon, svgAwesome.Foreground);
            }
        }

        /// <summary>
        /// Creates a new System.Windows.Media.ImageSource of a specified FontAwesomeIcon and foreground System.Windows.Media.Brush.
        /// </summary>
        /// <param name="icon">The FontAwesome icon to be drawn.</param>
        /// <param name="foregroundBrush">The System.Windows.Media.Brush to be used as the foreground.</param>
        /// <returns>A new System.Windows.Media.ImageSource</returns>
        public static Path CreatePath(EFontAwesomeIcon icon, Brush foregroundBrush)
        {
            Path path1 = null;
            if (icon.GetSvg(out string path2, out int width, out int height))
            {
                path1 = new Path
                {
                    Data = Geometry.Parse(path2),
                    Width = width,
                    Height = height,
                    Fill = foregroundBrush
                };
            }
            return path1;
        }
    }
}
