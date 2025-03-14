using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using 記帳.Models;

namespace 記帳.ChartFactory
{
    [DisplayName("堆疊圖")]
    public class StackedChart : AChart
    {
        public override void DataBinding(object data, Chart chart, SeriesChartType chartType)
        {
            List<StackChartModel> model = (List<StackChartModel>)data;
            var types = model.Select(x => x.Type).Distinct().ToList();
            var res = model.GroupBy(x => x.Date).Select(x => new
            {
                x.Key,
                TypeData = x.GroupBy(y => y.Type).ToDictionary(y => y.Key, y => y.Sum(z => int.Parse(z.Price)))
            }).ToList();
            Random rnd = new Random();

            for (int i = 0; i < res.Count; i++)
            {
                for (int j = 0; j < res[i].TypeData.Count; j++)
                {

                    (Series series, Legend legend) = SetSeries(chartType);
                    series.Color = Color.FromArgb(
                        200,                 // 透明度 (0 = 完全透明, 255 = 不透明)
                        rnd.Next(200, 256),
                        rnd.Next(180, 256),
                        rnd.Next(200, 256)
                    );
                    series.Name = types[j];
                    double value = res[i].TypeData.ContainsKey(types[j]) ? res[i].TypeData[types[j]] : 0;
                    series.Points.AddXY(res[i].Key.ToString(), value);
                    chart.Series.Add(series);
                    chart.Legends.Add(legend);
                }

            }
        }

        public override Chart GetChart(object datas, DateTime time1, DateTime time2, Chart chart1)
        {
            List<StackChartModel> model = (List<StackChartModel>)datas;
            var types = model.Select(x => x.Type).Distinct().ToList();
            var res = model.GroupBy(x => x.Date).Select(x => new
            {
                x.Key,
                TypeData = x.GroupBy(y => y.Type).ToDictionary(y => y.Key, y => y.Sum(z => int.Parse(z.Price)))
            }).ToList();

            DataBinding(datas, chart1, SeriesChartType.StackedColumn);

            return chart1;
        }


    }
}
