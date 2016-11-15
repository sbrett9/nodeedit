namespace MVVMNodeEditor.ViewModel
{
    #region Using Declarations

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Windows.Controls;
    using Design;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Ioc;
    using Interfaces;
    using Microsoft.Practices.ServiceLocation;
    using Model;
    using Operation;

    #endregion

    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        //Attempting to find MethodInfo for method :  public void SimpleIoc::Register<TInterface, TClass>() where TInterface : class where TClass : class;
        private MethodInfo registerServiceMethod = typeof(SimpleIoc).GetMethods().Where(m => m.Name == "Register")
                                                   .Select(m => new { Method = m, Params = m.GetParameters(), Args = m.GetGenericArguments() })
                                                   .Where(x => x.Params.Length == 0 && x.Args.Length == 2)
                                                   .Select(x => x.Method)
                                                   .First();


        //Attempting to find MethodInfo for method : public void SimplIoc::Register<TClass>(Func<TClass> factory, string key) where TClass : class;
        private MethodInfo registerInstanceMethod = typeof(SimpleIoc).GetMethods().Where(m => m.Name == "Register")
                                                   .Select(m => new { Method = m, Params = m.GetParameters(), Args = m.GetGenericArguments() })
                                                   .Where(x => x.Params.Length == 3 && x.Params[1].ParameterType == typeof(string) && x.Params[2].ParameterType == typeof(bool))
                                                   .Select(x => x.Method)
                                                   .First();


        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);




            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Unregister<DesignTimeNetworkDataService>();
                SimpleIoc.Default.Register<DesignTimeNetworkDataService>(()=>new DesignTimeNetworkDataService());
                if (!SimpleIoc.Default.IsRegistered<INetworkDataService>())
                    SimpleIoc.Default.Register<INetworkDataService, DesignTimeNetworkDataService>();
            }
            else
            {
                SimpleIoc.Default.Register<RuntimeNetworkDataService>(()=>new RuntimeNetworkDataService(),true);
                if(!SimpleIoc.Default.IsRegistered<INetworkDataService>())
                    SimpleIoc.Default.Register<INetworkDataService,RuntimeNetworkDataService>();
            }

            INetworkDataService dataService = SimpleIoc.Default.GetInstance<INetworkDataService>();

            foreach (var t in dataService.ModelMap)
                IoCRegisterService(t.Key, t.Value);

            
            foreach (var op in dataService.Operations)
            {
                Guid guid = Guid.NewGuid();
                Type k = dataService.ModelMap[op.GetType()];
                IoCRegisterInstance(op,k,guid.ToString());
            }
            
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<NetworkViewModel>();


        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public NetworkViewModel Network
        {
            get
            {
                return ServiceLocator.Current.GetInstance<NetworkViewModel>();
            }
        }

        private void IoCRegisterService(Type _a, Type _b)
        {
            var reg = registerServiceMethod.MakeGenericMethod(_a, _b);
            reg.Invoke(SimpleIoc.Default, new object[] {});
        }

        /// <summary>
        /// Reflectively calls SimplIoC's Register<TClass>(Func<TClass> factory, string key) method.
        /// First thing however, is the construction of the first parameter of the function, a Func<T>
        /// As this is being done dynamically, since we want it to be generic for any type TClass, without having to
        /// specialize the method call at compile time, we must construct during runtime an instance of Func<TClass> templated
        /// to the right class. Since this Func acts as a type instance factory for the Registrer function, we must also construct,
        /// dynamically, body of the Func, which will vary depending on the Type _t parameter passed into the IoCRegisterInstance method.
        /// </summary>
        private void IoCRegisterInstance(IOperation _op, Type _t, string _key)
        {
            //Get templated MethodInfo for SimplIoC's Register<TClass>(Func<TClass> factory, string key) method
            //TClass is now whatever the type supplied by variable _t is.
            var registerMethod = registerInstanceMethod.MakeGenericMethod(_t);
            
            //The types of the parameters for the constructor of the instance of type saved in variable _t.
            //All types saved in variable _t will at least be of an OperationViewModel base.
            Type[] types = new Type[] {_op.GetType(), _key.GetType()};
            //Get the constructor info for the OperationViewModel derivative that takes some subclass of IOperation, and a String.
            var constructorInfo = _t.GetConstructor(types);
            //Bind the data at variable _op into a lambda expression parameter
            var param1 = Expression.Constant(_op);
            //Bind the data at variable _key into a lambda expression parameter
            var param2 = Expression.Constant(_key);

            //Create a linq expression that calls a parameterized constructor. IE   {new KekOperationViewModel(_op,_key); }
            var constructionExpression = Expression.New(constructorInfo, new List<Expression>() {param1,param2});

            //Create a lambda expression, ie ()=>new KekOperationViewModel(_op,_key);
            var inner = Expression.Lambda(constructionExpression, new List<ParameterExpression>() {});

            //Create the invocation expression ie Invoke(()=>new KekOperationViewModel(_op,_key);)
            var body = Expression.Invoke(inner,new List<Expression>() {});
            
            //Create some specialized generic types, needed for calling generic methods reflectively.
            Type funcType = typeof(Func<>);
            Type templatedFuncType = funcType.MakeGenericType(_t);
            Type expressionType = typeof(Expression<>);
            Type templatedExpressionType = expressionType.MakeGenericType(templatedFuncType);

            //Find MethodInfo for static Expression<TDelegate> System.Linq.Expressions.Expression::Lambda<TDelegate>(Expression body, IEnumerable<ParameterExpression> parameters);
            var genericLambdaFunction = typeof(Expression).GetMethods().Where(m => m.Name == "Lambda")
                               .Select(m => new { Method = m, Params = m.GetParameters(), Args = m.GetGenericArguments() })
                                .Where(x => x.Params.Length == 2 && x.Params[0].ParameterType == typeof(Expression) && x.Params[1].ParameterType == typeof(IEnumerable<ParameterExpression>) && x.Args.Length == 1)
                                .Select(x => x.Method)
                                .First();

            //Specialize the function template with the specialized Func<> type. IE Lambda<Func<KekOperationViewModel> >(Expression body, IEnumerable<ParameterExpression> parameters);
            MethodInfo templatedLambdaFunction = genericLambdaFunction.MakeGenericMethod(templatedFuncType);

            //Call the now fully specialized generic method, passing in no parameters. This returns an instance of a Expression<Func<KekOperationViewModel> >, assuming the templated class type from previous examples
            var  constructedLambdaFunction = templatedLambdaFunction.Invoke(null, new object[] {body,new List<ParameterExpression>() { }});


            //Find the method info for the Expression.Compile function.
            var compileFunc = templatedExpressionType.GetMethods().Where(m => m.Name == "Compile")
                               .Select(m => new { Method = m, Params = m.GetParameters(), Args = m.GetGenericArguments() })
                                .Where(x => x.Params.Length == 0 && x.Args.Length == 0)
                                .Select(x => x.Method)
                                .First();

            //Call the compile function on the instance of Expression<Func<KekOperationViewModel> >, turning the expression into an actual instance of Func<KekOperationViewModel> (again following prior examples). 
            //This completes the variable scope closure needed for lambda functions and renders the resultant Func<KekOperationViewModel> instance usable.
            var realizedLambdaFunction  = compileFunc.Invoke(constructedLambdaFunction, new object[] {});

            //Call the SimpleIoc.Register method supplying the factory lambda function and the GUID key.
            registerMethod.Invoke(SimpleIoc.Default, new object[] { realizedLambdaFunction, _key, true});

            //A reservation  of the type stored in parameter _t has been made in the IoC container, accessible by the string stored in the _key variable.
            //The actual instance of the view model will be constructed immediately.
            //Once done, that instance at that key will remain until unregistred, and be returned by subsequent calls of GetInstance supplying the same key.
        }


        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}