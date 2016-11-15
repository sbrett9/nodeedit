namespace MVVMNodeEditor.Model
{
    #region Using Declarations

    using Interfaces;

    #endregion

    public abstract class Operation : IOperation
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
