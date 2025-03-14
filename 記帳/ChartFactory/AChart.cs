using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace 記帳.ChartFactory
{
    public abstract class AChart
    {

        public abstract Chart GetChart(object datas, DateTime time1, DateTime time2, Chart chart);


        public (Series, Legend) SetSeries(SeriesChartType chartType)
        {
            Series series = new Series { };
            Legend legend = new Legend();
            series.ChartType = chartType;
            series.XValueType = ChartValueType.String;
            series.IsValueShownAsLabel = true;
            return (series, legend);
        }

        public abstract void DataBinding(object data, Chart chart, SeriesChartType chartType);
    }
}
