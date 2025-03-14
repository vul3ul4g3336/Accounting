using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using 記帳.Enums;
using 記帳.Models;

namespace 記帳.Contract
{
    internal class ChartAnalysis_Contract
    {
        private ChartAnalysisIView _view;
        public interface ChartAnalysisIView
        {
            void search_Response(Chart model);

            //void StackedChartResponse(List<StackChartModel> model);
            //void PieChartResponse(List<RecordModel> model);
            //void LineChartResponse(ChartModel model);
        }
        public interface ChartAnalysisIPresenter
        {
            void GetChart(DateTime dateTime1, DateTime dateTime2, ChartEnum chartType);
        }
    }
}
