using CSV_Library;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 記帳.ExtensionFile;
using 記帳.Models;
using 記帳.Repositary;
using static 記帳.Contract.AccountAnalysisContract;

namespace 記帳.Presenter
{
    internal class AccountAnalysisPresenter : AccountAnalysisIPresenter
    {
        private AccountAnalysisIView _view;
        private Form chartForm;
        IRepository repository = new CSV_Repository();
        public AccountAnalysisPresenter(AccountAnalysisIView view)
        {
            _view = view;
        }


        public void search_Events(DateTime dateTime1, DateTime dateTime2, List<string> typeFilter, List<string> targetFilter, List<string> payMethodFilter)
        {

            var datas = repository.GetClassificationData
                (dateTime1, dateTime2, typeFilter, targetFilter, payMethodFilter);
            _view.search_Response(datas);
        }
    }
}
