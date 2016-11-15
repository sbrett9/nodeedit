namespace MvvmLight13.Controls
{
    #region Using Declarations

    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using Utility;

    #endregion

    public partial class Tail : UserControl
    {

        public static readonly DependencyProperty FillProperty = DependencyProperty.Register("Fill", typeof(Brush), typeof(Tail), new FrameworkPropertyMetadata(System.Windows.Media.Brushes.LawnGreen, FrameworkPropertyMetadataOptions.None));
        public static readonly DependencyProperty ChevAngleProperty = DependencyProperty.Register("ChevAngle", typeof(double), typeof(Tail), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty ChevWidthProperty = DependencyProperty.Register("ChevWidth", typeof(double), typeof(Tail), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty ChevHeightProperty = DependencyProperty.Register("ChevHeight", typeof(double), typeof(Tail), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty BindableActualHeightProperty = DependencyProperty.Register("BindableActualHeight", typeof(double), typeof(Tail), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty BindableActualWidthProperty = DependencyProperty.Register("BindableActualWidth", typeof(double), typeof(Tail), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty XProperty = DependencyProperty.Register("X", typeof(double), typeof(Tail), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.None));
        public static readonly DependencyProperty YProperty = DependencyProperty.Register("Y", typeof(double), typeof(Tail), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.None));

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

        public double BindableActualHeight
        {
            get { return (double)GetValue(BindableActualHeightProperty); }
            set { SetValue(BindableActualHeightProperty, value); }
        }

        public double BindableActualWidth
        {
            get { return (double)GetValue(BindableActualWidthProperty); }
            set { SetValue(BindableActualWidthProperty, value); }
        }
        public double X
        {
            get { return (double)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }
        public double Y
        {
            get { return (double)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        public Tail()
        {
            InitializeComponent();
        }

        private void OnLayoutUpdated(object _sender, EventArgs _e)
        {
            BindableActualHeight = ActualHeight;
            BindableActualWidth = ActualWidth;
        }
    }
}
