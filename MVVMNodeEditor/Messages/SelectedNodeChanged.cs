namespace MVVMNodeEditor.Messages
{
    using Interfaces;

    public class SelectedNodeChanged : MessageBase
    {
        private readonly INodeViewModel node = null;

        public INodeViewModel Node
        {
            get { return node; }
        }

        public SelectedNodeChanged(INodeViewModel _node)
        {
            node = _node;
        }


    }
}
