namespace Interfaces
{
    using System.Collections.ObjectModel;

    public interface IDataService
    {
        #region Properties

        ObservableCollection<IOperation> Operations { get; }

        #endregion


    }
}