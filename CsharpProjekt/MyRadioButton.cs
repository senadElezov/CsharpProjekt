using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace CsharpProjekt
{
    class MyRadioButton:RadioButton
    {
        private Brush checkBack;
        public Brush CheckedBackground
        {
            set
            {
                checkBack = value;
            }
            get
            {
                return checkBack;
            }
        }
   
        public static readonly DependencyProperty HoveredBackgroundProperty =
          DependencyProperty.Register("HoveredBackground", typeof(Brush), typeof(MyRadioButton),
              new PropertyMetadata(new SolidColorBrush(Colors.White), new PropertyChangedCallback(ChangeBackGround)));
        public Brush HoveredBackground
        {
            get => (Brush)GetValue(HoveredBackgroundProperty);
            set => SetValue(HoveredBackgroundProperty, value);
        }
        private static void ChangeBackGround(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue as Brush != null)
                (d as MyRadioButton).HoveredBackground = e.NewValue as Brush;
        }
        public static readonly DependencyProperty CornerRadiusProperty =
         DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(MyRadioButton),
             new PropertyMetadata(new CornerRadius(0), new PropertyChangedCallback(ChangeCornerRadius)));
        public CornerRadius CornerRadius
        {
            get => (CornerRadius)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }
        private static void ChangeCornerRadius(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is CornerRadius)
                (d as MyRadioButton).CornerRadius =(CornerRadius)e.NewValue ;
        }

        public static readonly DependencyProperty OpacityBorderProperty =
       DependencyProperty.Register("OpacityBorder", typeof(Double), typeof(MyRadioButton),
           new PropertyMetadata(0.0, new PropertyChangedCallback(ChangeOpacityBorder)));
        public Double OpacityBorder
        {
            get => (Double)GetValue(OpacityBorderProperty);
            set => SetValue(OpacityBorderProperty, value);
        }
        private static void ChangeOpacityBorder(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is Double)
                (d as MyRadioButton).OpacityBorder = (Double)e.NewValue;
        }
    }
}
