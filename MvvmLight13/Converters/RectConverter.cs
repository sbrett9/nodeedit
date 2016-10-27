namespace MvvmLight13.Converters
{
    #region Using Declarations

    using System;
    using System.Windows.Data;
    using System.Windows.Shapes;

    #endregion

    public class RectConvertor : IMultiValueConverter
    {
        #region IMultiValueConverter Members

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new System.Windows.Rect(0, 0, values[0] is double ? (double)values[0] : 0.0, values[1] is double ? (double)values[1] : 0.0);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
