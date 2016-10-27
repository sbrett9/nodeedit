namespace MvvmLight13.Design
{
    #region Using Declarations

    using System.Collections.ObjectModel;
    using Interfaces;

    #endregion

    public class DesignTimeDataService : IDataService
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

        public DesignTimeDataService()
        {
            operations.Add(new DesignOperation());
            operations.Add(new DesignOperation());
        }
        #endregion
    }
}