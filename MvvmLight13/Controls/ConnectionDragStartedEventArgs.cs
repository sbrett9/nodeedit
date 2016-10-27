namespace MvvmLight13.Controls
{
    using System.Windows;

    /// <summary>
    /// Arguments for event raised when the user starts to drag a connection out from a node.
    /// </summary>
    public class ConnectionDragStartedEventArgs : ConnectionDragEventArgs
    {
        /// <summary>
        /// The connection that will be dragged out.
        /// </summary>
        public object Connection
        {
            get
            {
                return connection;
            }
            set
            {
                connection = value;
            }
        }

        #region Private Methods

        internal ConnectionDragStartedEventArgs(RoutedEvent routedEvent, object source, object node, object connector) :
            base(routedEvent, source, node, null, connector)
        {
        }

        #endregion Private Methods
    }
}
