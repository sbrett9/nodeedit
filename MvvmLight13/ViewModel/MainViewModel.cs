namespace MvvmLight13.ViewModel
{
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Input;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using Interfaces;
    using Utility;

    public class MainViewModel : ViewModelBase
    {
        #region Members
        private readonly IDataService _dataService;
        private NetworkViewModel network = new NetworkViewModel();
        ///
        /// The current scale at which the content is being viewed.
        /// 
        private double contentScale = 1;

        ///
        /// The X coordinate of the offset of the viewport onto the content (in content coordinates).
        /// 
        private double contentOffsetX = 0;

        ///
        /// The Y coordinate of the offset of the viewport onto the content (in content coordinates).
        /// 
        private double contentOffsetY = 0;

        ///
        /// The width of the content (in content coordinates).
        /// 
        private double contentWidth = 1600;

        ///
        /// The heigth of the content (in content coordinates).
        /// 
        private double contentHeight = 900;

        ///
        /// The width of the viewport onto the content (in content coordinates).
        /// The value for this is actually computed by the main window's ZoomAndPanControl and update in the
        /// view-model so that the value can be shared with the overview window.
        /// 
        private double contentViewportWidth = 0;

        ///
        /// The height of the viewport onto the content (in content coordinates).
        /// The value for this is actually computed by the main window's ZoomAndPanControl and update in the
        /// view-model so that the value can be shared with the overview window.
        /// 
        private double contentViewportHeight = 0;
        #endregion

        #region Command Properties
        public RelayCommand ConnectionDragStartedCommand { get; private set; }
        public RelayCommand QueryConnectionFeedbackCommand { get; private set; }
        public RelayCommand ConnectionDraggingCommand { get; private set; }
        public RelayCommand ConnectionDragCompletedCommand { get; private set; }
        public RelayCommand MouseDownCommand { get; private set; }
        public RelayCommand MouseUpCommand { get; private set; }
        public RelayCommand MouseMoveCommand { get; private set; }
        public RelayCommand MouseWheelCommand { get; private set; }

        #endregion

        #region Properties

        public NetworkViewModel Network
        {
            get { return network; }
            set
            {
                network = value; 
                RaisePropertyChanged(()=>Network);
            }
        }

        ///
        /// The current scale at which the content is being viewed.
        /// 
        public double ContentScale
        {
            get
            {
                return contentScale;
            }
            set
            {
                contentScale = value;

                RaisePropertyChanged("ContentScale");
            }
        }

        ///
        /// The X coordinate of the offset of the viewport onto the content (in content coordinates).
        /// 
        public double ContentOffsetX
        {
            get
            {
                return contentOffsetX;
            }
            set
            {
                contentOffsetX = value;

                RaisePropertyChanged("ContentOffsetX");
            }
        }

        ///
        /// The Y coordinate of the offset of the viewport onto the content (in content coordinates).
        /// 
        public double ContentOffsetY
        {
            get
            {
                return contentOffsetY;
            }
            set
            {
                contentOffsetY = value;

                RaisePropertyChanged("ContentOffsetY");
            }
        }

        ///
        /// The width of the content (in content coordinates).
        /// 
        public double ContentWidth
        {
            get
            {
                return contentWidth;
            }
            set
            {
                contentWidth = value;

                RaisePropertyChanged("ContentWidth");
            }
        }

        ///
        /// The heigth of the content (in content coordinates).
        /// 
        public double ContentHeight
        {
            get
            {
                return contentHeight;
            }
            set
            {
                contentHeight = value;

                RaisePropertyChanged("ContentHeight");
            }
        }

        ///
        /// The width of the viewport onto the content (in content coordinates).
        /// The value for this is actually computed by the main window's ZoomAndPanControl and update in the
        /// view-model so that the value can be shared with the overview window.
        /// 
        public double ContentViewportWidth
        {
            get
            {
                return contentViewportWidth;
            }
            set
            {
                contentViewportWidth = value;

                RaisePropertyChanged("ContentViewportWidth");
            }
        }

        ///
        /// The heigth of the viewport onto the content (in content coordinates).
        /// The value for this is actually computed by the main window's ZoomAndPanControl and update in the
        /// view-model so that the value can be shared with the overview window.
        /// 
        public double ContentViewportHeight
        {
            get
            {
                return contentViewportHeight;
            }
            set
            {
                contentViewportHeight = value;

                RaisePropertyChanged("ContentViewportHeight");
            }
        }

        #endregion


        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            ConnectionDragStartedCommand = new RelayCommand(ExecuteConnectionDragStarted);
            QueryConnectionFeedbackCommand = new RelayCommand(ExecuteQueryConnectionFeedbackCommand);
            ConnectionDraggingCommand = new RelayCommand(ExecuteConnectionDraggingCommand);
            ConnectionDragCompletedCommand = new RelayCommand(ExecuteConnectionDragCompletedCommand);
            MouseDownCommand = new RelayCommand(ExecuteMouseDownCommand);
            MouseUpCommand = new RelayCommand(ExecuteMouseUpCommand);
            MouseMoveCommand = new RelayCommand(ExecuteMouseMoveCommand);
            MouseWheelCommand = new RelayCommand(ExecuteMouseWheelCommand);

            
            for(int i = 0; i< dataService.Operations.Count;i++)
            {
                var t = dataService.Operations[i];
                var x = new NodeViewModel(t)
                {
                    Name = "kxfdsd",
                    X = 100 + (100*i),
                    Y=60 + (100*i)
                };
                
                Network.Nodes.Add(x);
            }
        }



        #endregion

        #region Methods
        private void ExecuteMouseWheelCommand()
        {
        }

        private void ExecuteMouseMoveCommand()
        {
        }

        private void ExecuteMouseUpCommand()
        {
        }

        private void ExecuteMouseDownCommand()
        {
        }

        private void ExecuteConnectionDragCompletedCommand()
        {
        }

        private void ExecuteConnectionDraggingCommand()
        {
        }

        private void ExecuteQueryConnectionFeedbackCommand()
        {
        }

        private void ExecuteConnectionDragStarted()
        {
        }
        #endregion
    }
}