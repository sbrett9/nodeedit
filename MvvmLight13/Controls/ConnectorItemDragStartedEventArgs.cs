namespace MvvmLight13.Controls
{
    using System.Windows;

    /// <summary>
    /// Arguments for event raised when the user starts to drag a connector out from a node.
    /// </summary>
    internal class ConnectorItemDragStartedEventArgs : RoutedEventArgs
    {
        internal ConnectorItemDragStartedEventArgs(RoutedEvent routedEvent, object source) :
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
