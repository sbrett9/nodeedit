﻿namespace MvvmLight13.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class EqualityToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Equals(value, parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool) value)
                return parameter;

            return Binding.DoNothing;
        }
    }
}