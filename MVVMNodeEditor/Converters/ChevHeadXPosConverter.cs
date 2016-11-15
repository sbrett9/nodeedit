namespace MVVMNodeEditor.Converters
{
    #region Using Declarations

    using System;
    using System.Windows.Data;

    #endregion

    public class ChevHeadXPosConverter : IMultiValueConverter
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

            return width - c;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
