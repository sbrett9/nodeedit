namespace MvvmLight13.Events.Drag.Edge.EdgeDragging
{
    using System.Windows;

    /// <summary>
    /// Arguments for event raised while user is dragging a node in the network.
    /// </summary>
    internal class EdgeDraggingEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// The amount the connector has been dragged horizontally.
        /// </summary>
        private double horizontalChange = 0;

        /// <summary>
        /// The amount the connector has been dragged vertically.
        /// </summary>
        private double verticalChange = 0;

        public EdgeDraggingEventArgs(RoutedEvent routedEvent, object source, double horizontalChange, double verticalChange) :
            base(routedEvent, source)
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
