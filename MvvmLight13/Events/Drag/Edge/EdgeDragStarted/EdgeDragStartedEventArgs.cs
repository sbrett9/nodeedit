namespace MvvmLight13.Events.Drag.Edge.EdgeDragStarted
{
    using System.Windows;

    /// <summary>
    /// Arguments for event raised when the user starts to drag a connector out from a node.
    /// </summary>
    internal class EdgeDragStartedEventArgs : RoutedEventArgs
    {
        internal EdgeDragStartedEventArgs(RoutedEvent routedEvent, object source) :
            base(routedEvent, source)
        {
        }

        /// <summary>
        /// Cancel dragging out of the connector.
        /// </summary>
        public bool Cancel
        {
            get;
            set;
        }
    }
}
