namespace MVVMNodeEditor.ViewModel
{
    #region Using Declarations

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Ioc;
    using GalaSoft.MvvmLight.Messaging;
    using Interfaces;
    using Messages;
    using Utility;

    #endregion

    public class NetworkViewModel : ViewModelBase, INetworkViewModel
    {
        #region Events
        public event SelectionChangedEventHandler SelectionChanged;
        #endregion

        #region Members
        private ImpObservableCollection<INodeViewModel> nodes = new ImpObservableCollection<INodeViewModel>();
        private ImpObservableCollection<IEdgeViewModel> edges = new ImpObservableCollection<IEdgeViewModel>();
        private bool enableNodeDragging = true;
        private bool isDragging;
        private ImpObservableCollection<INodeViewModel> selectedNodes = new ImpObservableCollection<INodeViewModel>();
        private INodeViewModel selectedNode = null;
        private FrameworkElement visual;
        private bool restrictNodesToVisual = true;

        #endregion

        #region Command Properties

        public RelayCommand ArrangeCommand { get; private set; }
        public RelayCommand ScatterCommand { get; private set; }
        public RelayCommand<FrameworkElement> VisualLoadedCommand { get; private set; }

        #endregion

        #region Properties


        public FrameworkElement Visual
        {
            get { return visual; }
            set
            {
                visual = value;
                RaisePropertyChanged(() => Visual);
            }
        }
        public ObservableCollection<INodeViewModel> Nodes
        {
            get { return nodes; }
        }

        public ObservableCollection<IEdgeViewModel> Edges
        {
            get { return edges; }
        }

        public INodeViewModel SelectedNode
        {
            get { return selectedNode; }
            set
            {
                if (selectedNode != null)
                    selectedNode.IsSelected = false;
                selectedNode = value;
                RaisePropertyChanged(() => SelectedNode);
                if (selectedNode != null)
                    selectedNode.IsSelected = true;

            }
        }

        public bool EnableNodeDragging
        {
            get { return enableNodeDragging; }
            set
            {
                enableNodeDragging = value;
                RaisePropertyChanged(() => EnableNodeDragging);
            }
        }

        public bool IsDragging
        {
            get { return isDragging; }
            set
            {
                isDragging = value;
                RaisePropertyChanged(() => IsDragging);
            }
        }



        #endregion

        #region Constructors
        public NetworkViewModel(INetworkDataService _networkData)
        {
            ArrangeCommand = new RelayCommand(ExecuteArrangeCommand);
            ScatterCommand = new RelayCommand(ExecuteScatterCommand);

            VisualLoadedCommand = new RelayCommand<FrameworkElement>(ExecuteVisualLoadedCommand);

            Messenger.Default.Register<DragStartedMessage>(this, DragStarted);
            Messenger.Default.Register<DragMessage>(this, Drag);
            Messenger.Default.Register<DragCompletedMessage>(this, DragCompleted);
            Messenger.Default.Register<SelectedNodeChanged>(this, NodeChanged);
            Messenger.Default.Register<NodeVisualLoaded>(this,NodeVisualLoaded);

            int z = 1;
            List<IOperationViewModel> tmp = new List<IOperationViewModel>();
            foreach (var modelType in _networkData.ModelMap.Values)
            {
                var instances = SimpleIoc.Default.GetAllInstances(modelType).Cast<IOperationViewModel>().ToList();
                tmp.AddRange(instances);
            }
            foreach (var operation in _networkData.Operations)
            {
                var operationModel = tmp.First(x => x.Operation == operation);
                NodeViewModel nodeViewModel = new NodeViewModel(this, (IOperationViewModel)operationModel);
                //nodeViewModel.X += 10 * z;
                //nodeViewModel.Y += 10 * z;
                Nodes.Add(nodeViewModel);

                Edges.Add(nodeViewModel.LeftEdgeViewModel);
                Edges.Add(nodeViewModel.RightEdgeViewModel);
                z++;
            }
        }
        #endregion

        #region Methods


        private void Drag(DragMessage _action)
        {
            // Update the position of the node within the Canvas.                      
            if (SelectedNode == _action.Node)
            {
                Vector displacement = new Vector(_action.DeltaX, _action.DeltaY);
                //check for clipping
                bool willClip = Utilities.WillClip(_action.Node.Visual, displacement, Visual);
                //if (!willClip)
                {
                    Debug.WriteLine(string.Format("@{0},{1}", _action.Node.X, _action.Node.Y));
                    //Translate the selected node
                    _action.Node.X += _action.DeltaX;
                    _action.Node.Y += _action.DeltaY;



                    //Update the edge positions as well, allowing easier edge collision detection
                    _action.Node.LeftEdgeViewModel.X = _action.DeltaX;
                    _action.Node.LeftEdgeViewModel.Y = _action.DeltaY;

                    _action.Node.RightEdgeViewModel.X += _action.DeltaX;
                    _action.Node.RightEdgeViewModel.Y += _action.DeltaY;

                }
                //else
                {
                    //If the vector magnitude will take the node outside the network visual
                    //set the position of the node to the edge of the visual, and not beyond.


                }
            }
        }

        private void DragStarted(DragStartedMessage _action)
        {
            IsDragging = true;
            Mouse.OverrideCursor = Cursors.Hand;
        }

        private void DragCompleted(DragCompletedMessage _action)
        {
            IsDragging = false;
            Mouse.OverrideCursor = Cursors.Arrow;

            //ArrangeNodes();

        }

        private void NodeVisualLoaded(NodeVisualLoaded _nodeLoaded)
        {
            //ArrangeNodes();
        }

        public void ArrangeNodes()
        {
            if (Nodes.All(x => x.Visual != null))
            {
                var loadedNodes = nodes.Where(x => x.Visual != null).ToList();
                //check if nodes have uniform height
                bool uniformNodeHeights = loadedNodes.Select(x => x.Height).Distinct().Count() == 1;
                var visuals = loadedNodes.Select(x => x.Visual).ToList();
                bool uniformVisualHeights = visuals.Select(x => x.Height).Distinct().Count() == 1;

                double tallestNodeHeight = loadedNodes.Max(x => x.Height);
                INodeViewModel tallestNode = loadedNodes.First(x => x.Height == tallestNodeHeight);
                double tallestVisualHeight = visuals.Max(x => x.ActualHeight);
                var tallestVisual = visuals.First(x => x.ActualHeight == tallestVisualHeight);
                var totalHeight = tallestNodeHeight/2 + tallestVisualHeight;
                double xPos = 0;
                foreach (var node in loadedNodes)
                {
                    node.X = xPos;
                    if (!uniformNodeHeights && !uniformVisualHeights)
                    {
                        node.Y = tallestNodeHeight/2 - node.Height/2;
                    }
                    else if (!uniformVisualHeights)
                    {
                        if (node.Visual == tallestVisual)
                        {
                            node.Y = 0;
                        }
                        else
                        {
                            node.Y = 0;
                        }                        
                    }
                    else if (!uniformNodeHeights)
                    {
                    }
                    else
                        node.Y = 0;

                    xPos = node.RightEdgeViewModel.X + node.RightEdgeViewModel.Width;
                }
            }
            UpdateVisual();
        }

        private void NodeChanged(SelectedNodeChanged _obj)
        {
            SelectedNode = _obj.Node;
        }

        private void ExecuteVisualLoadedCommand(FrameworkElement _obj)
        {
            Visual = _obj;
//            ArrangeNodes();
        }

        /// <summary>
        ///     Find the max ZIndex of all the nodes.
        /// </summary>
        public int FindMaxZIndex()
        {
            int maxZ = 0;

            foreach (var nodeViewModel in Nodes)
            {
                if (nodeViewModel.ZIndex > maxZ)
                {
                    maxZ = nodeViewModel.ZIndex;
                }
            }
            return maxZ;
        }

        public bool RestrictNodesToVisual
        {
            get { return restrictNodesToVisual; }
            set
            {
                restrictNodesToVisual = value;
                RaisePropertyChanged(() => RestrictNodesToVisual);
            }
        }

        public void UpdateVisual()
        {
            if (Visual != null)
            {
                Visual.InvalidateMeasure();
                Visual.InvalidateArrange();
                Visual.InvalidateVisual();
            }

        }

        private void ExecuteScatterCommand()
        {
            Random rnd = new Random();
            foreach (var t in Nodes)
            {
                t.X = rnd.Next(20, 1000);
                t.Y = rnd.Next(20, 800);
            }
        }

        private void ExecuteArrangeCommand()
        {
            Point nextPoint = new Point(100, 100);
            foreach (var t in Nodes)
            {
                Size k = new Size(t.Visual.ActualWidth, t.Visual.ActualHeight);
                t.X = nextPoint.X;
                t.Y = nextPoint.Y;
                nextPoint = new Point(nextPoint.X + k.Width, nextPoint.Y);
            }
        }
        #endregion
    }
}