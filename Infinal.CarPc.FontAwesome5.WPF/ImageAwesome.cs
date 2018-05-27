using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Infinal.CarPc.FontAwesome5.WPF.Extensions;

namespace Infinal.CarPc.FontAwesome5.WPF
{
    /// <summary>
    /// Represents a control that draws an FontAwesome icon as an image.
    /// </summary>
    public class ImageAwesome : Image, ISpinable, IRotatable, IFlippable
    {
        /// <summary>
        /// Identifies the FontAwesome.WPF.ImageAwesome.Foreground dependency property.
        /// </summary>
        public static readonly DependencyProperty ForegroundProperty = DependencyProperty.Register(nameof(Foreground), typeof(Brush), typeof(ImageAwesome), new PropertyMetadata(Brushes.Black, OnIconPropertyChanged));
        /// <summary>
        /// Identifies the FontAwesome.WPF.ImageAwesome.Icon dependency property.
        /// </summary>
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(nameof(Icon), typeof(EFontAwesomeIcon), typeof(ImageAwesome), new PropertyMetadata(EFontAwesomeIcon.None, OnIconPropertyChanged));
        /// <summary>
        /// Identifies the FontAwesome.WPF.ImageAwesome.Spin dependency property.
        /// </summary>
        public static readonly DependencyProperty SpinProperty = DependencyProperty.Register(nameof(Spin), typeof(bool), typeof(ImageAwesome), new PropertyMetadata(false, OnSpinPropertyChanged, SpinCoerceValue));
        /// <summary>
        /// Identifies the FontAwesome.WPF.ImageAwesome.Spin dependency property.
        /// </summary>
        public static readonly DependencyProperty SpinDurationProperty = DependencyProperty.Register(nameof(SpinDuration), typeof(double), typeof(ImageAwesome), new PropertyMetadata(1.0, SpinDurationChanged, SpinDurationCoerceValue));
        /// <summary>
        /// Identifies the FontAwesome.WPF.ImageAwesome.Rotation dependency property.
        /// </summary>
        public static readonly DependencyProperty RotationProperty = DependencyProperty.Register(nameof(Rotation), typeof(double), typeof(ImageAwesome), new PropertyMetadata(0.0, RotationChanged, RotationCoerceValue));
        /// <summary>
        /// Identifies the FontAwesome.WPF.ImageAwesome.FlipOrientation dependency property.
        /// </summary>
        public static readonly DependencyProperty FlipOrientationProperty = DependencyProperty.Register(nameof(FlipOrientation), typeof(EFlipOrientation), typeof(ImageAwesome), new PropertyMetadata(EFlipOrientation.Normal, FlipOrientationChanged));

        static ImageAwesome()
        {
            OpacityProperty.OverrideMetadata(typeof(ImageAwesome), (PropertyMetadata)new UIPropertyMetadata((object)1.0, new PropertyChangedCallback(OpacityChanged)));
        }

        public ImageAwesome()
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
            if (!(d is ImageAwesome control))
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
            ImageAwesome imageAwesome = (ImageAwesome)d;
            if (!imageAwesome.IsVisible || imageAwesome.Opacity == 0.0 || imageAwesome.SpinDuration == 0.0)
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
            if (!(d is ImageAwesome control) || !control.Spin || (!(e.NewValue is double) || e.NewValue.Equals(e.OldValue)))
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
            if (!(d is ImageAwesome control) || control.Spin || (!(e.NewValue is double) || e.NewValue.Equals(e.OldValue)))
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
            if (!(d is ImageAwesome control) || !(e.NewValue is EFlipOrientation) || e.NewValue.Equals(e.OldValue))
            {
                return;
            }

            control.SetFlipOrientation();
        }

        private static void OnIconPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is ImageAwesome imageAwesome))
            {
                return;
            }

            imageAwesome.SetValue(SourceProperty, CreateImageSource(imageAwesome.Icon, imageAwesome.Foreground));
        }

        /// <summary>
        /// Creates a new System.Windows.Media.ImageSource of a specified FontAwesomeIcon and foreground System.Windows.Media.Brush.
        /// </summary>
        /// <param name="icon">The FontAwesome icon to be drawn.</param>
        /// <param name="foregroundBrush">The System.Windows.Media.Brush to be used as the foreground.</param>
        /// <param name="emSize">The font size in em.</param>
        /// <returns>A new System.Windows.Media.ImageSource</returns>
        public static ImageSource CreateImageSource(EFontAwesomeIcon icon, Brush foregroundBrush, double emSize = 100.0)
        {
            DrawingVisual drawingVisual = new DrawingVisual();
#pragma warning disable CS0618 // Typ oder Element ist veraltet
            FormattedText formattedText = new FormattedText(icon.GetUnicode(), CultureInfo.InvariantCulture,
            FlowDirection.LeftToRight, icon.GetTypeFace(), emSize, foregroundBrush)
            {
                TextAlignment = TextAlignment.Center
            };
#pragma warning restore CS0618 // Typ oder Element ist veraltet
            using (DrawingContext drawingContext1 = drawingVisual.RenderOpen())
            {
                DrawingContext drawingContext2 = drawingContext1;
                Point origin = new Point(0.0, 0.0);
                drawingContext2.DrawText(formattedText, origin);
            }
            return new DrawingImage(drawingVisual.Drawing);
        }
    }
}
