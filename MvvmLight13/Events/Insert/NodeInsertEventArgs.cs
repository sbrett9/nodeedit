namespace MvvmLight13.Events.Insert
{
    #region Using Declarations

    using System.Windows;

    #endregion

    public class NodeInsertEventArgs : RoutedEventArgs
    {
        #region Members

        object leftConnection = null;
        object rightConnection = null;

        #endregion

        #region Properties
        public object LeftConnection
        {
            get { return leftConnection; }
            set { leftConnection = value; }
        }

        public object RightConnection
        {
            get { return rightConnection; }
            set { rightConnection = value; }
        }
        #endregion

        #region Constructors

        public NodeInsertEventArgs(RoutedEvent _routedEvent, object _source, object _leftConnection,
            object _rightConnection) : base(_routedEvent, _source)
        {
            leftConnection = _leftConnection;
            rightConnection = _rightConnection;
        }
        #endregion

        #region Methods

        #endregion
    }
}
