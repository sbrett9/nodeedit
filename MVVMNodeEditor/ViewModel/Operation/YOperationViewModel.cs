namespace MVVMNodeEditor.ViewModel.Operation
{
    #region Using Declarations

    using GalaSoft.MvvmLight;
    using Interfaces;
    using Model;

    #endregion

    public class YOperationViewModel : OperationViewModel
    {
        #region Members
        private YOperation operation;
        #endregion

        #region Properties
        public new YOperation Operation
        {
            get { return operation; }
            set
            {
                operation = (YOperation)value;
                RaisePropertyChanged(() => Operation);
            }
        }
        #endregion

        #region Constructors

        public YOperationViewModel(YOperation _operation, string _key) : base(_operation, _key)
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