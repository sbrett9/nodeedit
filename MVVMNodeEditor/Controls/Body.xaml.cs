namespace MVVMNodeEditor.Controls
{
    #region Using Declarations

    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Media;

    #endregion

    public partial class Body : ContentControl
    {

        public static readonly DependencyProperty FillProperty = DependencyProperty.Register("Fill", typeof(Brush), typeof(Body), new FrameworkPropertyMetadata(System.Windows.Media.Brushes.LightSkyBlue, FrameworkPropertyMetadataOptions.None));
        public static readonly DependencyProperty ChevAngleProperty = DependencyProperty.Register("ChevAngle", typeof(double), typeof(Body), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.None));
        public static readonly DependencyProperty BindableActualHeightProperty = DependencyProperty.Register("BindableActualHeight", typeof(double), typeof(Body), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.None));
        public static readonly DependencyProperty BindableActualWidthProperty = DependencyProperty.Register("BindableActualWidth", typeof(double), typeof(Body), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.None));
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

        public double BindableActualHeight
        {
            get { return (double)GetValue(BindableActualHeightProperty); }
            set
            {
                SetValue(BindableActualHeightProperty, value); 
                
            }
        }

        public double BindableActualWidth
        {
            get { return (double)GetValue(BindableActualWidthProperty); }
            set
            {
                SetValue(BindableActualWidthProperty, value); 
              }
        }
        public Body()
        {
            InitializeComponent();
        }

        private void Body_OnLayoutUpdated(object _sender, EventArgs _e)
        {
            if(Math.Abs(BindableActualHeight - ActualHeight) > 0.01)
                BindableActualHeight = ActualHeight;
            if(Math.Abs(BindableActualWidth - ActualWidth) > 0.01)
                BindableActualWidth = ActualWidth;
        }


        protected override Size MeasureOverride(Size _size)
        {
            var t = base.MeasureOverride(_size);
            return t;
        }
    }
}
