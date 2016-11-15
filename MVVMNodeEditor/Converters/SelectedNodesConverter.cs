namespace MVVMNodeEditor.Converters
{
    #region Using Declarations

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using Interfaces;
    using Utility;
    using System.Linq;
    using System.Linq.Expressions;

    #endregion

    public class SelectedNodesConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            IEnumerable z = value as IEnumerable;
            var k = z.Cast<INodeViewModel>();
            ObservableCollection<INodeViewModel> m = new ObservableCollection<INodeViewModel>(k);
            return m;
        }
    }
}
