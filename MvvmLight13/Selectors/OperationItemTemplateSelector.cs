namespace MvvmLight13.Selectors
{
    using System.Windows;
    using System.Windows.Controls;
    using GalaSoft.MvvmLight;
    using ViewModel;

    public class OperationItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate AspirateOperationTemplate { get; set; }
        public DataTemplate DispenseOperationTemplate { get; set; }
        public DataTemplate TipEjectOperationTemplate { get; set; }
        public DataTemplate TipPickupOperationTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var viewModel = item as ViewModelBase;
            if (viewModel is NodeViewModel)
            {
                return AspirateOperationTemplate;
            }
            return null;
        }
    }
}