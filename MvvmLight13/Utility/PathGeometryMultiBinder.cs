namespace MvvmLight13.Utility
{
    #region Using Declarations

    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Interactivity;
    using System.Windows.Media;

    #endregion

    public class PathGeometryMultiBinder : Behavior<PathGeometry>
    {
        /// <summary>
        /// Dependency property to set the orientation of the axis
        /// </summary>

        public MultiBinding multiBinding = null;

        public static readonly DependencyProperty FiguresProperty = DependencyProperty.RegisterAttached("BoundFigures", typeof(MultiBinding), typeof(PathGeometryMultiBinder), new FrameworkPropertyMetadata(OnBoundFiguresChanged));

        private static void OnBoundFiguresChanged(DependencyObject _d, DependencyPropertyChangedEventArgs _e)
        {
        }

        public PathFigureCollection BoundFigures
        {
            get { return (PathFigureCollection)base.GetValue(FiguresProperty); }
            set { base.SetValue(FiguresProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            PathFigureCollection x = new PathFigureCollection();
            if (AssociatedObject == null)
                return;
        }

        public PathGeometryMultiBinder()
        {
            
        }

    }
}
