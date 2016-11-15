namespace MVVMNodeEditor.Model
{
    #region Using Declarations

    using System;
    using System.Collections.Generic;
    using Interfaces;
    using ViewModel.Operation;

    #endregion

    public class RuntimeNetworkDataService : INetworkDataService
    {
        private readonly List<IOperation> operations = new List<IOperation>();
        private readonly Dictionary<Type, Type> modelMap = new Dictionary<Type, Type>();

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
                modelMap.Add(operationType, viewModelType);
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


        public RuntimeNetworkDataService()
        {

            RegisterOperationViewModel(typeof(QOperation), typeof(QOperationViewModel));
            RegisterOperationViewModel(typeof(TOperation), typeof(TOperationViewModel));
            RegisterOperationViewModel(typeof(YOperation), typeof(YOperationViewModel));
            RegisterOperationViewModel(typeof(ZOperation), typeof(ZOperationViewModel));

            operations.Add(new QOperation() { Name = "1" });
            operations.Add(new TOperation() { Name = "2" });
            operations.Add(new TOperation() { Name = "3" });
            operations.Add(new YOperation() { Name = "4" });
            operations.Add(new ZOperation() { Name = "5" });
            operations.Add(new QOperation() { Name = "6" });

        }
    }
}
