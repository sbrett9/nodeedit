namespace MvvmLight13.Events.Drag.Edge.EdgeDragCompleted
{
    using System.Windows;

    /// <summary>
    /// Arguments for event raised when the user has completed dragging a connector.
    /// </summary>
    internal class EdgeDragCompletedEventArgs : RoutedEventArgs
    {
        public EdgeDragCompletedEventArgs(RoutedEvent routedEvent, object source) :
            base(routedEvent, source)
        {
        }
    }
}
