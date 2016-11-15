namespace MVVMNodeEditor.Controls
{
    #region Using Declarations

    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using Interfaces;
    using MahApps.Metro.Controls;
    using ViewModel;

    #endregion

    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:MVVMNodeEditor.Controls"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:MVVMNodeEditor.Controls;assembly=MVVMNodeEditor.Controls"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:NetworkControl/>
    ///
    /// </summary>
    public class NetworkControl : ListBox
    {
        #region Dependency Properties
        public static readonly DependencyProperty TranslationProperty;
        protected static readonly DependencyPropertyKey TranslationPropertyKey = DependencyProperty.RegisterReadOnly("Translation", typeof(Vector), typeof(NetworkControl), new UIPropertyMetadata(new Vector()));

        public static readonly DependencyProperty XProperty = DependencyProperty.RegisterAttached("X", typeof(double), typeof(NetworkControl), new FrameworkPropertyMetadata(double.NaN,FrameworkPropertyMetadataOptions.AffectsMeasure |
                                                                        FrameworkPropertyMetadataOptions.AffectsArrange |
                                                                        FrameworkPropertyMetadataOptions.AffectsRender |
                                                                        FrameworkPropertyMetadataOptions.AffectsParentMeasure |
                                                                        FrameworkPropertyMetadataOptions.AffectsParentArrange |
                                                                        FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                                                                        XPropertyChanged));

        public static readonly DependencyProperty YProperty =DependencyProperty.RegisterAttached("Y", typeof(double), typeof(NetworkControl),new FrameworkPropertyMetadata(double.NaN,FrameworkPropertyMetadataOptions.AffectsMeasure |FrameworkPropertyMetadataOptions.AffectsArrange |FrameworkPropertyMetadataOptions.AffectsRender |FrameworkPropertyMetadataOptions.AffectsParentMeasure |FrameworkPropertyMetadataOptions.AffectsParentArrange |FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,YPropertyChanged));

        public static readonly DependencyProperty OrigoProperty = DependencyProperty.Register("Origo", typeof(Point), typeof(NetworkControl),new FrameworkPropertyMetadata(new Point(),FrameworkPropertyMetadataOptions.AffectsMeasure |FrameworkPropertyMetadataOptions.AffectsArrange |FrameworkPropertyMetadataOptions.AffectsRender |FrameworkPropertyMetadataOptions.AffectsParentMeasure |FrameworkPropertyMetadataOptions.AffectsParentArrange));
        #endregion

        #region Attached Routed Events
        public static readonly RoutedEvent PositionChangedEvent = EventManager.RegisterRoutedEvent("PositionChanged", RoutingStrategy.Bubble, typeof(PositionChangedEventHandler), typeof(NetworkControl));
        #endregion

        #region Members
        /// <summary>The position of the topLeft corner of the most top-left vertex.</summary>
        private Point _topLeft;

        /// <summary>The position of the bottom right corner of the most bottom-right vertex.</summary>
        private Point _bottomRight;
        #endregion

        #region Properties

        public Vector Translation
        {
            get { return (Vector)GetValue(TranslationProperty); }
            protected set { SetValue(TranslationPropertyKey, value); }
        }
        /// <summary>
        /// Gets or sets the virtual origo of the canvas.
        /// </summary>
        public Point Origo
        {
            get { return (Point)GetValue(OrigoProperty); }
            set { SetValue(OrigoProperty, value); }
        }
        #endregion

        #region Attached Properties
        [AttachedPropertyBrowsableForChildren]
        public static double GetX(DependencyObject obj)
        {
            var x = obj.GetValue(DataContextProperty);
            if (x is NodeViewModel)
            {
                return (x as NodeViewModel).X;
            }
            else return double.NaN;
        }

        public static void SetX(DependencyObject obj, double value)
        {
            var x = obj.GetValue(DataContextProperty);
            if (x is NodeViewModel)
            {
                (x as NodeViewModel).X = value;
            }
            else
                obj.SetValue(XProperty, value);
        }

        [AttachedPropertyBrowsableForChildren]
        public static double GetY(DependencyObject obj)
        {
            //return (double)obj.GetValue(YProperty);
            var x = obj.GetValue(DataContextProperty);
            if (x is NodeViewModel)
            {
                return (x as NodeViewModel).Y;
            }
            else return double.NaN;
        }

        public static void SetY(DependencyObject obj, double value)
        {
            var x = obj.GetValue(DataContextProperty);
            if (x is NodeViewModel)
            {
                (x as NodeViewModel).Y = value;
            }
            else
                obj.SetValue(YProperty, value);
        }

        #endregion



        #region Constructors
        static NetworkControl()
        {
            TranslationProperty = TranslationPropertyKey.DependencyProperty;
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NetworkControl), new FrameworkPropertyMetadata(typeof(NetworkControl)));
        }

        #endregion

        #region Event Handlers
        private static void XPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var xChange = (double)e.NewValue - (double)e.OldValue;
            PositionChanged(d, xChange, 0);
        }
        private static void YPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var yChange = (double)e.NewValue - (double)e.OldValue;
            PositionChanged(d, 0, yChange);
        }
        #endregion


        #region Methods
        /// <summary>Arranges the size of the control.</summary>
        /// <param name="arrangeSize">The arranged size of the control.</param>
        /// <returns>The size of the control.</returns>
        protected override Size ArrangeOverride(Size arrangeSize)
        {
            Vector originalSize;
            var translate = new Vector(-_topLeft.X, -_topLeft.Y);
            Vector graphSize = originalSize = (_bottomRight - _topLeft);

            if (double.IsNaN(graphSize.X) || double.IsNaN(graphSize.Y) || double.IsInfinity(graphSize.X) || double.IsInfinity(graphSize.Y))
                translate = new Vector(0, 0);

            Translation = translate;
            //Debug.WriteLine(string.Format("Translate={0}", translate.ToString()));
            Canvas canvas = GetVisualChild(0) as Canvas;
            graphSize = canvas.Children.Count > 0
                            ? new Vector(double.NegativeInfinity, double.NegativeInfinity)
                            : new Vector(0, 0);


            //translate with the topLeft
            foreach (UIElement child in canvas.Children)
            {
                double x = GetX(child);
                double y = GetY(child);
                if (double.IsNaN(x) || double.IsNaN(y))
                {
                    //not a vertex, set the coordinates of the top-left corner
                    x = double.IsNaN(x) ? translate.X : x;
                    y = double.IsNaN(y) ? translate.Y : y;
                }
                else
                {
                    //this is a vertex
                    x += translate.X;
                    y += translate.Y;

                    //get the top-left corner
                    x -= child.DesiredSize.Width/2;
                    y -= child.DesiredSize.Height/2;
                }
                child.Arrange(new Rect(new Point(x, y), child.DesiredSize));

                graphSize.X = Math.Max(0, Math.Max(graphSize.X, x + child.DesiredSize.Width));
                graphSize.Y = Math.Max(0, Math.Max(graphSize.Y, y + child.DesiredSize.Height));
            }

            Size newSize = new Size(graphSize.X, graphSize.Y);
            return newSize;
        }

        /// <summary>Overridden measure. It calculates a size where all of of the vertices are visible.</summary>
        /// <param name="constraint">The size constraint.</param>
        /// <returns>The calculated size.</returns>
        protected override Size MeasureOverride(Size constraint)
        {
            _topLeft = new Point(double.PositiveInfinity, double.PositiveInfinity);
            _bottomRight = new Point(double.NegativeInfinity, double.NegativeInfinity);
            Canvas canvas = GetVisualChild(0) as Canvas;

            foreach (UIElement child in canvas.Children)
            {
                //measure the child
                child.Measure(constraint);

                //get the position of the vertex
                double left = GetX(child);
                double top = GetY(child);

                double halfWidth = child.DesiredSize.Width * 0.5;
                double halfHeight = child.DesiredSize.Height * 0.5;

                if (double.IsNaN(left) || double.IsNaN(top))
                {
                    left = halfWidth;
                    top = halfHeight;
                }

                //get the top left corner point
                _topLeft.X = Math.Min(_topLeft.X, left - halfWidth - Origo.X);
                _topLeft.Y = Math.Min(_topLeft.Y, top - halfHeight - Origo.Y);

                //calculate the bottom right corner point
                _bottomRight.X = Math.Max(_bottomRight.X, left + halfWidth - Origo.X);
                _bottomRight.Y = Math.Max(_bottomRight.Y, top + halfHeight - Origo.Y);
            }

            var graphSize = (Size)(_bottomRight - _topLeft);
            graphSize.Width = Math.Max(0, graphSize.Width);
            graphSize.Height = Math.Max(0, graphSize.Height);

            if (double.IsNaN(graphSize.Width) || double.IsNaN(graphSize.Height) || double.IsInfinity(graphSize.Width) || double.IsInfinity(graphSize.Height))
                return new Size(0, 0);

            return graphSize;
        }

       
        private static void PositionChanged(DependencyObject d, double xChange, double yChange)
        {
            var e = d as UIElement;
            if (e != null)
                e.RaiseEvent(new PositionChangedEventArgs(PositionChangedEvent, e, xChange, yChange));
        }
        #endregion
    }
}
