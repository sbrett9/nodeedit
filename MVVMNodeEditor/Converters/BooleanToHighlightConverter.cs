namespace MVVMNodeEditor.Converters
{
    #region Using Declarations

    using System;
    using System.Windows;
    using System.Windows.Data;

    #endregion

    public class BooleanToHighlightConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //Element 0 is the boolean
            //element 1 is the brush color when not highlighted
            //element 2 is the brush color when highlighted
            bool z = false;
            try
            {
                z = (bool) values[0];
            }
            catch (InvalidCastException _e)
            {
                return null;
            }
            
            if (z)
            {
                return values[2];
            }
            else
            {
                return values[1];
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
