﻿namespace MvvmLight13.Controls
{
    #region Using Declarations

    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    #endregion

    public partial class Tail : UserControl
    {

        public static readonly DependencyProperty FillProperty = DependencyProperty.Register("Fill", typeof(Brush), typeof(Tail), new FrameworkPropertyMetadata(System.Windows.Media.Brushes.LawnGreen, FrameworkPropertyMetadataOptions.None));
        public static readonly DependencyProperty ChevAngleProperty = DependencyProperty.Register("ChevAngle", typeof(double), typeof(Tail), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty ChevWidthProperty = DependencyProperty.Register("ChevWidth", typeof(double), typeof(Tail), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty ChevHeightProperty = DependencyProperty.Register("ChevHeight", typeof(double), typeof(Tail), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        public double ChevAngle
        {
            get { return (double)GetValue(ChevAngleProperty); }
            set { SetValue(ChevAngleProperty, value); }
        }

        public double ChevWidth
        {
            get { return (double)GetValue(ChevWidthProperty); }
            set { SetValue(ChevWidthProperty, value); }
        }
        public double ChevHeight
        {
            get { return (double)GetValue(ChevHeightProperty); }
            set { SetValue(ChevHeightProperty, value); }
        }

        public Tail()
        {
            InitializeComponent();
        }
    }
}