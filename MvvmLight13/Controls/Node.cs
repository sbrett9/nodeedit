namespace MvvmLight13.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using Events.Drag.Edge.EdgeDragCompleted;
    using Events.Drag.Edge.EdgeDragging;
    using Events.Drag.Edge.EdgeDragStarted;
    using Events.Drag.Node.NodeDragCompleted;
    using Events.Drag.Node.NodeDragging;
    using Events.Drag.Node.NodeDragStarted;
    using Events.Insert.NodeInsertCompleted;
    using Events.Insert.NodeInserting;
    using Events.Insert.NodeInsertStarted;

    /// <summary>
    ///     This is a UI element that represents a network/flow-chart node.
    /// </summary>
    public class Node : ListBoxItem
    {
        public Node()
        {
            //
            // By default, we don't want this UI element to be focusable.
            //
            Focusable = false;
        }

        /// <summary>
        ///     The X coordinate of the node.
        /// </summary>
        public double X
        {
            get { return (double) GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        /// <summary>
        ///     The Y coordinate of the node.
        /// </summary>
        public double Y
        {
            get { return (double) GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        /// <summary>
        ///     The Z index of the node.
        /// </summary>
        public int ZIndex
        {
            get { return (int) GetValue(ZIndexProperty); }
            set { SetValue(ZIndexProperty, value); }
        }

        #region Dependency Property/Event Definitions

        public static readonly DependencyProperty XProperty = DependencyProperty.Register("X", typeof(double), typeof(Node),new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty YProperty = DependencyProperty.Register("Y", typeof(double), typeof(Node), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        
        public static readonly DependencyProperty ZIndexProperty = DependencyProperty.Register("ZIndex", typeof(int), typeof(Node), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty ParentNodeViewProperty = DependencyProperty.Register("ParentNodeView", typeof(NodeView), typeof(Node),new FrameworkPropertyMetadata(ParentNodeView_PropertyChanged));

        public static readonly RoutedEvent NodeDragStartedEvent = EventManager.RegisterRoutedEvent("NodeDragStarted", RoutingStrategy.Bubble,typeof(NodeDragStartedEventHandler), typeof(Node));

        public static readonly RoutedEvent NodeDraggingEvent = EventManager.RegisterRoutedEvent("NodeDragging", RoutingStrategy.Bubble, typeof(NodeDraggingEventHandler), typeof(Node));

        public static readonly RoutedEvent NodeDragCompletedEvent = EventManager.RegisterRoutedEvent("NodeDragCompleted", RoutingStrategy.Bubble, typeof(NodeDragCompletedEventHandler), typeof(Node));

        public static readonly RoutedEvent NodeInsertStartedEvent = EventManager.RegisterRoutedEvent("NodeInsertStarted", RoutingStrategy.Bubble, typeof(NodeInsertStartedEventHandler), typeof(Node));
        public static readonly RoutedEvent NodeInsertingEvent = EventManager.RegisterRoutedEvent("NodeInserting", RoutingStrategy.Bubble, typeof(NodeInsertingEventHandler), typeof(Node));
        public static readonly RoutedEvent NodeInsertCompletedEvent = EventManager.RegisterRoutedEvent("NodeInsertCompleted", RoutingStrategy.Bubble, typeof(NodeInsertCompletedEventHandler), typeof(Node));


        #endregion Dependency Property/Event Definitions

        #region Private Data Members\Properties

        /// <summary>
        ///     Reference to the data-bound parent NodeView.
        /// </summary>
        internal NodeView ParentNodeView
        {
            get { return (NodeView) GetValue(ParentNodeViewProperty); }
            set { SetValue(ParentNodeViewProperty, value); }
        }

        /// <summary>
        ///     The point the mouse was last at when dragging.
        /// </summary>
        private Point lastMousePoint;

        /// <summary>
        ///     Set to 'true' when left mouse button is held down.
        /// </summary>
        private bool isLeftMouseDown;

        /// <summary>
        ///     Set to 'true' when left mouse button and the control key are held down.
        /// </summary>
        private bool isLeftMouseAndControlDown;

        /// <summary>
        ///     Set to 'true' when dragging has started.
        /// </summary>
        private bool isDragging;

        /// <summary>
        ///     The threshold distance the mouse-cursor must move before dragging begins.
        /// </summary>
        private static readonly double DragThreshold = 5;

        #endregion Private Data Members\Properties

        #region Private Methods

        /// <summary>
        ///     Static constructor.
        /// </summary>
        static Node()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Node), new FrameworkPropertyMetadata(typeof(Node)));
        }

        /// <summary>
        ///     Bring the node to the front of other elements.
        /// </summary>
        internal void BringToFront()
        {
            if (ParentNodeView == null)
            {
                return;
            }

            int maxZ = ParentNodeView.FindMaxZIndex();
            ZIndex = maxZ + 1;
        }

        /// <summary>
        ///     Called when a mouse button is held down.
        /// </summary>
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            BringToFront();

            if (ParentNodeView != null)
            {
                ParentNodeView.Focus();
            }

            if (e.ChangedButton == MouseButton.Left && ParentNodeView != null)
            {
                lastMousePoint = e.GetPosition(ParentNodeView);
                isLeftMouseDown = true;

                LeftMouseDownSelectionLogic();

                e.Handled = true;
            }
            else if (e.ChangedButton == MouseButton.Right && ParentNodeView != null)
            {
                RightMouseDownSelectionLogic();
            }
        }

        /// <summary>
        ///     This method contains selection logic that is invoked when the left mouse button is pressed down.
        ///     The reason this exists in its own method rather than being included in OnMouseDown is
        ///     so that ConnectorItem can reuse this logic from its OnMouseDown.
        /// </summary>
        internal void LeftMouseDownSelectionLogic()
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) != 0)
            {
                //
                // Control key was held down.
                // This means that the rectangle is being added to or removed from the existing selection.
                // Don't do anything yet, we will act on this later in the MouseUp event handler.
                //
                isLeftMouseAndControlDown = true;
            }
            else
            {
                //
                // Control key is not held down.
                //
                isLeftMouseAndControlDown = false;

                if (ParentNodeView.SelectedNodes.Count == 0)
                {
                    //
                    // Nothing already selected, select the item.
                    //
                    IsSelected = true;
                }
                else if (ParentNodeView.SelectedNodes.Contains(this) ||
                         ParentNodeView.SelectedNodes.Contains(DataContext))
                {
                    // 
                    // Item is already selected, do nothing.
                    // We will act on this in the MouseUp if there was no drag operation.
                    //
                }
                else
                {
                    //
                    // Item is not selected.
                    // Deselect all, and select the item.
                    //
                    ParentNodeView.SelectedNodes.Clear();
                    IsSelected = true;
                }
            }
        }

        /// <summary>
        ///     This method contains selection logic that is invoked when the right mouse button is pressed down.
        ///     The reason this exists in its own method rather than being included in OnMouseDown is
        ///     so that ConnectorItem can reuse this logic from its OnMouseDown.
        /// </summary>
        internal void RightMouseDownSelectionLogic()
        {
            if (ParentNodeView.SelectedNodes.Count == 0)
            {
                //
                // Nothing already selected, select the item.
                //
                IsSelected = true;
            }
            else if (ParentNodeView.SelectedNodes.Contains(this) ||
                     ParentNodeView.SelectedNodes.Contains(DataContext))
            {
                // 
                // Item is already selected, do nothing.
                //
            }
            else
            {
                //
                // Item is not selected.
                // Deselect all, and select the item.
                //
                ParentNodeView.SelectedNodes.Clear();
                IsSelected = true;
            }
        }

        /// <summary>
        ///     Called when the mouse cursor is moved.
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (isDragging)
            {
                //
                // Raise the event to notify that dragging is in progress.
                //

                var curMousePoint = e.GetPosition(ParentNodeView);

                object item = this;
                if (DataContext != null)
                {
                    item = DataContext;
                }

                var offset = curMousePoint - lastMousePoint;
                if (offset.X != 0.0 || offset.Y != 0.0)
                {
                    lastMousePoint = curMousePoint;
                    RaiseEvent(new NodeDraggingEventArgs(NodeDraggingEvent, this, new[] {item}, offset.X, offset.Y));
                }
            }
            else if (isLeftMouseDown && ParentNodeView.EnableNodeDragging)
            {
                //
                // The user is left-dragging the node,
                // but don't initiate the drag operation until 
                // the mouse cursor has moved more than the threshold distance.
                //
                var curMousePoint = e.GetPosition(ParentNodeView);
                var dragDelta = curMousePoint - lastMousePoint;
                var dragDistance = Math.Abs(dragDelta.Length);
                if (dragDistance > DragThreshold)
                {
                    //
                    // When the mouse has been dragged more than the threshold value commence dragging the node.
                    //

                    //
                    // Raise an event to notify that that dragging has commenced.
                    //
                    NodeDragStartedEventArgs eventArgs = new NodeDragStartedEventArgs(NodeDragStartedEvent, this, new[] {this});
                    RaiseEvent(eventArgs);

                    if (eventArgs.Cancel)
                    {
                        //
                        // Handler of the event disallowed dragging of the node.
                        //
                        isLeftMouseDown = false;
                        isLeftMouseAndControlDown = false;
                        return;
                    }

                    isDragging = true;
                    CaptureMouse();
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        ///     Called when a mouse button is released.
        /// </summary>
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);

            if (isLeftMouseDown)
            {
                if (isDragging)
                {
                    //
                    // Raise an event to notify that node dragging has finished.
                    //

                    RaiseEvent(new NodeDragCompletedEventArgs(NodeDragCompletedEvent, this, new[] {this}));

                    ReleaseMouseCapture();

                    isDragging = false;
                }
                else
                {
                    //
                    // Execute mouse up selection logic only if there was no drag operation.
                    //

                    LeftMouseUpSelectionLogic();
                }

                isLeftMouseDown = false;
                isLeftMouseAndControlDown = false;

                //unconditionally unselect any node if it is not part of a group selection
                if (ParentNodeView.SelectedNodes.Count == 1)
                    ParentNodeView.SelectedNodes.Clear();

                e.Handled = true;

            }


        }

        /// <summary>
        ///     This method contains selection logic that is invoked when the left mouse button is released.
        ///     The reason this exists in its own method rather than being included in OnMouseUp is
        ///     so that ConnectorItem can reuse this logic from its OnMouseUp.
        /// </summary>
        internal void LeftMouseUpSelectionLogic()
        {
            if (isLeftMouseAndControlDown)
            {
                //
                // Control key was held down.
                // Toggle the selection.
                //
                IsSelected = !IsSelected;
            }
            else
            {
                //
                // Control key was not held down.
                //
                if (ParentNodeView.SelectedNodes.Count == 1 &&
                    (ParentNodeView.SelectedNode == this ||
                     ParentNodeView.SelectedNode == DataContext))
                {
                    //
                    // The item that was clicked is already the only selected item.
                    // Don't need to do anything.
                    //
                }
                else
                {
                    //
                    // Clear the selection and select the clicked item as the only selected item.
                    //
                    ParentNodeView.SelectedNodes.Clear();
                    IsSelected = true;
                }
            }

            isLeftMouseAndControlDown = false;
        }

        /// <summary>
        ///     Event raised when the ParentNodeView property has changed.
        /// </summary>
        private static void ParentNodeView_PropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            //
            // Bring new nodes to the front of the z-order.
            //
            var nodeItem = (Node) o;
            nodeItem.BringToFront();
        }

        #endregion Private Methods
    }
}