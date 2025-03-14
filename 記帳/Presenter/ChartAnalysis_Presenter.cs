using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using 記帳.Contract;
using 記帳.Enums;
using 記帳.Models;
using 記帳.Repositary;
using static 記帳.Contract.ChartAnalysis_Contract;

namespace 記帳.Presenter
{
    internal class ChartAnalysis_Presenter : ChartAnalysisIPresenter
    {
        private ChartAnalysisIView _view;

        IRepository repository = new CSV_Repository();
        public ChartAnalysis_Presenter(ChartAnalysisIView view)
        {
            _view = view;
        }
        public void GetChart(DateTime dateTime1, DateTime dateTime2, ChartEnum chartType)
        {
            ChartBuilder chartBuilder = new ChartBuilder();

            object data = null;
            switch (chartType)
            {
                case ChartEnum.圓餅圖:


                    data = repository.GetRawData(dateTime1, dateTime2);
                    break;
                case ChartEnum.折線圖:

                    data = repository.GetTimeRangeData(dateTime1, dateTime2);
                    break;
                case ChartEnum.堆疊圖:

                    data = repository.GetStackData(dateTime1, dateTime2);
                    break;

            }
            Chart chart = chartBuilder.Build(chartType, data, dateTime1, dateTime2);
            _view.search_Response(chart);
        }

    }
}
