namespace MVVMNodeEditor.ViewModel
{
    #region Using Declarations

    using System.Windows;
    using System.Windows.Input;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;

    #endregion

    public class MainViewModel : ViewModelBase
    {

        #region Members

        private double contentHeight = 900;
        private double contentWidth = 1600;
        #endregion

        #region Properties


        public double ContentHeight
        {
            get { return contentHeight; }
            set
            {
                contentHeight = value; 
                RaisePropertyChanged(()=>ContentHeight);
            }
        }

        public double ContentWidth
        {
            get { return contentWidth; }
            set
            {
                contentWidth = value; 
                RaisePropertyChanged(()=>ContentWidth);
            }
        }
        #endregion

        #region Constructors
        public MainViewModel()
        {

        }



        #endregion

        #region Methods
        #endregion


    }
}