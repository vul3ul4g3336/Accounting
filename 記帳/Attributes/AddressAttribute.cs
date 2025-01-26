using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 記帳.Attributes
{
    public class AddressAttribute : Attribute
    {
        public int Index = 0;
        public AddressAttribute(int index)
        {
            this.Index = index;
        }
    }
}
