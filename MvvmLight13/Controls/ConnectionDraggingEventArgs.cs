namespace MvvmLight13.Controls
{
    using System.Windows;

    /// <summary>
    /// Arguments for event raised while user is dragging a node in the network.
    /// </summary>
    public class ConnectionDraggingEventArgs : ConnectionDragEventArgs
    {
        /// <summary>
        /// The connection being dragged out.
        /// </summary>
        public object Connection
        {
            get
            {
                return connection;
            }
        }

        #region Private Methods

        internal ConnectionDraggingEventArgs(RoutedEvent routedEvent, object source,
                object node, object connection, object connector) :
            base(routedEvent, source, node, connection, connector)
        {
        }

        #endregion Private Methods
    }

}
