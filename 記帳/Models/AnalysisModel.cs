using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 記帳.Models
{
    public class AnalysisModel
    {
        public string Type { get; set; }
        public string Target { get; set; }
        public string PayMethod { get; set; }

        public string Price { get; set; }
    }
}
