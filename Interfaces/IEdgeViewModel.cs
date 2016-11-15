namespace Interfaces
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;

    public interface IEdgeViewModel
    {
        event EventHandler<EventArgs> SizeChanged;

        FrameworkElement Visual { get; set; }
        string Name { get; set; }
        INetworkViewModel ParentNetworkView { get; }
        INodeViewModel ParentNode { get; }
        INodeViewModel TargetNode { get; set; }
        double X { get; set; }
        double Y { get; set; }
        double Width { get; set; }
        double Height { get; set; }
        int ZIndex { get; set; }
        bool Highlighted { get; set; }
        ConnectorType ConnectorType { get;}
        bool IsConnected { get; }


        RelayCommand<FrameworkElement> VisualLoadedCommand { get; }
        void ExecuteVisualLoadedCommand(FrameworkElement _e);
    }
}
