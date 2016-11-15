namespace Interfaces
{
    #region Using Declarations

    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using GalaSoft.MvvmLight.Command;

    #endregion

    public interface INetworkViewModel
    {
        FrameworkElement Visual { get; set; }
        event SelectionChangedEventHandler SelectionChanged;
        ObservableCollection<INodeViewModel> Nodes { get; }
        ObservableCollection<IEdgeViewModel> Edges { get; }
        INodeViewModel SelectedNode { get; set; }
        bool EnableNodeDragging { get; set; }
        bool IsDragging { get; set; }
        RelayCommand<FrameworkElement> VisualLoadedCommand { get; }
        int FindMaxZIndex();
        bool RestrictNodesToVisual { get; set; }
        void UpdateVisual();
    }
}
