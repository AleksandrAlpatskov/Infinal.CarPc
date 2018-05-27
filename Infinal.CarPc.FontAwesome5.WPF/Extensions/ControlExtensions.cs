using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Infinal.CarPc.FontAwesome5.WPF.Extensions
{
    /// <summary>Control extensions</summary>
    public static class ControlExtensions
    {
        /// <summary>The key used for storing the spinner Storyboard.</summary>
        private static readonly string SpinnerStoryBoardName = string.Format("{0}Spinner", typeof(FontAwesome).Name);

        /// <summary>Start the spinning animation</summary>
        /// <typeparam name="T">FrameworkElement and ISpinable</typeparam>
        /// <param name="control">Control to apply the rotation </param>
        public static void BeginSpin<T>(this T control) where T : FrameworkElement, ISpinable
        {
            TransformGroup transformGroup = control.RenderTransform as TransformGroup ?? new TransformGroup();
            RotateTransform rotateTransform = transformGroup.Children.OfType<RotateTransform>().FirstOrDefault();
            if (rotateTransform != null)
            {
                rotateTransform.Angle = 0.0;
            }
            else
            {
                transformGroup.Children.Add(new RotateTransform(0.0));
                control.RenderTransform = transformGroup;
                control.RenderTransformOrigin = new Point(0.5, 0.5);
            }
            Storyboard storyboard = new Storyboard();
            DoubleAnimation doubleAnimation1 = new DoubleAnimation
            {
                From = 0.0,
                To = 360.0,
                AutoReverse = false,
                RepeatBehavior = RepeatBehavior.Forever,
                Duration = new Duration(TimeSpan.FromSeconds(control.SpinDuration))
            };
            DoubleAnimation doubleAnimation2 = doubleAnimation1;
            storyboard.Children.Add(doubleAnimation2);
            Storyboard.SetTarget(doubleAnimation2, control);
            Storyboard.SetTargetProperty(doubleAnimation2, new PropertyPath("(0).(1)[0].(2)", UIElement.RenderTransformProperty, TransformGroup.ChildrenProperty, RotateTransform.AngleProperty));
            storyboard.Begin();
            control.Resources.Add(SpinnerStoryBoardName, storyboard);
        }

        /// <summary>Stop the spinning animation</summary>
        /// <typeparam name="T">FrameworkElement and ISpinable</typeparam>
        /// <param name="control">Control to stop the rotation.</param>
        public static void StopSpin<T>(this T control) where T : FrameworkElement, ISpinable
        {
            if (!(control.Resources[SpinnerStoryBoardName] is Storyboard resource))
            {
                return;
            }

            resource.Stop();
            control.Resources.Remove(SpinnerStoryBoardName);
        }

        /// <summary>Sets the rotation for the control</summary>
        /// <typeparam name="T">FrameworkElement and IRotatable</typeparam>
        /// <param name="control">Control to apply the rotation</param>
        public static void SetRotation<T>(this T control) where T : FrameworkElement, IRotatable
        {
            TransformGroup transformGroup = control.RenderTransform as TransformGroup ?? new TransformGroup();
            RotateTransform rotateTransform = transformGroup.Children.OfType<RotateTransform>().FirstOrDefault();
            if (rotateTransform != null)
            {
                rotateTransform.Angle = control.Rotation;
            }
            else
            {
                transformGroup.Children.Add(new RotateTransform(control.Rotation));
                control.RenderTransform = transformGroup;
                control.RenderTransformOrigin = new Point(0.5, 0.5);
            }
        }

        /// <summary>Sets the flip orientation for the control</summary>
        /// <typeparam name="T">FrameworkElement and IRotatable</typeparam>
        /// <param name="control">Control to apply the rotation</param>
        public static void SetFlipOrientation<T>(this T control) where T : FrameworkElement, IFlippable
        {
            TransformGroup transformGroup = control.RenderTransform as TransformGroup ?? new TransformGroup();
            int num1 = control.FlipOrientation == EFlipOrientation.Normal || control.FlipOrientation == EFlipOrientation.Vertical ? 1 : -1;
            int num2 = control.FlipOrientation == EFlipOrientation.Normal || control.FlipOrientation == EFlipOrientation.Horizontal ? 1 : -1;
            ScaleTransform scaleTransform = transformGroup.Children.OfType<ScaleTransform>().FirstOrDefault();
            if (scaleTransform != null)
            {
                scaleTransform.ScaleX = num1;
                scaleTransform.ScaleY = num2;
            }
            else
            {
                transformGroup.Children.Add(new ScaleTransform(num1, num2));
                control.RenderTransform = transformGroup;
                control.RenderTransformOrigin = new Point(0.5, 0.5);
            }
        }
    }
}
