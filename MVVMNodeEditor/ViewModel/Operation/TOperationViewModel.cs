namespace MVVMNodeEditor.ViewModel.Operation
{
    #region Using Declarations

    using Interfaces;
    using Model;

    #endregion

    public class TOperationViewModel : OperationViewModel
    {
        #region Members
        private TOperation operation;
        #endregion

        #region Properties
        public new TOperation Operation
        {
            get { return operation; }
            set
            {
                operation = (TOperation)value;
                RaisePropertyChanged(() => Operation);
            }
        }
        #endregion

        #region Constructors

        public TOperationViewModel(TOperation _operation, string _key) : base(_operation, _key)
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