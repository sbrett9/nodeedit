namespace MVVMNodeEditor.Messages
{
    using System.Diagnostics;
    using Interfaces;

    public class DragMessage : MessageBase
    {
        #region Members

        private readonly INodeViewModel node = null;
        private readonly double deltaX = 0.0;
        private readonly double deltaY = 0.0;
        #endregion

        #region Properties
        public double DeltaX
        {
            get { return deltaX; }
        }

        public double DeltaY
        {
            get { return deltaY; }
        }

        public INodeViewModel Node
        {
            get { return node; }
        }
        #endregion

        #region Constructors

        public DragMessage(INodeViewModel _node, double x, double y)
        {
            node = _node;
            deltaX = x;
            deltaY = y;
            //Debug.WriteLine(string.Format("DragMessage Construction @ DX/DY ({0}, {0})", x.ToString(), y.ToString()));
        }
        #endregion

    }
}
