namespace MVVMNodeEditor.Model
{
    using Interfaces;
    public class ZOperation : IOperation
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
