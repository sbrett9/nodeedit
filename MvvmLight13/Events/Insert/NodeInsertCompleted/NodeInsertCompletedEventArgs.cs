namespace MvvmLight13.Events.Insert.NodeInsertCompleted
{
    #region Using Declarations

    using System.Windows;

    #endregion

    public class NodeInsertCompletedEventArgs : NodeInsertEventArgs
    {
        public NodeInsertCompletedEventArgs(RoutedEvent _routedEvent, object _source, object _leftConnection,
            object _rightConnection) : base(_routedEvent, _source, _leftConnection, _rightConnection)
        {
            
        }
    }
}
