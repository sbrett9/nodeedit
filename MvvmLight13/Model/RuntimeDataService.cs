namespace MvvmLight13.Model
{
    #region Using Declarations

    using System.Collections.ObjectModel;
    using Interfaces;

    #endregion

    public class RuntimeDataService : IDataService
    {
        #region Members
        private ObservableCollection<IOperation> operations = new ObservableCollection<IOperation>();
        #endregion

        #region Properties
        public ObservableCollection<IOperation> Operations
        {
            get { return operations; }
        }
        #endregion

        #region Constructors

        public RuntimeDataService()
        {
            operations.Add(new Operation());
            operations.Add(new Operation());
        }
        #endregion


    }
}
