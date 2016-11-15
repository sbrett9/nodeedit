using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MVVMNodeEditor.Model
{
    using Interfaces;
    public class YOperation : IOperation
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
