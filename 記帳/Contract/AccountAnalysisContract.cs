using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 記帳.Models;

namespace 記帳.Contract
{
    internal class AccountAnalysisContract
    {
        public interface AccountAnalysisIView
        {
            void search_Response(List<AnalysisModel> list);
        }
        public interface AccountAnalysisIPresenter
        {
            void search_Events(DateTime dateTimePicker1,
               DateTime dateTimePicker2, List<string> typeFilter, List<string> targetFilter, List<string> payMethodFilter);


        }
    }
}
