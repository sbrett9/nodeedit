namespace MvvmLight13.ViewModel
{
    using GalaSoft.MvvmLight;
    using Interfaces;

    public class NodeViewModel : NodeViewModelBase
    {
        #region Members

        private IOperation operation = null;
        #endregion

        #region Properties
        #endregion

        #region Constructors

        public NodeViewModel(IOperation op)
        {
            operation = op;
        }
        #endregion

        #region Methods
        #endregion
    }
}