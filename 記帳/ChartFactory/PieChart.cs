using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using 記帳.Models;

namespace 記帳.ChartFactory
{
    [DisplayName("圓餅圖")]
    internal class PieChart : AChart
    {
        public override Chart GetChart(object rawData, DateTime time1, DateTime time2, Chart chart1)
        {

            DataBinding(rawData, chart1, SeriesChartType.Pie);

            //Series series = new Series { ChartType = SeriesChartType.Pie };


            //foreach (var point in series.Points)
            //{
            //    point.Label = "#VALY";       // 在饼图上显示 Price（数值）
            //    point.LegendText = "#VALX";  // 在图例中显示 Key（类别）
            //}


            return chart1;
        }



        public override void DataBinding(object data, Chart chart, SeriesChartType chartType)
        {
            List<RecordModel> records = (List<RecordModel>)data;
            var res = records.GroupBy(x => x.Type).Select(x => new
            {
                x.Key,
                Price = x.Sum(y => int.Parse(y.Price))
            }).ToList();
            (Series series, Legend legend) = SetSeries(chartType);
            series.Points.DataBindXY(res.Select(x => x.Key).ToList(), res.Select(x => x.Price).ToList());
            chart.Series.Add(series);
            chart.Legends.Add(legend);
        }
    }
}
