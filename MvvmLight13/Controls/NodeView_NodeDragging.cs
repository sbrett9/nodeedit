﻿namespace MvvmLight13.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using MahApps.Metro.Controls;
    using Utility;
    using ViewModel;

    public partial class NodeView
    {
        #region Private Methods

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
                this.cachedSelectedNodeItems = new List<NodeItem>();

                foreach (var selectedNode in this.SelectedNodes)
                {
                    NodeItem nodeItem = FindAssociatedNodeItem(selectedNode);
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
                Debug.WriteLine(string.Format("Node {0} Position = {1},{2}",Name,nodeItem.X,nodeItem.Y));
                Debug.WriteLine(string.Format("Node {0} w/h = {1},{2}", Name, nodeItem.ActualWidth, nodeItem.ActualHeight));
            }

            //detect if the head of the node intersects with any other node's tail
            if (SelectedNodes.Count == 1)
            {
                NodeItem selected = FindAssociatedNodeItem(SelectedNodes[0]);
                foreach (var nodeObj in Nodes)
                {
                    if (nodeObj == SelectedNodes[0])
                        continue;
                    NodeItem nodeItem = FindAssociatedNodeItem(nodeObj);

                    var selectedHead = Utilities.FindChild<Head>(selected);
                    var targetTail = Utilities.FindChild<Tail>(nodeItem);
                    
                    if (selectedHead == null)
                        throw new InvalidOperationException();
                    if(targetTail == null)
                        throw new InvalidOperationException();

                    Rect intersect = Utilities.DetectCollisions(selectedHead, targetTail);
                    ConnectorViewModel headConnector = selectedHead.DataContext as ConnectorViewModel;
                    ConnectorViewModel tailConnector = targetTail.DataContext as ConnectorViewModel;
                    if (intersect.IsEmpty)
                    {
                        headConnector.Highlighted = false;
                        tailConnector.Highlighted = false;
                    }
                    else
                    {
                        headConnector.Highlighted = true;
                        tailConnector.Highlighted = true;
                    }
                }
            }


            var eventArgs = new NodeDraggingEventArgs(NodeDraggingEvent, this, this.SelectedNodes, e.HorizontalChange,e.VerticalChange);
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