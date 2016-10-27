namespace MvvmLight13.Controls
{
    #region Using Declarations

    using System.Windows;

    #endregion

    /// <summary>
    /// Base class for connection dragging event args.
    /// </summary>
    public class ConnectionDragEventArgs : RoutedEventArgs
    {
        #region Private Data Members

        /// <summary>
        /// The NodeItem or it's DataContext (when non-NULL).
        /// </summary>
        private object node = null;

        /// <summary>
        /// The ConnectorItem or it's DataContext (when non-NULL).
        /// </summary>
        private object draggedOutConnector = null;

        /// <summary>
        /// The connector that will be dragged out.
        /// </summary>
        protected object connection = null;

        #endregion Private Data Members

        /// <summary>
        /// The NodeItem or it's DataContext (when non-NULL).
        /// </summary>
        public object Node
        {
            get
            {
                return node;
            }
        }

        /// <summary>
        /// The ConnectorItem or it's DataContext (when non-NULL).
        /// </summary>
        public object ConnectorDraggedOut
        {
            get
            {
                return draggedOutConnector;
            }
        }

        #region Private Methods

        protected ConnectionDragEventArgs(RoutedEvent routedEvent, object source, object node, object connection, object connector) :
            base(routedEvent, source)
        {
            this.node = node;
            this.draggedOutConnector = connector;
            this.connection = connection;
        }

        #endregion Private Methods
    }

}
