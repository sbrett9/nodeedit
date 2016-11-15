namespace MvvmLight13.Events.Drag.Node.NodeDragCompleted
{
    #region Using Declarations

    using System.Collections;
    using System.Windows;
    using NodeDrag;

    #endregion

    /// <summary>
    /// Arguments for event raised when the user has completed dragging a node in the network.
    /// </summary>
    public class NodeDragCompletedEventArgs : NodeDragEventArgs
    {
        public NodeDragCompletedEventArgs(RoutedEvent routedEvent, object source, ICollection nodes) :
            base(routedEvent, source, nodes)
        {
        }
    }

}
