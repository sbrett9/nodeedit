namespace MVVMNodeEditor.ViewModel.Operation
{
    #region Using Declarations

    using GalaSoft.MvvmLight;
    using Interfaces;
    using Model;

    #endregion

    public class QOperationViewModel : OperationViewModel
    {
        #region Members
        private QOperation operation;
        #endregion

        #region Properties
        public new QOperation Operation
        {
            get { return operation; }
            set
            {
                operation = (QOperation)value;
                RaisePropertyChanged(() => Operation);
            }
        }
        #endregion

        #region Constructors

        public QOperationViewModel(QOperation _operation, string _key) :base(_operation,_key)
        {
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