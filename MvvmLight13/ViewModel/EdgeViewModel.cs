namespace MvvmLight13.ViewModel
{
    #region Using Declarations

    using System;
    using System.Windows;
    using GalaSoft.MvvmLight;
    using Interfaces;
    using Model;
    using Utility;

    #endregion
    /// <summary>
    /// Defines a connector (aka connection point) that can be attached to a node and is used to connect the node to another node.
    /// </summary>
    public class EdgeViewModel : ViewModelBase
    {
        #region Internal Data Members

        /// <summary>
        /// The connections that are attached to this connector, or null if no connections are attached.
        /// </summary>
        private NodeViewModelBase attachedNode = null;

        /// <summary>
        ///     The name of the edge.
        /// </summary>
        private string name = string.Empty;

        /// <summary>
        ///     The X coordinate for the position of the edge.
        /// </summary>
        private double x;

        /// <summary>
        ///     The Y coordinate for the position of the edge.
        /// </summary>
        private double y;

        /// <summary>
        ///     The Z index of the edge.
        /// </summary>
        private int zIndex;


        private double height = 0;
        private double width = 0;


        /// <summary>
        /// The hotspot (or center) of the connector.
        /// This is pushed through from ConnectorItem in the UI.
        /// </summary>
        private Point hotspot;
        private bool highlighted = false;

        #endregion Internal Data Members

        public EdgeViewModel(string name)
        {
            this.Name = name;
            this.Type = ConnectorType.Undefined;
        }

        /// <summary>
        /// The name of the connector.
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Defines the type of the connector.
        /// </summary>
        public ConnectorType Type
        {
            get;
            internal set;
        }

        /// <summary>
        /// Returns 'true' if the connector connected to another node.
        /// </summary>
        public bool IsConnected
        {
            get
            {
                var connection = AttachedNode;
                {
                    if (connection != null)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Returns 'true' if a connection is attached to the connector.
        /// The other end of the connection may or may not be attached to a node.
        /// </summary>
        public bool IsConnectionAttached
        {
            get
            {
                return AttachedNode != null;
            }
        }

        /// <summary>
        /// The connections that are attached to this connector, or null if no connections are attached.
        /// </summary>
        public NodeViewModelBase AttachedNode
        {
            get
            {
                return attachedNode;
            }
            set
            {
                attachedNode = value;
                RaisePropertyChanged(()=>IsConnectionAttached);
                RaisePropertyChanged(()=>IsConnected);
            }
        }

        /// <summary>
        /// The parent node that the connector is attached to, or null if the connector is not attached to any node.
        /// </summary>
        public NodeViewModelBase ParentNode
        {
            get;
            internal set;
        }

        /// <summary>
        /// The hotspot (or center) of the connector.
        /// This is pushed through from ConnectorItem in the UI.
        /// </summary>
        public Point Hotspot
        {
            get
            {
                return hotspot;
            }
            set
            {
                if (hotspot == value)
                {
                    return;
                }

                hotspot = value;

                OnHotspotUpdated();
            }
        }

        public bool Highlighted
        {
            get { return highlighted; }
            set
            {
                highlighted = value; 
                OnHighlightConnection();
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

        public double Width
        {
            get { return width; }
            set
            {
                width = value;
                RaisePropertyChanged(()=>Width);
            }
        }

        /// <summary>
        ///     The X coordinate for the position of the edge.
        /// </summary>
        public double X
        {
            get { return x; }
            set
            {
                x = value; 
                RaisePropertyChanged(()=>X);
            }
        }

        /// <summary>
        ///     The Y coordinate for the position of the edge.
        /// </summary>
        public double Y
        {
            get { return y; }
            set
            {
                y = value; 
                RaisePropertyChanged(()=>Y);
            }
        }

        /// <summary>
        ///     The Z index of the edge.
        /// </summary>
        public int ZIndex
        {
            get { return zIndex; }
            set
            {
                zIndex = value; 
                RaisePropertyChanged(()=>ZIndex);
            }
        }

        /// <summary>
        /// Event raised when the connector hotspot has been updated.
        /// </summary>
        public event EventHandler<EventArgs> HotspotUpdated;
        public event EventHandler<EventArgs> HighlightConnection;

        #region Private Methods


        /// <summary>
        /// Called when the connector hotspot has been updated.
        /// </summary>
        private void OnHotspotUpdated()
        {
            RaisePropertyChanged(()=>Hotspot);

            if (HotspotUpdated != null)
            {
                HotspotUpdated(this, EventArgs.Empty);
            }
        }

        #endregion Private Methods

        private void OnHighlightConnection()
        {
            RaisePropertyChanged(()=>Highlighted);
            if(HighlightConnection != null)
                HighlightConnection(this,EventArgs.Empty);
        }
    }

}
