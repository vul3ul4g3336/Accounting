using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using 記帳.ChartFactory;
using 記帳.Enums;


namespace 記帳
{
    public class ChartBuilder
    {
        Chart chart = new Chart();
        List<Series> seriesList = new List<Series>();
        //public ChartBuilder SetSeries(int count = 1)
        //{
        //    ChartArea chartArea1 = new ChartArea();
        //    chartArea1.Name = "ChartArea1";
        //    chart.ChartAreas.Add(chartArea1);
        //    for (int i = 0; i < count; i++)
        //    {
        //        Series series = new Series { };
        //        series.XValueType = ChartValueType.String;
        //        series.IsValueShownAsLabel = true;

        //        seriesList.Add(series);
        //    }
        //    return this;
        //}
        //public ChartBuilder SetLegend(int count = 1)
        //{
        //    for (int i = 0; i < count; i++)
        //    {
        //        Legend legend = new Legend { };
        //        chart.Legends.Add(legend);
        //    }
        //    return this;
        //}
        public Chart Build(ChartEnum chartType, object rawData, DateTime time1, DateTime time2)
        {
            ChartArea chartArea1 = new ChartArea();
            chartArea1.Name = "ChartArea1";
            chart.ChartAreas.Add(chartArea1);

            Assembly assembly = Assembly.GetExecutingAssembly();
            var types = assembly.DefinedTypes;
            var chartInstance = types.First(x =>
                               x.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName == chartType.ToString());

            AChart type = (AChart)Activator.CreateInstance(chartInstance);
            Chart chartResult = type.GetChart(/*rawData, time1, time2*/rawData, time1, time2, chart);
            return chartResult;
        }

    }
}
