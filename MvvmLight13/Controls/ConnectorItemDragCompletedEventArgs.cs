namespace MvvmLight13.Controls
{
    using System.Windows;

    /// <summary>
    /// Arguments for event raised when the user has completed dragging a connector.
    /// </summary>
    internal class ConnectorItemDragCompletedEventArgs : RoutedEventArgs
    {
        public ConnectorItemDragCompletedEventArgs(RoutedEvent routedEvent, object source) :
            base(routedEvent, source)
        {
        }
    }
}
