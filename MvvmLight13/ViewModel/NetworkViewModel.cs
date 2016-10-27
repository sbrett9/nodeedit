namespace MvvmLight13.ViewModel
{
    using System;
    using Utility;
    using Utility.Utils;

    /// <summary>
    /// Defines a network of nodes and connections between the nodes.
    /// </summary>
    public class NetworkViewModel
    {
        #region Internal Data Members

        /// <summary>
        /// The collection of nodes in the network.
        /// </summary>
        private ImpObservableCollection<OperationViewModel> nodes = null;

        /// <summary>
        /// The collection of connections in the network.
        /// </summary>
        private ImpObservableCollection<ConnectionViewModel> connections = null;

        #endregion Internal Data Members

        /// <summary>
        /// The collection of nodes in the network.
        /// </summary>
        public ImpObservableCollection<OperationViewModel> Nodes
        {
            get
            {
                if (nodes == null)
                {
                    nodes = new ImpObservableCollection<OperationViewModel>();
                }

                return nodes;
            }
        }

        /// <summary>
        /// The collection of connections in the network.
        /// </summary>
        public ImpObservableCollection<ConnectionViewModel> Connections
        {
            get
            {
                if (connections == null)
                {
                    connections = new ImpObservableCollection<ConnectionViewModel>();
                    connections.ItemsRemoved += new EventHandler<CollectionItemsChangedEventArgs>(connections_ItemsRemoved);
                }

                return connections;
            }
        }

        #region Private Methods

        /// <summary>
        /// Event raised then Connections have been removed.
        /// </summary>
        private void connections_ItemsRemoved(object sender, CollectionItemsChangedEventArgs e)
        {
            foreach (ConnectionViewModel connection in e.Items)
            {
                connection.SourceConnector = null;
                connection.DestConnector = null;
            }
        }

        #endregion Private Methods
    }
}
