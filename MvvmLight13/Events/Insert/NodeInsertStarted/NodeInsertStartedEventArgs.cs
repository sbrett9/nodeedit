namespace MvvmLight13.Events.Insert.NodeInsertStarted
{
    using System.Windows;
    using Insert;

    public class NodeInsertStartedEventArgs : NodeInsertEventArgs
    {
        #region Constructors

        public NodeInsertStartedEventArgs(RoutedEvent _routedEvent, object _source, object _leftConnection,
            object _rightConnection) : base(_routedEvent, _source, _leftConnection, _rightConnection)
        {

        }
        #endregion

    }
}