namespace MvvmLight13.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Input;
    using Events.Drag.Edge.EdgeDragging;
    using Events.Drag.Node.NodeDragCompleted;
    using Events.Drag.Node.NodeDragging;
    using Events.Drag.Node.NodeDragStarted;
    using ViewModel;

    public partial class NodeView
    {
        #region Private Methods

        private void SetEdgePositions(EdgeViewModel edgeModel, double x, double y)
        {
            edgeModel.X = x;
            edgeModel.Y = y;
        }
        private void UpdateEdgePositions(EdgeViewModel edgeModel, double _deltaX, double _deltaY)
        {
            edgeModel.X += _deltaX;
            edgeModel.Y += _deltaY;
        }

        /// <summary>
        ///     Event raised when the user starts to drag a node.
        /// </summary>
        private void NodeItem_DragStarted(object source, NodeDragStartedEventArgs e)
        {
            e.Handled = true;

            this.IsDragging = true;
            this.IsNotDragging = false;
            this.IsDraggingNode = true;
            this.IsNotDraggingNode = false;

            var eventArgs = new NodeDragStartedEventArgs(NodeDragStartedEvent, this, this.SelectedNodes);
            RaiseEvent(eventArgs);

            e.Cancel = eventArgs.Cancel;
        }

        /// <summary>
        ///     Event raised while the user is dragging a node.
        /// </summary>
        private void NodeItem_Dragging(object source, NodeDraggingEventArgs e)
        {
            e.Handled = true;

            //
            // Cache the NodeItem for each selected node whilst dragging is in progress.
            //
            if (this.cachedSelectedNodeItems == null)
            {
                this.cachedSelectedNodeItems = new List<Node>();

                foreach (var selectedNode in this.SelectedNodes)
                {
                    Node nodeItem = FindAssociatedNodeItem(selectedNode);
                    if (nodeItem == null)
                    {
                        throw new ApplicationException("Unexpected code path!");
                    }

                    this.cachedSelectedNodeItems.Add(nodeItem);
                }
            }

            // 
            // Update the position of the node within the Canvas.
            //
            foreach (var nodeItem in this.cachedSelectedNodeItems)
            {
                nodeItem.X += e.HorizontalChange;
                nodeItem.Y += e.VerticalChange;
                NodeViewModelBase vm = nodeItem.Content as NodeViewModelBase;
                if (vm != null)
                {
                    vm.Left.X += e.HorizontalChange;
                    vm.Left.Y += e.VerticalChange;
                    vm.Right.X += e.HorizontalChange;
                    vm.Right.Y += e.VerticalChange;
                }

            }

            //Detect Selected node insert operation
            Point p = Mouse.GetPosition(this);
            List<object> unselected = new List<object>();
            foreach (var node in Nodes)
            {
                if (!SelectedNodes.Contains(node))
                    unselected.Add(node);

            }
            foreach (var node in unselected)
            {
                Node nodeItem = FindAssociatedNodeItem(node);
                NodeViewModelBase vm = nodeItem.Content as NodeViewModelBase;
                if (vm == null)
                    throw new NotSupportedException("Node Control contents must inherit from NodeViewModelBase.");
                var targetRect = new Rect(new Point(vm.Left.X, vm.Left.Y), new Size(vm.Left.Width, vm.Left.Height));
            }


            var eventArgs = new NodeDraggingEventArgs(NodeDraggingEvent, this, this.SelectedNodes, e.HorizontalChange, e.VerticalChange);
            RaiseEvent(eventArgs);
        }


        /// <summary>
        ///     Event raised when the user has finished dragging a node.
        /// </summary>
        private void NodeItem_DragCompleted(object source, NodeDragCompletedEventArgs e)
        {
            e.Handled = true;

            var eventArgs = new NodeDragCompletedEventArgs(NodeDragCompletedEvent, this, this.SelectedNodes);
            RaiseEvent(eventArgs);

            if (cachedSelectedNodeItems != null)
            {
                cachedSelectedNodeItems = null;
            }

            this.IsDragging = false;
            this.IsNotDragging = true;
            this.IsDraggingNode = false;
            this.IsNotDraggingNode = true;

        }

        #endregion Private Methods
    }
}