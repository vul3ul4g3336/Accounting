using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using 記帳.Models;

namespace 記帳.ChartFactory
{
    [DisplayName("折線圖")]
    public class LineChart : AChart
    {
        private DateTime _time1; private DateTime _time2;
        public override void DataBinding(object data, Chart chart, SeriesChartType chartType)
        {
            ChartModel model = (ChartModel)data;
            //Chart chart1 = new Chart();

            var res = model.NowadaysModel.GroupBy(x => x.Type).Select(x => new
            {

                x.Key,
                Price = x.Sum(y => int.Parse(y.Price))
            }).ToList();

            (Series series1, Legend legend1) = SetSeries(chartType);
            series1.XValueType = ChartValueType.String;
            //设置X轴上的值类型
            series1.ChartType = chartType;
            series1.Points.DataBindXY(res.Select(x => x.Key).ToList(), res.Select(x => x.Price).ToList());
            series1.IsValueShownAsLabel = true;
            series1.Name = _time1.ToString("MM,dd") + "--" + _time2.ToString("MM,dd");
            chart.Series.Add(series1);

            var res2 = model.formerModel.GroupBy(x => x.Type).Select(x => new
            {

                x.Key,
                Price = x.Sum(y => int.Parse(y.Price))
            }).ToList();
            (Series series2, Legend legend2) = SetSeries(chartType);
            series2.ChartType = chartType;
            series2.XValueType = ChartValueType.String;
            //设置X轴上的值类型
            series2.Points.DataBindXY(res2.Select(x => x.Key).ToList(), res2.Select(x => x.Price).ToList());
            series2.IsValueShownAsLabel = true;
            series2.Name = _time1.AddMonths(-1).ToString("MM,dd") + "--" + _time2.AddMonths(-1).ToString("MM,dd");
            chart.Series.Add(series2);

            chart.Legends.Add(legend1);
            chart.Legends.Add(legend2);

            chart.Legends[0].Docking = Docking.Top;  // 移到頂部
            chart.Legends[1].Docking = Docking.Right; // 移到右側


        }

        public override Chart GetChart(object datas, DateTime time1, DateTime time2, Chart chart1)
        {
            _time1 = time1;
            _time2 = time2;

            DataBinding(datas, chart1, SeriesChartType.Line);

            return chart1;
        }


    }
}
