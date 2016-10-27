

namespace MvvmLight13.ViewModel
{
    using Design;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Ioc;
    using Interfaces;
    using Microsoft.Practices.ServiceLocation;
    using Model;

    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Unregister<DesignTimeDataService>();
                SimpleIoc.Default.Register<DesignTimeDataService>(() => new DesignTimeDataService());
                if (!SimpleIoc.Default.IsRegistered<IDataService>())
                    SimpleIoc.Default.Register<IDataService, DesignTimeDataService>();
            }
            else
            {
                SimpleIoc.Default.Register<RuntimeDataService>(()=> new RuntimeDataService());
                if (!SimpleIoc.Default.IsRegistered<IDataService>())
                    SimpleIoc.Default.Register<IDataService, RuntimeDataService>();
            }

            SimpleIoc.Default.Register<MainViewModel>();
        }

        public MainViewModel Main
        {
            get { return ServiceLocator.Current.GetInstance<MainViewModel>(); }
        }

        /// <summary>
        ///     Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}