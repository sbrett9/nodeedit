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

    /// <summary>
    /// This is the UI element for a connector.
    /// Each nodes has multiple connectors that are used to connect it to other nodes.
    /// </summary>
    public class Edge : ContentControl
    {
        #region Dependency Property/Event Definitions
        public static readonly DependencyProperty XProperty = DependencyProperty.Register("X", typeof(double), typeof(Edge), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty YProperty = DependencyProperty.Register("Y", typeof(double), typeof(Edge), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty ZIndexProperty = DependencyProperty.Register("ZIndex", typeof(int), typeof(Edge), new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty HotspotProperty =DependencyProperty.Register("Hotspot", typeof(Point), typeof(Edge));

        public static readonly DependencyProperty ParentNodeViewProperty =DependencyProperty.Register("ParentNodeView", typeof(NodeView), typeof(Edge),new FrameworkPropertyMetadata(ParentNodeView_PropertyChanged));

        public static readonly DependencyProperty ParentNodeItemProperty =DependencyProperty.Register("ParentNodeItem", typeof(Node), typeof(Edge));

        public static readonly RoutedEvent EdgeDragStartedEvent =EventManager.RegisterRoutedEvent("EdgeDragStarted", RoutingStrategy.Direct, typeof(EdgeDragStartedEventHandler), typeof(Edge));
        public static readonly RoutedEvent EdgeDraggingEvent =EventManager.RegisterRoutedEvent("EdgeDragging", RoutingStrategy.Direct, typeof(EdgeDraggingEventHandler), typeof(Edge));
        public static readonly RoutedEvent EdgeDragCompletedEvent =EventManager.RegisterRoutedEvent("EdgeDragCompleted", RoutingStrategy.Direct, typeof(EdgeDragCompletedEventHandler), typeof(Edge));

        #endregion Dependency Property/Event Definitions

        #region Private Data Members

        /// <summary>
        /// The point the mouse was last at when dragging.
        /// </summary>
        private Point lastMousePoint;

        /// <summary>
        /// Set to 'true' when left mouse button is held down.
        /// </summary>
        private bool isLeftMouseDown = false;

        /// <summary>
        /// Set to 'true' when the user is dragging the connector.
        /// </summary>
        private bool isDragging = false;

        /// <summary>
        /// The threshold distance the mouse-cursor must move before dragging begins.
        /// </summary>
        private static readonly double DragThreshold = 2;

        #endregion Private Data Members

        public Edge()
        {
            //
            // By default, we don't want a connector to be focusable.
            //
            Focusable = false;

            //
            // Hook layout update to recompute 'Hotspot' when the layout changes.
            //
            this.LayoutUpdated += new EventHandler(Edge_LayoutUpdated);
            this.Loaded += new RoutedEventHandler(Edge_Loaded);
        }

        /// <summary>
        /// Automatically updated dependency property that specifies the hotspot (or center point) of the connector.
        /// Specified in content coordinate.
        /// </summary>
        public Point Hotspot
        {
            get
            {
                return (Point)GetValue(HotspotProperty);
            }
            set
            {
                SetValue(HotspotProperty, value);
            }
        }

        #region Private Data Members\Properties

        /// <summary>
        /// Reference to the data-bound parent NodeView.
        /// </summary>
        internal NodeView ParentNodeView
        {
            get
            {
                return (NodeView)GetValue(ParentNodeViewProperty);
            }
            set
            {
                SetValue(ParentNodeViewProperty, value);
            }
        }


        /// <summary>
        /// Reference to the data-bound parent NodeItem.
        /// </summary>
        internal Node ParentNodeItem
        {
            get
            {
                return (Node)GetValue(ParentNodeItemProperty);
            }
            set
            {
                SetValue(ParentNodeItemProperty, value);
            }
        }

        #endregion Private Data Members\Properties

        #region Private Methods

        /// <summary>
        /// Static constructor.
        /// </summary>
        static Edge()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Edge), new FrameworkPropertyMetadata(typeof(Edge)));
        }


        /// <summary>
        /// A mouse button has been held down.
        /// </summary>
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            if (this.ParentNodeItem != null)
            {
                this.ParentNodeItem.BringToFront();
            }

            if (this.ParentNodeView != null)
            {
                this.ParentNodeView.Focus();
            }

            if (e.ChangedButton == MouseButton.Left)
            {
                if (this.ParentNodeItem != null)
                {
                    //
                    // Delegate to parent node to execute selection logic.
                    //
                    this.ParentNodeItem.LeftMouseDownSelectionLogic();
                }

                lastMousePoint = e.GetPosition(this.ParentNodeView);
                isLeftMouseDown = true;
                e.Handled = true;
            }
            else if (e.ChangedButton == MouseButton.Right)
            {
                if (this.ParentNodeItem != null)
                {
                    //
                    // Delegate to parent node to execute selection logic.
                    //
                    this.ParentNodeItem.RightMouseDownSelectionLogic();
                }
            }
        }

        /// <summary>
        /// The mouse cursor has been moved.
        /// </summary>        
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (isDragging)
            {
                //
                // Raise the event to notify that dragging is in progress.
                //

                Point curMousePoint = e.GetPosition(this.ParentNodeView);
                Vector offset = curMousePoint - lastMousePoint;
                if (offset.X != 0.0 && offset.Y != 0.0)
                {
                    lastMousePoint = curMousePoint;

                    RaiseEvent(new EdgeDraggingEventArgs(EdgeDraggingEvent, this, offset.X, offset.Y));
                }

                e.Handled = true;
            }
            else if (isLeftMouseDown)
            {
                if (this.ParentNodeView != null && this.ParentNodeView.EnableConnectionDragging)
                {
                    //
                    // The user is left-dragging the connector and connection dragging is enabled,
                    // but don't initiate the drag operation until 
                    // the mouse cursor has moved more than the threshold distance.
                    //
                    Point curMousePoint = e.GetPosition(this.ParentNodeView);
                    var dragDelta = curMousePoint - lastMousePoint;
                    double dragDistance = Math.Abs(dragDelta.Length);
                    if (dragDistance > DragThreshold)
                    {
                        //
                        // When the mouse has been dragged more than the threshold value commence dragging the node.
                        //

                        //
                        // Raise an event to notify that that dragging has commenced.
                        //
                        var eventArgs = new EdgeDragStartedEventArgs(EdgeDragStartedEvent, this);
                        RaiseEvent(eventArgs);

                        if (eventArgs.Cancel)
                        {
                            //
                            // Handler of the event disallowed dragging of the node.
                            //
                            isLeftMouseDown = false;
                            return;
                        }

                        isDragging = true;
                        this.CaptureMouse();
                        e.Handled = true;
                    }
                }
            }
        }

        /// <summary>
        /// A mouse button has been released.
        /// </summary>
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.ChangedButton == MouseButton.Left)
            {
                if (isLeftMouseDown)
                {
                    if (isDragging)
                    {
                        RaiseEvent(new EdgeDragCompletedEventArgs(EdgeDragCompletedEvent, this));

                        this.ReleaseMouseCapture();

                        isDragging = false;
                    }
                    else
                    {
                        //
                        // Execute mouse up selection logic only if there was no drag operation.
                        //
                        if (this.ParentNodeItem != null)
                        {
                            //
                            // Delegate to parent node to execute selection logic.
                            //
                            this.ParentNodeItem.LeftMouseUpSelectionLogic();
                        }
                    }

                    isLeftMouseDown = false;

                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// Cancel connection dragging for the connector that was dragged out.
        /// </summary>
        internal void CancelConnectionDragging()
        {
            if (isLeftMouseDown)
            {
                //
                // Raise ConnectorDragCompleted, with a null connector.
                //
                RaiseEvent(new EdgeDragCompletedEventArgs(EdgeDragCompletedEvent, null));

                isLeftMouseDown = false;
                this.ReleaseMouseCapture();
            }
        }

        /// <summary>
        /// Event raised when 'ParentNodeView' property has changed.
        /// </summary>
        private static void ParentNodeView_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Edge c = (Edge)d;
            c.UpdateHotspot();
        }


        private void Edge_Loaded(object _sender, RoutedEventArgs _e)
        {
            
        }


        /// <summary>
        /// Event raised when the layout of the connector has been updated.
        /// </summary>
        private void Edge_LayoutUpdated(object sender, EventArgs e)
        {
            UpdateHotspot();
        }

        /// <summary>
        /// Update the connector hotspot.
        /// </summary>
        private void UpdateHotspot()
        {
            if (this.ParentNodeView == null)
            {
                // No parent NodeView is set.
                return;
            }

            if (!this.ParentNodeView.IsAncestorOf(this))
            {
                //
                // The parent NodeView is no longer an ancestor of the connector.
                // This happens when the connector (and its parent node) has been removed from the network.
                // Reset the property null so we don't attempt to check again.
                //
                this.ParentNodeView = null;
                return;
            }

            //
            // The parent NodeView is still valid.
            // Compute the center point of the connector.
            //
            var centerPoint = new Point(this.ActualWidth / 2, this.ActualHeight / 2);

            //
            // Transform the center point so that it is relative to the parent NodeView.
            // Then assign it to Hotspot.  Usually Hotspot will be data-bound to the application
            // view-model using OneWayToSource so that the value of the hotspot is then pushed through
            // to the view-model.
            //
            this.Hotspot = this.TransformToAncestor(this.ParentNodeView).Transform(centerPoint);
        }

        #endregion Private Methods
    }
}
