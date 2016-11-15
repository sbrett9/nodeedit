namespace MVVMNodeEditor.Design
{
    #region Using Declarations

    using System;
    using System.Collections.Generic;
    using Interfaces;
    using Model;
    using ViewModel.Operation;

    #endregion

    public class DesignTimeNetworkDataService : INetworkDataService
    {
        private readonly List<IOperation> operations = new List<IOperation>();
        private readonly Dictionary<Type,Type> modelMap = new Dictionary<Type, Type>();

        public IEnumerable<IOperation> Operations
        {
            get { return operations; }
        }

        public Dictionary<Type, Type> ModelMap
        {
            get { return modelMap; }
        }

        public void RegisterOperationViewModel(Type operationType, Type viewModelType)
        {
            Type x;
            bool found = modelMap.TryGetValue(operationType, out x);
            if (found)
            {
                throw new InvalidOperationException(string.Format("Operation Type {0} already has a registered View Model: {1}", operationType.FullName, x.FullName));
            }
            else
            {
                modelMap.Add(operationType,viewModelType);
            }

        }

        public Type GetModelTypeForOperation(IOperation _operation)
        {
            return GetModelTypeForOperationType(_operation.GetType());
        }

        public Type GetModelTypeForOperationType(Type _operationType)
        {
            Type x;
            bool found = modelMap.TryGetValue(_operationType, out x);
            if (found)
                return x;
            else return null;
        }

        public DesignTimeNetworkDataService()
        {
            RegisterOperationViewModel(typeof(QOperation), typeof(QOperationViewModel));
            RegisterOperationViewModel(typeof(TOperation), typeof(TOperationViewModel));
            RegisterOperationViewModel(typeof(YOperation), typeof(YOperationViewModel));
            RegisterOperationViewModel(typeof(ZOperation), typeof(ZOperationViewModel));

            operations.Add(new QOperation());
            operations.Add(new TOperation());
            operations.Add(new TOperation());
            operations.Add(new YOperation());
            operations.Add(new ZOperation());
            operations.Add(new QOperation());
        }
    }
}
