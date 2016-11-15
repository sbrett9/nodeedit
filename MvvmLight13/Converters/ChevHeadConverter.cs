namespace MvvmLight13.Converters
{

    #region Using Declarations

    using System;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Media;

    #endregion

    public class ChevHeadConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double def = 132.5;
            object x = values[0];
            double chevAngle = def;
            if (x is double)
                chevAngle = (double)values[0];
            double width = values[1] is double ? (double)values[1] : 0;
            double height = values[2] is double ? (double)values[2] : 0;

            double angleFromCenter = (180 - chevAngle) / 2;
            double thirdAngle = 180 - 90 - angleFromCenter;
            double halfHeight = height / 2.0;

            double A = (Math.PI * thirdAngle) / 180;
            double B = (Math.PI * 90) / 180;
            double C = (Math.PI * angleFromCenter) / 180;
            double a = halfHeight;
            double b = (a * Math.Sin(B)) / Math.Sin(A);
            double c = (a * (Math.Sin(C))) / Math.Sin(A);

            var z = new PathFigureCollection();
            var fig = new PathFigure();
            fig.IsClosed = true;

            fig.StartPoint = new Point(0, 0);
            fig.Segments.Add(new LineSegment(new Point(c, 0), false));
            fig.Segments.Add(new LineSegment(new Point(c, height), false));
            fig.Segments.Add(new LineSegment(new Point(0, height), false));
            fig.Segments.Add(new LineSegment(new Point(c, halfHeight), false));
            z.Add(fig);


            ////Lower Right Triangle
            //fig = new PathFigure();
            //fig.IsClosed = true;
            //fig.StartPoint = new Point(width - c, height);
            //fig.Segments.Add(new LineSegment(new Point(width, height), false));
            //fig.Segments.Add(new LineSegment(new Point(width, halfHeight), false));
            //fig.Segments.Add(new LineSegment(new Point(width - c, height), false));
            //z.Add(fig);
            return z;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

}
