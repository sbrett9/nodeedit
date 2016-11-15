namespace MVVMNodeEditor.ViewModel.Operation
{
    #region Using Declarations

    using Interfaces;
    using Model;

    #endregion

    public class ZOperationViewModel : OperationViewModel
    {
        #region Members
        private ZOperation operation;
        #endregion

        #region Properties
        public new ZOperation Operation
        {
            get { return operation; }
            set
            {
                operation = (ZOperation)value;
                RaisePropertyChanged(() => Operation);
            }
        }
        #endregion

        #region Constructors

        public ZOperationViewModel(ZOperation _operation, string _key) : base(_operation, _key)
        {
            Operation = _operation;
        }
        #endregion


        #region Methods
        protected override IOperation GetOperation()
        {
            return Operation;
        }
        #endregion
    }
}