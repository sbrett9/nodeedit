namespace MvvmLight13.Events.Insert.NodeInserting
{
    #region Using Declarations

    using System.Windows;

    #endregion

    public class NodeInsertingEventArgs : NodeInsertEventArgs
    {
        public NodeInsertingEventArgs(RoutedEvent _routedEvent, object _source, object _leftConnection, object _rightConnection) : base(_routedEvent, _source, _leftConnection, _rightConnection)
        {
        }
    }
}
