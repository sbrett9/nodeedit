namespace Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface INetworkDataService
    {
        IEnumerable<IOperation> Operations { get; }
        Dictionary<Type,Type> ModelMap { get; }
        void RegisterOperationViewModel(Type operationType, Type viewModelType);
        Type GetModelTypeForOperation(IOperation _operation);
        Type GetModelTypeForOperationType(Type _operationType);
    }
}
