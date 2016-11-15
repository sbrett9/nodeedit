namespace Interfaces
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;

    public interface INodeViewModel
    {
        event EventHandler<EventArgs> SizeChanged;

        FrameworkElement Visual { get; set; }
        INetworkViewModel ParentNetworkView { get; set;  }
        IEdgeViewModel LeftEdgeViewModel { get; }
        IEdgeViewModel RightEdgeViewModel { get; }
        double X { get; set; }
        double Y { get; set; }
        double Width { get; set; }
        double Height { get; set; }
        int ZIndex { get; set; }
        Point LastMousePoint { get; set; }
        bool IsLeftMouseDown { get; set; }
        bool IsLeftMouseAndControlDown { get; set; }
        bool IsDragging { get; set; }
        bool Focusable { get; set; }
        bool IsSelected { get; set; }
        bool IsFastened { get; set; }
        IOperationViewModel OperationModel { get; set; }

        //RelayCommand<>
        RelayCommand<FrameworkElement> VisualLoadedCommand { get; }
        RelayCommand VisualUpdatedCommand { get; }
        RelayCommand<MouseButtonEventArgs> DragStartedCommand { get;}
        RelayCommand<MouseEventArgs> DraggingCommand { get;  }
        RelayCommand<MouseButtonEventArgs> DragCompletedCommand { get;  }


        void ExecuteDragStartedCommand(MouseButtonEventArgs _e);
        void ExecuteDraggingCommand(MouseEventArgs _e);
        void ExecuteDragCompletedCommand(MouseButtonEventArgs _e);
        void ExecuteVisualLoadedCommand(FrameworkElement _element);

        void BringToFront();



    }
}
