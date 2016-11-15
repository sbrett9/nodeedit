namespace MVVMNodeEditor.ViewModel
{
    #region Using Declarations

    using System;
    using System.Windows;
    using System.Windows.Input;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using Interfaces;

    #endregion

    public class EdgeViewModel : ViewModelBase,IEdgeViewModel
    {

        #region Events
        public event EventHandler<EventArgs> SizeChanged;



        #endregion

        #region Members
        private string name;
        private INodeViewModel parentNode;
        private INodeViewModel targetNode;
        private double x;
        private double y;
        private double width;
        private double height;
        private int zIndex;
        private bool highlighted = false;
        private ConnectorType connectorType;
        private bool isConnected;
        private FrameworkElement visual;

        #endregion

        #region Command Properties

        #endregion

        #region Properties
        public string Name
        {
            get { return name; }
            set
            {
                name = value; 
                RaisePropertyChanged(()=>Name);
            }
        }

        public INetworkViewModel ParentNetworkView
        {
            get
            {
                return parentNode.ParentNetworkView; 
                
            }
        }

        public INodeViewModel ParentNode
        {
            get { return parentNode; }
            private set
            {
                parentNode = value; 
                RaisePropertyChanged(()=>ParentNode);
            }
        }

        public INodeViewModel TargetNode
        {
            get { return targetNode; }
            set
            {
                targetNode = value; 
                RaisePropertyChanged(()=>TargetNode);
                RaisePropertyChanged(()=>IsConnected);
            }
        }

        public double X
        {
            get { return x; }
            set
            {
                x = value; 
                RaisePropertyChanged(()=>X);
            }
        }

        public double Y
        {
            get { return y; }
            set
            {
                y = value; 
                RaisePropertyChanged(()=>Y);
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

        public bool Highlighted
        {
            get { return highlighted; }
            set
            {
                highlighted = value; 
                RaisePropertyChanged(()=>Highlighted);
            }
        }

        public ConnectorType ConnectorType
        {
            get { return connectorType; }
            set
            {
                connectorType = value; 
                RaisePropertyChanged(()=>ConnectorType);
            }
        }

        public bool IsConnected
        {
            get { return targetNode != null; }
        }

        public RelayCommand<FrameworkElement> VisualLoadedCommand { get; private set; }

        public FrameworkElement Visual
        {
            get { return visual; }
            set
            {
                visual = value;
                RaisePropertyChanged(() => Visual);
            }
        }

        #endregion

        #region Constructors

        public EdgeViewModel(INodeViewModel _parentNode, ConnectorType _connectorType)
        {
            VisualLoadedCommand = new RelayCommand<FrameworkElement>(ExecuteVisualLoadedCommand);
            ParentNode = _parentNode;
            ConnectorType = _connectorType;
        }
        #endregion


        #region Methods
        public void ExecuteVisualLoadedCommand(FrameworkElement _obj)
        {
            Visual = _obj;
            Point relativeLocation = Visual.TranslatePoint(new Point(0, 0), ParentNetworkView.Visual);
            X = relativeLocation.X;
            Y = relativeLocation.Y;
        }


        #endregion
    }
}
