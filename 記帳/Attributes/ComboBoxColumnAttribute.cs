using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 記帳.Attributes
{
    public class ComboBoxColumnAttribute : Attribute
    {
        public string _source;
        public ComboBoxColumnAttribute(string source)
        {
            this._source = source;
        }

        public ComboBoxColumnAttribute(List<string> DataSource)
        {

        }
    }
}
