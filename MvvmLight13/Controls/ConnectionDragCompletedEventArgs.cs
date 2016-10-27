namespace MvvmLight13.Controls
{
    using System.Windows;

    /// <summary>
    /// Arguments for event raised when the user has completed dragging a connector.
    /// </summary>
    public class ConnectionDragCompletedEventArgs : ConnectionDragEventArgs
    {
        #region Private Data Members

        /// <summary>
        /// The ConnectorItem or it's DataContext (when non-NULL).
        /// </summary>
        private object connectorDraggedOver = null;

        #endregion Private Data Members

        /// <summary>
        /// The ConnectorItem or it's DataContext (when non-NULL).
        /// </summary>
        public object ConnectorDraggedOver
        {
            get
            {
                return connectorDraggedOver;
            }
        }

        /// <summary>
        /// The connection that will be dragged out.
        /// </summary>
        public object Connection
        {
            get
            {
                return connection;
            }
        }

        #region Private Methods

        internal ConnectionDragCompletedEventArgs(RoutedEvent routedEvent, object source, object node, object connection, object connector, object connectorDraggedOver) :
            base(routedEvent, source, node, connection, connector)
        {
            this.connectorDraggedOver = connectorDraggedOver;
        }

        #endregion Private Methods
    }
}
