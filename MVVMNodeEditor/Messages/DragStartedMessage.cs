namespace MVVMNodeEditor.Messages
{
    using Interfaces;

    public class DragStartedMessage : MessageBase
    {
        #region Members
        public INodeViewModel Node { get; set; }
        public bool Cancel { get; set; }
        #endregion

        #region Constructors

        public DragStartedMessage(INodeViewModel _node)
        {
            Node = _node;
        }
        #endregion
    }
}
