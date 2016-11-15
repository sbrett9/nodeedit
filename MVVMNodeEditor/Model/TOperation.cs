namespace MVVMNodeEditor.Model
{
    #region Using Declarations

    using Interfaces;

    #endregion

    public class TOperation : IOperation
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
