using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 記帳.Models;

namespace 記帳.Repositary
{
    public interface IRepository
    {

        List<RecordModel> GetRawData(DateTime dateTime1, DateTime dateTime2);

        List<AnalysisModel> GetClassificationData(DateTime dateTime1, DateTime dateTime2,
            List<string> typeFilter, List<string> targetFilter, List<string> payMethodFilter);

        bool SaveRecord(RecordModel model);

        bool SaveRecords(string date, List<RecordModel> list);


        bool DeleteData(string Date);
    }
}
