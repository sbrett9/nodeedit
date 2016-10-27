namespace MvvmLight13.Controls
{
    #region Using Declarations

    using System.Windows;

    #endregion

    /// <summary>
    /// Arguments for event raised while user is dragging a node in the network.
    /// </summary>
    public class QueryConnectionFeedbackEventArgs : ConnectionDragEventArgs
    {
        #region Private Data Members

        /// <summary>
        /// The ConnectorItem or it's DataContext (when non-NULL).
        /// </summary>
        private object draggedOverConnector = null;

        /// <summary>
        /// Set to 'true' / 'false' to indicate that the connection from the dragged out connection to the dragged over connector is valid.
        /// </summary>
        private bool connectionOk = true;

        /// <summary>
        /// The indicator to display.
        /// </summary>
        private object feedbackIndicator = null;

        #endregion Private Data Members

        /// <summary>
        /// The ConnectorItem or it's DataContext (when non-NULL).
        /// </summary>
        public object DraggedOverConnector
        {
            get
            {
                return draggedOverConnector;
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

        /// <summary>
        /// Set to 'true' / 'false' to indicate that the connection from the dragged out connection to the dragged over connector is valid.
        /// </summary>
        public bool ConnectionOk
        {
            get
            {
                return connectionOk;
            }
            set
            {
                connectionOk = value;
            }
        }

        /// <summary>
        /// The indicator to display.
        /// </summary>
        public object FeedbackIndicator
        {
            get
            {
                return feedbackIndicator;
            }
            set
            {
                feedbackIndicator = value;
            }
        }

        #region Private Methods

        internal QueryConnectionFeedbackEventArgs(RoutedEvent routedEvent, object source,
            object node, object connection, object connector, object draggedOverConnector) :
            base(routedEvent, source, node, connection, connector)
        {
            this.draggedOverConnector = draggedOverConnector;
        }

        #endregion Private Methods
    }

}
