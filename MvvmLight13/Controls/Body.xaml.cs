namespace MvvmLight13.Controls
{
    #region Using Declarations

    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    #endregion

    public partial class Body : UserControl
    {

        public static readonly DependencyProperty FillProperty = DependencyProperty.Register("Fill", typeof(Brush), typeof(Body), new FrameworkPropertyMetadata(System.Windows.Media.Brushes.LightSkyBlue, FrameworkPropertyMetadataOptions.None));
        public static readonly DependencyProperty ChevAngleProperty = DependencyProperty.Register("ChevAngle", typeof(double), typeof(Body), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));
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

        public Body()
        {
            InitializeComponent();
        }
    }
}
