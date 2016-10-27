﻿namespace MvvmLight13.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using GalaSoft.MvvmLight;
    using Model;
    using Utility;
    using Utility.Utils;

    /// <summary>
    ///     Defines a node in the view-model.
    ///     Nodes are connected to other nodes through attached connectors (aka anchor/connection points).
    /// </summary>
    public abstract class OperationViewModel : ViewModelBase
    {
        #region Events
        /// <summary>
        ///     Event raised when the size of the node is changed.
        ///     The size will change when the UI has determined its size based on the contents
        ///     of the nodes data-template.  It then pushes the size through to the view-model
        ///     and this 'SizeChanged' event occurs.
        /// </summary>
        public event EventHandler<EventArgs> SizeChanged;

        #endregion

        #region Private Data Members

        /// <summary>
        ///     The name of the node.
        /// </summary>
        private string name = string.Empty;

        /// <summary>
        ///     The X coordinate for the position of the node.
        /// </summary>
        private double x;

        /// <summary>
        ///     The Y coordinate for the position of the node.
        /// </summary>
        private double y;

        /// <summary>
        ///     The Z index of the node.
        /// </summary>
        private int zIndex;

        /// <summary>
        ///     The size of the node.
        ///     Important Note:
        ///     The size of a node in the UI is not determined by this property!!
        ///     Instead the size of a node in the UI is determined by the data-template for the Node class.
        ///     When the size is computed via the UI it is then pushed into the view-model
        ///     so that our application code has access to the size of a node.
        /// </summary>
        private Size size = Size.Empty;

        /// <summary>
        ///     List of input connectors (connections points) attached to the node.
        /// </summary>
        private ConnectorViewModel inputConnector = new ConnectorViewModel("input");

        /// <summary>
        ///     List of output connectors (connections points) attached to the node.
        /// </summary>
        private ConnectorViewModel outputConnector = new ConnectorViewModel("output");

        /// <summary>
        ///     Set to 'true' when the node is selected.
        /// </summary>
        private bool isSelected;

        #endregion Private Data Members



        #region Properties
        /// <summary>
        ///     The name of the node.
        /// </summary>
        public string Name
        {
            get { return name; }
            set
            {
                if (name == value)
                {
                    return;
                }

                name = value;

                RaisePropertyChanged("Name");
            }
        }

        /// <summary>
        ///     The X coordinate for the position of the node.
        /// </summary>
        public double X
        {
            get { return x; }
            set
            {
                if (x == value)
                {
                    return;
                }

                x = value;

                RaisePropertyChanged("X");
            }
        }

        /// <summary>
        ///     The Y coordinate for the position of the node.
        /// </summary>
        public double Y
        {
            get { return y; }
            set
            {
                if (y == value)
                {
                    return;
                }

                y = value;

                RaisePropertyChanged("Y");
            }
        }

        /// <summary>
        ///     The Z index of the node.
        /// </summary>
        public int ZIndex
        {
            get { return zIndex; }
            set
            {
                if (zIndex == value)
                {
                    return;
                }

                zIndex = value;

                RaisePropertyChanged("ZIndex");
            }
        }

        /// <summary>
        ///     The size of the node.
        ///     Important Note:
        ///     The size of a node in the UI is not determined by this property!!
        ///     Instead the size of a node in the UI is determined by the data-template for the Node class.
        ///     When the size is computed via the UI it is then pushed into the view-model
        ///     so that our application code has access to the size of a node.
        /// </summary>
        public Size Size
        {
            get { return size; }
            set
            {
                if (size == value)
                {
                    return;
                }

                size = value;

                if (SizeChanged != null)
                {
                    SizeChanged(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        ///     List of input connectors (connections points) attached to the node.
        /// </summary>
        public ConnectorViewModel InputConnector
        {
            get
            {
                return inputConnector;
            }
            set
            {
                inputConnector.ParentNode = null;
                inputConnector.Type = ConnectorType.Undefined;
                ConnectorViewModel t = value;
                if (t != null)
                {
                    t.ParentNode = this;
                    t.Type = ConnectorType.Input;
                }
                inputConnector = value;
            }
        }

        /// <summary>
        ///     List of output connectors (connections points) attached to the node.
        /// </summary>
        public ConnectorViewModel OutputConnector
        {
            get
            {
                return outputConnector;
            }
            set
            {
                outputConnector.ParentNode = null;
                outputConnector.Type = ConnectorType.Undefined;
                ConnectorViewModel t = value;
                if (t != null)
                {
                    t.ParentNode = this;
                    t.Type = ConnectorType.Output;
                }
                inputConnector = value;
            }
        }

        /// <summary>
        ///     A helper property that retrieves a list (a new list each time) of all connections attached to the node.
        /// </summary>
        public ICollection<ConnectionViewModel> AttachedConnections
        {
            get
            {
                var attachedConnections = new List<ConnectionViewModel>();
                if(inputConnector != null)
                    attachedConnections.Add(inputConnector.AttachedConnection);
                if(outputConnector != null)
                    attachedConnections.Add(outputConnector.AttachedConnection);

                return attachedConnections;
            }
        }

        /// <summary>
        ///     Set to 'true' when the node is selected.
        /// </summary>
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected == value)
                {
                    return;
                }

                isSelected = value;

                RaisePropertyChanged("IsSelected");
            }
        }
        #endregion



        #region Constructors
        public OperationViewModel()
        {
        }

        public OperationViewModel(string name)
        {
            this.name = name;
        }
        #endregion

        #region Private Methods

        ///// <summary>
        /////     Event raised when connectors are added to the node.
        ///// </summary>
        //private void inputConnectors_ItemsAdded(object sender, CollectionItemsChangedEventArgs e)
        //{
        //    foreach (ConnectorViewModel connector in e.Items)
        //    {
        //        connector.ParentNode = this;
        //        connector.Type = ConnectorType.Input;
        //    }
        //}

        ///// <summary>
        /////     Event raised when connectors are removed from the node.
        ///// </summary>
        //private void inputConnectors_ItemsRemoved(object sender, CollectionItemsChangedEventArgs e)
        //{
        //    foreach (ConnectorViewModel connector in e.Items)
        //    {
        //        connector.ParentNode = null;
        //        connector.Type = ConnectorType.Undefined;
        //    }
        //}

        ///// <summary>
        /////     Event raised when connectors are added to the node.
        ///// </summary>
        //private void outputConnectors_ItemsAdded(object sender, CollectionItemsChangedEventArgs e)
        //{
        //    foreach (ConnectorViewModel connector in e.Items)
        //    {
        //        connector.ParentNode = this;
        //        connector.Type = ConnectorType.Output;
        //    }
        //}

        ///// <summary>
        /////     Event raised when connectors are removed from the node.
        ///// </summary>
        //private void outputConnectors_ItemsRemoved(object sender, CollectionItemsChangedEventArgs e)
        //{
        //    foreach (ConnectorViewModel connector in e.Items)
        //    {
        //        connector.ParentNode = null;
        //        connector.Type = ConnectorType.Undefined;
        //    }
        //}

        #endregion Private Methods
    }
}