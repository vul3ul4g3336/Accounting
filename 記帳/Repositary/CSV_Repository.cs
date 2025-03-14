using CSV_Library;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using 記帳.Models;

namespace 記帳.Repositary
{
    internal class CSV_Repository : IRepository
    {
        public List<AnalysisModel> GetClassificationData(DateTime dateTime1, DateTime dateTime2, List<string> typeFilter, List<string> targetFilter, List<string> payMethodFilter)
        {
            var records = GetRawData(dateTime1, dateTime2);
            List<AnalysisModel> datas = records.Select(x => new AnalysisModel
            {
                //startDate = dateTime1.ToString(),
                //endDate = dateTime2.ToString(),
                Type = x.Type,
                PayMethod = x.PayMethod,
                Target = x.Target,
                Price = x.Price
            }).ToList();
            if (typeFilter.Count > 0 || payMethodFilter.Count > 0 || targetFilter.Count > 0)
            {
                datas = datas.GroupBy(item => new
                {
                    Type = typeFilter.Count > 0 ? item.Type : null,
                    Target = targetFilter.Count > 0 ? item.Target : null,
                    PayMethod = payMethodFilter.Count > 0 ? item.PayMethod : null
                }).Where(x => typeFilter.Count > 0 ? typeFilter.Contains(x.Key.Type) : true)
                       .Where(x => targetFilter.Count > 0 ? targetFilter.Contains(x.Key.Target) : true)
                       .Where(x => payMethodFilter.Count > 0 ? payMethodFilter.Contains(x.Key.PayMethod) : true)
                       .Select(x => new AnalysisModel
                       {
                           //startDate = dateTime1.ToString(),
                           //endDate = dateTime2.ToString(),
                           Type = x.Key.Type,
                           Target = x.Key.Target,
                           PayMethod = x.Key.PayMethod,
                           Price = x.Sum(y => int.Parse(y.Price)).ToString()
                       })
                       .ToList();
            }
            return datas;
        }

        public List<RecordModel> GetRawData(DateTime dateTime1, DateTime dateTime2)
        {
            List<RecordModel> list = new List<RecordModel>();
            string rootFile = ConfigurationManager.AppSettings["DirectoryPath"];
            TimeSpan timeSpan = dateTime2 - dateTime1;
            for (int i = 0; i <= timeSpan.Days; i++)
            {
                string date = dateTime1.AddDays(i).ToString("yyyy-MM-dd");
                if (!Directory.Exists(Path.Combine(rootFile, date)))
                {
                    continue;
                }
                var data = CSV.Read<RecordModel>(Path.Combine(rootFile, date, "記帳.csv"));
                list.AddRange(data);
            }
            return list;
        }

        public bool SaveRecord(RecordModel model)
        {
            CSV.Write<RecordModel>(Path.Combine(ConfigurationManager.AppSettings["DirectoryPath"]
                , model.Date, "記帳.csv"), model);
            return true;
        }
        public bool SaveRecords(string filePath, List<RecordModel> list)
        {

            File.Delete(filePath);
            CSV.Write(filePath, list);
            return true;
        }
        public bool DeleteData(string filePath)
        {
            if (!File.Exists(filePath))
                return false;

            File.Delete(filePath);
            return true;
            //刪照片
            // 刪除的資料 A
        }

        public ChartModel GetTimeRangeData(DateTime dateTime1, DateTime dateTime2)
        {
            ChartModel chartModel = new ChartModel();
            var records = GetRawData(dateTime1, dateTime2);
            List<AnalysisModel> datas = records.Select(x => new AnalysisModel
            {
                //startDate = dateTime1.ToString(),
                //endDate = dateTime2.ToString(),
                Type = x.Type,
                Price = x.Price
            }).ToList();
            chartModel.NowadaysModel = datas;
            DateTime lastMonthOfdateTime1 = dateTime1.AddMonths(-1);
            DateTime lastMonthOfdateTime2 = dateTime2.AddMonths(-1);
            records = GetRawData(lastMonthOfdateTime1, lastMonthOfdateTime2);

            List<AnalysisModel> datas2 = records.Select(x => new AnalysisModel
            {
                //startDate = dateTime1.ToString(),
                //endDate = dateTime2.ToString(),                
                Type = x.Type,
                Price = x.Price
            }).ToList();

            chartModel.formerModel = datas2;
            return chartModel;
        }

        public List<StackChartModel> GetStackData(DateTime dateTime1, DateTime dateTime2)
        {

            var records = GetRawData(dateTime1, dateTime2);
            List<StackChartModel> datas = records.Select(x => new StackChartModel
            {
                //startDate = dateTime1.ToString(),
                //endDate = dateTime2.ToString(),
                Date = x.Date,
                Type = x.Type,
                Price = x.Price
            }).ToList();

            return datas;
        }
    }
}
