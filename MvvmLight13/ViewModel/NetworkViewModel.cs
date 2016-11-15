namespace MvvmLight13.ViewModel
{
    using System;
    using Interfaces;
    using Interfaces.Utils;
    using Utility;

    /// <summary>
    /// Defines a network of nodes and connections between the nodes.
    /// </summary>
    public class NetworkViewModel
    {
        #region Internal Data Members

        /// <summary>
        /// The collection of nodes in the network.
        /// </summary>
        private ImpObservableCollection<NodeViewModelBase> nodes = null;

        /// <summary>
        /// The collection of connections in the network.
        /// </summary>
        private ImpObservableCollection<EdgeViewModel> edges = null;

        #endregion Internal Data Members

        /// <summary>
        /// The collection of nodes in the network.
        /// </summary>
        public ImpObservableCollection<NodeViewModelBase> Nodes
        {
            get
            {
                if (nodes == null)
                {
                    nodes = new ImpObservableCollection<NodeViewModelBase>();
                }

                return nodes;
            }
        }

        /// <summary>
        /// The collection of connections in the network.
        /// </summary>
        public ImpObservableCollection<EdgeViewModel> Edges
        {
            get
            {
                if (edges == null)
                {
                    edges = new ImpObservableCollection<EdgeViewModel>();
                    edges.ItemsRemoved += new EventHandler<CollectionItemsChangedEventArgs>(connections_ItemsRemoved);
                }

                return edges;
            }
        }

        #region Private Methods

        /// <summary>
        /// Event raised then Connections have been removed.
        /// </summary>
        private void connections_ItemsRemoved(object sender, CollectionItemsChangedEventArgs e)
        {
            foreach (EdgeViewModel connection in e.Items)
            {
                connection.AttachedNode = null;
            }
        }

        #endregion Private Methods
    }
}
