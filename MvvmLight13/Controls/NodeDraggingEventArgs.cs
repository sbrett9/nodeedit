namespace MvvmLight13.Controls
{
    #region Using Declarations

    using System.Collections;
    using System.Windows;

    #endregion

    /// <summary>
    /// Arguments for event raised while user is dragging a node in the network.
    /// </summary>
    public class NodeDraggingEventArgs : NodeDragEventArgs
    {
        /// <summary>
        /// The amount the node has been dragged horizontally.
        /// </summary>
        public double horizontalChange = 0;

        /// <summary>
        /// The amount the node has been dragged vertically.
        /// </summary>
        public double verticalChange = 0;

        internal NodeDraggingEventArgs(RoutedEvent routedEvent, object source, ICollection nodes, double horizontalChange, double verticalChange) :
            base(routedEvent, source, nodes)
        {
            this.horizontalChange = horizontalChange;
            this.verticalChange = verticalChange;
        }

        /// <summary>
        /// The amount the node has been dragged horizontally.
        /// </summary>
        public double HorizontalChange
        {
            get
            {
                return horizontalChange;
            }
        }

        /// <summary>
        /// The amount the node has been dragged vertically.
        /// </summary>
        public double VerticalChange
        {
            get
            {
                return verticalChange;
            }
        }
    }
}
