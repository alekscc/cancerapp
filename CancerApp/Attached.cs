using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace CancerApp
{
    class Attached
    {
        static DataWindow dw;

        //------------------------- IsBoldMouseOver

        static double oldStrokeThickness;

        public static bool GetIsBoldMouseOver(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsBoldMouseOverProperty);
        }

        public static void SetIsBoldMouseOver(DependencyObject obj, bool value)
        {
            obj.SetValue(IsBoldMouseOverProperty, value);
        }

        public static readonly DependencyProperty IsBoldMouseOverProperty =
            DependencyProperty.RegisterAttached("IsBoldMouseOver", typeof(bool), typeof(Attached), new UIPropertyMetadata(false, IsBoldMouseOverChanged));

        static void IsBoldMouseOverChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var fe = obj as FrameworkElement;
            fe.MouseEnter += new System.Windows.Input.MouseEventHandler(fe_MouseEnter_IsBoldMouseOver);
            fe.MouseLeave += new System.Windows.Input.MouseEventHandler(fe_MouseLeave_IsBoldMouseOver);
        }

        static void fe_MouseEnter_IsBoldMouseOver(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var ele = sender as Path;
            oldStrokeThickness = ele.StrokeThickness;
            ele.StrokeThickness = 8.0D;
        }

        static void fe_MouseLeave_IsBoldMouseOver(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var ele = sender as Path;
            ele.StrokeThickness = oldStrokeThickness;
        }


        //------------------------- MouseOverBrush

        static Brush originalBrush;

        public static Brush GetMouseOverBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(MouseOverBrushProperty);
        }

        public static void SetMouseOverBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(MouseOverBrushProperty, value);
        }

        public static readonly DependencyProperty MouseOverBrushProperty =
            DependencyProperty.RegisterAttached("MouseOverBrush", typeof(Brush), typeof(Attached), new UIPropertyMetadata(null, MouseOverBrushChanged));


        static void MouseOverBrushChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var fe = obj as FrameworkElement;
            fe.MouseEnter += fe_MouseEnter_MouseOverBrush;
            fe.MouseLeave += fe_MouseLeave_MouseOverBrush;
        }

        static void fe_MouseLeave_MouseOverBrush(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var ele = sender as Path;
            ele.Fill = originalBrush;
        }

        static void fe_MouseEnter_MouseOverBrush(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var ele = sender as Path;
            originalBrush = ele.Fill;
            ele.Fill = (Brush)ele.GetValue(Attached.MouseOverBrushProperty);
        }

        public static readonly DependencyProperty MouseClickProperty =
            DependencyProperty.RegisterAttached("MouseClick", typeof(Brush), typeof(Attached), new UIPropertyMetadata(null, MouseClick));

        static void MouseClick(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            dw = new DataWindow();
            dw.Show();
        }

    }
}