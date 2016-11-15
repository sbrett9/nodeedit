namespace MVVMNodeEditor.ViewModel
{
    #region Using Declarations

    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;
    using Interfaces;
    using Messages;

    #endregion

    public class NodeViewModel : ViewModelBase, INodeViewModel
    {
        #region Events
        public event EventHandler<EventArgs> SizeChanged;
        #endregion

        #region Members
        private static readonly double dragThreshold = 5;
        private INetworkViewModel parentNetworkView;
        private IEdgeViewModel leftEdgeViewModel;
        private IEdgeViewModel rightEdgeViewModel;
        private double x;
        private double y;
        private double width;
        private double height;
        private int zIndex;
        private Point lastMousePoint;
        private Point lastPointWithinBounds;
        private bool isLeftMouseDown;
        private bool isLeftMouseAndControlDown;
        private bool isDragging;
        private bool focusable;
        private bool isSelected;
        private FrameworkElement visual;
        private IOperationViewModel operationModel = null;
        private bool fastened = false;
        #endregion

        #region Command Properties

        public RelayCommand<FrameworkElement> VisualLoadedCommand { get; private set; }

        public RelayCommand VisualUpdatedCommand { get; private set; }

        public RelayCommand<MouseButtonEventArgs> DragStartedCommand { get; private set; }
        public RelayCommand<MouseEventArgs> DraggingCommand { get; private set; }
        public RelayCommand<MouseButtonEventArgs> DragCompletedCommand { get; private set; }
        #endregion

        #region Properties
        public FrameworkElement Visual
        {
            get { return visual; }
            set
            {
                visual = value; 
                RaisePropertyChanged(()=>Visual);
            }
        }

        public INetworkViewModel ParentNetworkView
        {
            get { return parentNetworkView; }
            set
            {
                parentNetworkView = value; 
                RaisePropertyChanged(()=>ParentNetworkView);
            }
        }

        public IEdgeViewModel LeftEdgeViewModel
        {
            get { return leftEdgeViewModel; }
            set
            {
                leftEdgeViewModel = value; 
                RaisePropertyChanged(()=>LeftEdgeViewModel);
            }
        }

        public IEdgeViewModel RightEdgeViewModel
        {
            get { return rightEdgeViewModel; }
            set
            {
                rightEdgeViewModel = value; 
                RaisePropertyChanged(()=>RightEdgeViewModel);
            }
        }

        public double X
        {
            get { return x; }
            set
            {
                x = value;
                RaisePropertyChanged(()=>X);
                ParentNetworkView.UpdateVisual();
            }
        }

        public double Y
        {
            get { return y; }
            set
            {
                y = value;
                RaisePropertyChanged(()=>Y);
                ParentNetworkView.UpdateVisual();
            }
        }

        public double Width
        {
            get { return width; }
            set
            {
                width = value; 
                RaisePropertyChanged(()=>Width);
            }
        }

        public double Height
        {
            get { return height; }
            set
            {
                height = value; 
                RaisePropertyChanged(()=>Height);
            }
        }

        public int ZIndex
        {
            get { return zIndex; }
            set
            {
                zIndex = value; 
                RaisePropertyChanged(()=>ZIndex);
            }
        }

        public Point LastMousePoint
        {
            get { return lastMousePoint; }
            set
            {
                lastMousePoint = value; 
                RaisePropertyChanged(()=>LastMousePoint);
            }
        }

        public bool IsLeftMouseDown
        {
            get { return isLeftMouseDown; }
            set
            {
                isLeftMouseDown = value; 
                RaisePropertyChanged(()=>IsLeftMouseDown);
            }
        }

        public bool IsLeftMouseAndControlDown
        {
            get { return isLeftMouseAndControlDown; }
            set
            {
                isLeftMouseAndControlDown = value; 
                RaisePropertyChanged(()=>IsLeftMouseAndControlDown);
            }
        }

        public bool IsDragging
        {
            get { return isDragging; }
            set
            {
                isDragging = value; 
                RaisePropertyChanged(()=>IsDragging);
            }
        }

        public bool Focusable
        {
            get { return focusable; }
            set
            {
                focusable = value; 
                RaisePropertyChanged(()=>Focusable);
            }
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value; 
                RaisePropertyChanged(()=>IsSelected);
            }
        }

        public bool IsFastened
        {
            get { return fastened; }
            set
            {
                fastened = value; 
                RaisePropertyChanged(()=>IsFastened);
            }
        }

        public Point LastPointWithinBounds
        {
            get { return lastPointWithinBounds; }
            set
            {
                lastPointWithinBounds = value; 
                RaisePropertyChanged(()=>LastPointWithinBounds);
            }
        }

        public IOperationViewModel OperationModel
        {
            get { return operationModel; }
            set
            {
                operationModel = value;
                RaisePropertyChanged(() => OperationModel);
            }
        }
        #endregion

        #region Constructors
        public NodeViewModel(INetworkViewModel _parentNetworkViewModel, IOperationViewModel _operationViewModel)
        {
            VisualLoadedCommand = new RelayCommand<FrameworkElement>(ExecuteVisualLoadedCommand);
            VisualUpdatedCommand = new RelayCommand(ExecuteVisualUpdatedCommand);
            DragStartedCommand = new RelayCommand<MouseButtonEventArgs>(ExecuteDragStartedCommand);
            DraggingCommand = new RelayCommand<MouseEventArgs>(ExecuteDraggingCommand);
            DragCompletedCommand = new RelayCommand<MouseButtonEventArgs>(ExecuteDragCompletedCommand);

            OperationModel = _operationViewModel;
            ParentNetworkView = _parentNetworkViewModel;
            LeftEdgeViewModel = new EdgeViewModel(this, ConnectorType.Input);
            RightEdgeViewModel = new EdgeViewModel(this,ConnectorType.Output);
        }      
        #endregion



        #region Methods

        public void ExecuteVisualLoadedCommand(FrameworkElement _obj)
        {
            Visual = _obj;
            Messenger.Default.Send(new NodeVisualLoaded());
        }

        private void ExecuteVisualUpdatedCommand()
        {
            //Width = Visual.ActualWidth;
        }

        public void ExecuteDragStartedCommand(MouseButtonEventArgs _e)
        {

            BringToFront();

            if (ParentNetworkView != null)
            {
                ParentNetworkView.Visual.Focus();
            }

            if (_e.ChangedButton == MouseButton.Left && ParentNetworkView != null)
            {
                LastMousePoint = _e.GetPosition(ParentNetworkView.Visual);
                IsLeftMouseDown = true;

                LeftMouseDownSelectionLogic();

                _e.Handled = true;
            }


        }

        public void ExecuteDraggingCommand(MouseEventArgs _e)
        {

            if (IsDragging)
            {
                //
                // Raise the event to notify that dragging is in progress.
                //

                var curMousePoint = _e.GetPosition(ParentNetworkView.Visual);


                var offset = curMousePoint - LastMousePoint;
                if (offset.X != 0.0 || offset.Y != 0.0)
                {
                    LastMousePoint = curMousePoint;
                    Messenger.Default.Send(new DragMessage(this,offset.X,offset.Y));
                }
            }
            else if (isLeftMouseDown && ParentNetworkView.EnableNodeDragging)
            {
                //
                // The user is left-dragging the node,
                // but don't initiate the drag operation until 
                // the mouse cursor has moved more than the threshold distance.
                //
                var curMousePoint = _e.GetPosition(ParentNetworkView.Visual);
                var dragDelta = curMousePoint - lastMousePoint;
                var dragDistance = Math.Abs(dragDelta.Length);
                if (dragDistance > dragThreshold)
                {
                    //
                    // When the mouse has been dragged more than the threshold value commence dragging the node.
                    //

                    //
                    // Raise an event to notify that that dragging has commenced.
                    //
                    var msg = new DragStartedMessage(this);
                    Messenger.Default.Send(msg);

                    if (msg.Cancel)
                    {
                        //
                        // Handler of the event disallowed dragging of the node.
                        //
                        IsLeftMouseDown = false;
                        IsLeftMouseAndControlDown = false;
                        return;
                    }

                    IsDragging = true;
                    Visual.CaptureMouse();
                    _e.Handled = true;
                }
            }

        }

        public void ExecuteDragCompletedCommand(MouseButtonEventArgs _e)
        {

            if (IsLeftMouseDown)
            {
                if (IsDragging)
                {
                    //
                    // Raise an event to notify that node dragging has finished.
                    //
                    Messenger.Default.Send(new DragCompletedMessage(this));
                    Visual.ReleaseMouseCapture();

                    IsDragging = false;
                }
                else
                {
                    //
                    // Execute mouse up selection logic only if there was no drag operation.
                    //

                    LeftMouseUpSelectionLogic();
                }

                IsLeftMouseDown = false;
                IsLeftMouseAndControlDown = false;

                //unconditionally unselect any node at end of drag event
                Messenger.Default.Send(new SelectedNodeChanged(null));

                _e.Handled = true;

            }
          
        }

        public void BringToFront()
        {
            if (ParentNetworkView == null)
            {
                return;
            }

            int maxZ = ParentNetworkView.FindMaxZIndex();
            ZIndex = maxZ + 1;
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
                IsLeftMouseAndControlDown = true;
            }
            else
            {
                //
                // Control key is not held down.
                //
                IsLeftMouseAndControlDown = false;

                if (ParentNetworkView.SelectedNode == this)
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
                    Messenger.Default.Send(new SelectedNodeChanged(this));
                }
            }
        }



        /// <summary>
        ///     This method contains selection logic that is invoked when the left mouse button is released.
        ///     The reason this exists in its own method rather than being included in OnMouseUp is
        ///     so that ConnectorItem can reuse this logic from its OnMouseUp.
        /// </summary>
        internal void LeftMouseUpSelectionLogic()
        {
            {
                {
                    //
                    // Clear the selection and select the clicked item as the only selected item.
                    //
                    Messenger.Default.Send(new SelectedNodeChanged(null));
                }
            }

            IsLeftMouseAndControlDown = false;
        }

        void Arrange()
        {

        }

        #endregion
    }
}