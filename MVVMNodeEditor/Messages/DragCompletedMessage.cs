namespace MVVMNodeEditor.Messages
{
    using Interfaces;

    public class DragCompletedMessage : MessageBase
    {
        #region Members
        public INodeViewModel Node { get; set; }
        #endregion

        #region Constructors

        public DragCompletedMessage(INodeViewModel _node)
        {
            Node = _node;
        }
        #endregion
    }
}
