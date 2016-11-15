namespace MVVMNodeEditor.ViewModel.Operation
{
    #region Using Declarations

    using GalaSoft.MvvmLight;
    using Interfaces;

    #endregion

    public abstract class OperationViewModel : ViewModelBase,IOperationViewModel
    {
        private string key;
        private IOperation operation;
        protected abstract IOperation GetOperation();

        public string Key
        {
            get { return key; }
        }

        public string Name
        {
            get { return Operation.Name; }

        }

        public IOperation Operation { get; set; }

        protected OperationViewModel(IOperation _operation, string _key)
        {
            Operation = _operation;
            key = _key;
        }
    }
}
