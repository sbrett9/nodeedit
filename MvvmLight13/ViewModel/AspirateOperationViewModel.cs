namespace MvvmLight13.ViewModel
{
    using GalaSoft.MvvmLight;
    using Interfaces;

    public class AspirateOperationViewModel : OperationViewModel
    {
        #region Members

        private IOperation operation = null;
        #endregion

        #region Properties
        #endregion

        #region Constructors

        public AspirateOperationViewModel(IOperation op)
        {
            operation = op;
        }
        #endregion

        #region Methods
        #endregion
    }
}