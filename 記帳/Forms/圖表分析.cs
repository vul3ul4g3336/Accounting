using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using 記帳.Enums;

using 記帳.Models;
using 記帳.Presenter;
using 記帳.Repositary;
using static 記帳.Contract.AccountAnalysisContract;
using static 記帳.Contract.ChartAnalysis_Contract;

namespace 記帳.Forms
{
    public partial class 圖表分析 : Form, ChartAnalysisIView
    {

        ChartAnalysisIPresenter chartAnalysisPresenter;
        List<string> typeFilter = new List<string>();
        List<string> targetFilter = new List<string>();
        List<string> payMethodFilter = new List<string>();
        public 圖表分析()
        {
            InitializeComponent();
            chartAnalysisPresenter = new ChartAnalysis_Presenter(this);
        }

        public void StackedChartResponse(List<StackChartModel> model) /*ChartModel list*/
        {
            #region 圓餅圖
            //var res = list.Select(x => new
            //{
            //    Key = x.Type + " " + x.PayMethod + " " + x.Target,
            //    Total = x.Price
            //}).ToList();
            //var priceLabel = list.Select(x => x.Price).ToList();
            //Legend legend2 = new Legend("#VALX");
            //chart1.Legends[0].Position.Auto = true;
            //chart1.Series[0].IsValueShownAsLabel = true;
            //chart1.Series[0].Points.DataBindXY(res.Select(x => x.Key).ToList(), res.Select(x => x.Total).ToList());
            #endregion
            #region 折線圖
            //Chart chart1 = new Chart();
            //ChartArea chartArea1 = new ChartArea();
            //chartArea1.Name = "ChartArea1";
            //chart1.ChartAreas.Add(chartArea1);
            //Legend legend1 = new Legend();
            //Legend legend2 = new Legend();
            //var res = list.formerModel.GroupBy(x => x.Type).Select(x => new
            //{
            //    x.Key,
            //    Price = x.Sum(y => int.Parse(y.Price))
            //}).ToList();

            //Series series1 = new Series();
            //chart1.ChartAreas[0].AxisX.TitleFont = new Font("微软雅黑", 14f, FontStyle.Regular);
            //series1.XValueType = ChartValueType.String;
            ////设置X轴上的值类型
            //series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            //series1.Points.DataBindXY(res.Select(x => x.Key).ToList(), res.Select(x => x.Price).ToList());
            //series1.IsValueShownAsLabel = true;
            //series1.Name = dateTimePicker1.Value.ToString("MM,dd") + "--" + dateTimePicker2.Value.ToString("MM,dd");
            //chart1.Series.Add(series1);


            //Series series2 = new Series();
            //series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            //var res2 = list.NowadaysModel.GroupBy(x => x.Type).Select(x => new
            //{
            //    x.Key,
            //    Price = x.Sum(y => int.Parse(y.Price))
            //}).ToList();

            //series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

            //series2.XValueType = ChartValueType.String;
            ////设置X轴上的值类型
            //series2.Points.DataBindXY(res2.Select(x => x.Key).ToList(), res2.Select(x => x.Price).ToList());
            //series2.IsValueShownAsLabel = true;
            //series2.Name = dateTimePicker1.Value.AddMonths(-1).ToString("MM,dd") + " --" + dateTimePicker2.Value.AddMonths(-1).ToString("MM,dd");

            //chart1.Series.Add(series2);
            //chart1.Legends.Add(legend1);
            //chart1.Legends.Add(legend2);

            //chart1.Legends[0].Docking = Docking.Top;  // 移到頂部
            //chart1.Legends[1].Docking = Docking.Right; // 移到右側

            //flowLayoutPanel2.Controls.Add(chart1);
            #endregion
            Chart chart1 = new Chart();
            ChartArea chartArea1 = new ChartArea();
            chartArea1.Name = "ChartArea1";

            var types = model.Select(x => x.Type).Distinct().ToList();
            var res = model.GroupBy(x => x.Date).Select(x => new
            {
                x.Key,
                TypeData = x.GroupBy(y => y.Type).ToDictionary(y => y.Key, y => y.Sum(z => int.Parse(z.Price)))
            }).ToList();


            //List<string> colors = new List<string>();

            //for (int i = 0; i < types.Count; i++)
            //{
            //    colors.Add(Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256)).Name);
            //}
            Random rnd = new Random();

            for (int i = 0; i < types.Count; i++)
            {
                Series series = new Series(types[i]);
                series.ChartType = SeriesChartType.StackedColumn;
                series.Color = Color.FromArgb(
                    200,                 // 透明度 (0 = 完全透明, 255 = 不透明)
                    rnd.Next(200, 256),
                    rnd.Next(180, 256),
                    rnd.Next(200, 256)
                );
                series.Name = types[i];
                Legend legend = new Legend();

                for (int j = 0; j < res.Count; j++)
                {
                    double value = res[j].TypeData.ContainsKey(types[i]) ? res[j].TypeData[types[i]] : 0;
                    series.Points.AddXY(res[j].Key.ToString(), value);
                }
                chart1.Legends.Add(legend);
                chart1.Series.Add(series);
            }




            //for (int i = 0; i < res.Count; i++)
            //{
            //    Series series = new Series();
            //    series.Name = res[i].Key.ToString();
            //    series.ChartType = SeriesChartType.StackedColumn;
            //    series.IsValueShownAsLabel = true;



            //    series.Points.AddXY(res[i].Key.Date, res[i].foods);
            //    series.Points.AddXY(res[i].Key.Date, res[i].clothing);
            //    series.Points.AddXY(res[i].Key.Date, res[i].housing);
            //    series.Points.AddXY(res[i].Key.Date, res[i].transportation);

            //    chart1.Series.Add(series);
            //}
            chart1.ChartAreas.Add(chartArea1);
            flowLayoutPanel2.Controls.Add(chart1);

            //chart1.Dock = DockStyle.Fill;

            //string[] days = { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" };

            //// 隨機生成 5 種不同類別的數據 (總和為 100%)
            //double[][] values = {
            //    new double[] { 6.2, 18.2, 17.8, 18.7, 17.3, 12.9, 12.5 }, // 顏色1
            //    new double[] { 19.9, 8, 6, 7.5, 4, 9, 8.2 }, // 顏色2
            //    new double[] { 13.7, 11, 11.3, 13.1, 12.9, 12.9, 12.1 }, // 顏色3
            //    new double[] { 9.3, 12.8, 11.9, 8.6, 8.4, 12.9, 16 }, // 顏色4
            //    new double[] { 50.9, 50.1, 53.2, 52.2, 57.3, 52.2, 51.4 } // 顏色5
            //};

            //string[] seriesNames = { "A", "B", "C", "D", "E" };

            //for (int i = 0; i < seriesNames.Length; i++)
            //{
            //    Series series = new Series(seriesNames[i]);
            //    series.ChartType = SeriesChartType.StackedColumn;
            //    //series.IsValueShownAsLabel = true;
            //    series.Color = System.Drawing.Color.FromName(colors[i]);

            //    for (int j = 0; j < days.Length; j++)
            //    {
            //        series.Points.AddXY(days[j], values[i][j]);
            //    }

            //    chart1.Series.Add(series);
            //}

            //// 設定圖例
            //chart1.Legends.Add(new Legend("Legend"));
            //chart1.Legends[0].Docking = Docking.Top;

            //// 設定標題
            ////Title title = new Title();
            ////title.Text = "Weekly Stacked Bar Chart";
            ////title.Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold);
            ////chart1.Titles.Add(title);

            #region    範例
            //string[] x = new string[] { "成都大队", "广东大队", "广西大队", "云南大队", "上海大队", "苏州大队", "深圳大队", "北京大队", "湖北大队", "湖南大队", "重庆大队", "辽宁大队" };
            //double[] y = new double[] { 589, 598, 445, 654, 884, 457, 941, 574, 745, 854, 684, 257 };
            //string[] z = new string[] { "", "", "", "", "", "", "", "", "", "", "", "" };

            //string[] a = new string[] { "成都大队", "广东大队", "广西大队", "云南大队", "上海大队" };
            //double[] b = new double[] { 541, 574, 345, 854, 257 };



            ////标题
            //chart1.Titles.Add("饼图数据分析");
            //chart1.Titles[0].ForeColor = Color.Blue;
            //chart1.Titles[0].Font = new Font("微软雅黑", 12f, FontStyle.Regular);
            //chart1.Titles[0].Alignment = ContentAlignment.TopCenter;
            //chart1.Titles.Add("合计：25412 宗");
            //chart1.Titles[1].ForeColor = Color.Blue;
            //chart1.Titles[1].Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            //chart1.Titles[1].Alignment = ContentAlignment.TopRight;

            ////控件背景
            //chart1.BackColor = Color.Transparent;
            ////图表区背景
            ////chart1.ChartAreas[0].BackColor = Color.Transparent;
            ////chart1.ChartAreas[0].BorderColor = Color.Transparent;
            ////X轴标签间距
            //chart1.ChartAreas[0].AxisX.Interval = 1;
            //chart1.ChartAreas[0].AxisX.LabelStyle.IsStaggered = true;
            //chart1.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            //chart1.ChartAreas[0].AxisX.TitleFont = new Font("微软雅黑", 14f, FontStyle.Regular)
            //;
            ////chart1.ChartAreas[0].AxisX.TitleForeColor = Color.Blue;

            //////X坐标轴颜色
            //chart1.ChartAreas[0].AxisX.LineColor = ColorTranslator.FromHtml("#38587a");
            //chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Blue;
            //chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("微软雅黑", 10f, FontStyle.Regular);
            //////X坐标轴标题
            //chart1.ChartAreas[0].AxisX.Title = "数量(宗)";
            //chart1.ChartAreas[0].AxisX.TitleFont = new Font("微软雅黑", 10f, FontStyle.Regular);
            ////chart1.ChartAreas[0].AxisX.TitleForeColor = Color.Blue;
            //chart1.ChartAreas[0].AxisX.TextOrientation = TextOrientation.Horizontal;
            ////chart1.ChartAreas[0].AxisX.ToolTip = "数量(宗)";
            //////X轴网络线条
            ////chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
            ////chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = ColorTranslator.FromHtml("#2c4c6d");

            //////Y坐标轴颜色
            ////chart1.ChartAreas[0].AxisY.LineColor = ColorTranslator.FromHtml("#38587a");
            //chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Blue;
            ////chart1.ChartAreas[0].AxisY.LabelStyle.Font = new Font("微软雅黑", 10f, FontStyle.Regular);
            //////Y坐标轴标题
            //chart1.ChartAreas[0].AxisY.Title = "数量(宗)";
            //chart1.ChartAreas[0].AxisY.TitleFont = new Font("微软雅黑", 10f, FontStyle.Regular);
            //chart1.ChartAreas[0].AxisY.TitleForeColor = Color.Blue;
            //chart1.ChartAreas[0].AxisY.TextOrientation = TextOrientation.Rotated270;
            ////chart1.ChartAreas[0].AxisY.ToolTip = "数量(宗)";
            //////Y轴网格线条
            ////chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
            ////chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = ColorTranslator.FromHtml("#2c4c6d");

            ////chart1.ChartAreas[0].AxisY2.LineColor = Color.Transparent;

            //////背景渐变
            ////chart1.ChartAreas[0].BackGradientStyle = GradientStyle.None;

            ////图例样式
            ////Legend legend2 = new Legend("#VALX");
            ////legend2.Title = "图例";
            ////legend2.TitleBackColor = Color.Transparent;
            ////legend2.BackColor = Color.Transparent;
            ////legend2.TitleForeColor = Color.Blue;
            ////legend2.TitleFont = new Font("微软雅黑", 10f, FontStyle.Regular);
            ////legend2.Font = new Font("微软雅黑", 8f, FontStyle.Regular);
            ////legend2.ForeColor = Color.Blue;

            ////chart1.Series[0].XValueType = ChartValueType.String;  //设置X轴上的值类型
            ////chart1.Series[0].Label = "#VAL";                //设置显示X Y的值    
            ////chart1.Series[0].LabelForeColor = Color.Blue;
            ////chart1.Series[0].ToolTip = "#VALX:#VAL(宗)";     //鼠标移动到对应点显示数值
            ////chart1.Series[0].ChartType = SeriesChartType.Pie;    //图类型(折线)

            ////chart1.Series[0].Color = Color.Lime;
            ////chart1.Series[0].LegendText = legend2.Name;
            ////chart1.Series[0].IsValueShownAsLabel = true;
            ////chart1.Series[0].LabelForeColor = Color.Blue;
            ////chart1.Series[0].CustomProperties = "DrawingStyle = Cylinder";
            ////chart1.Series[0].CustomProperties = "PieLabelStyle = Outside";
            ////chart1.Legends.Add(legend2);
            ////chart1.Legends[0].Position.Auto = true;
            //chart1.Series[0].IsValueShownAsLabel = true;
            ////是否显示图例
            ////chart1.Series[0].IsVisibleInLegend = true;
            ////chart1.Series[0].ShadowOffset = 0;

            ////饼图折线
            ////chart1.Series[0]["PieLineColor"] = "Blue";
            ////绑定数据
            //chart1.Series[0].Points.DataBindXY(x, y);
            //chart1.Series[1].Points.DataBindXY(a, b);
            ////chart1.Series[0].Points[0].Color = Color.Blue;
            ////绑定颜色
            ////chart1.Series[0].Palette = ChartColorPalette.BrightPastel;



            #endregion
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = Enum.GetValues(typeof(ChartEnum));
            comboBox1.SelectedIndex = 0;
            var props = typeof(DataModel).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
                                    .Where(x => x.FieldType == typeof(List<string>)) /*& (!x.Name.Contains("Purpose")))*/
                                    .ToList();
            foreach (var prop in props)
            {
                var itemList = (List<string>)prop.GetValue(null);
                CheckBox totalCheck = new CheckBox
                {
                    Text = "全選",
                    AutoSize = true
                };
                totalCheck.CheckedChanged += TotalCheckChanged;
                FlowLayoutPanel panel = new FlowLayoutPanel
                {
                    Height = flowLayoutPanel1.Height / 8,
                    Width = flowLayoutPanel1.Width,
                    FlowDirection = FlowDirection.LeftToRight,

                };
                panel.Controls.Add(totalCheck);

                foreach (var property in itemList)
                {
                    CheckBox checkBox = new CheckBox
                    {
                        Text = property,
                        AutoSize = true,
                        Tag = prop.Name.ToString()

                    };
                    panel.Controls.Add(checkBox);
                    checkBox.CheckedChanged += CheckChanged;

                }
                flowLayoutPanel1.Controls.Add(panel);
            }

        }
        private void CheckChanged(object sender, EventArgs e)
        {

            CheckBox checkbox = (CheckBox)sender;
            Console.WriteLine(checkbox.Text);
            if (checkbox.Checked)
            {
                switch (checkbox.Tag)
                {
                    case "Type":
                        typeFilter.Add(checkbox.Text);
                        break;
                    case "Object":
                        targetFilter.Add(checkbox.Text);
                        break;
                    case "PaymentMethod":
                        payMethodFilter.Add(checkbox.Text);
                        break;
                }
            }
            if (!checkbox.Checked)
            {
                switch (checkbox.Tag)
                {
                    case "Type":
                        typeFilter.Remove(checkbox.Text);
                        break;
                    case "Object":
                        targetFilter.Remove(checkbox.Text);
                        break;
                    case "PaymentMethod":
                        payMethodFilter.Remove(checkbox.Text);
                        break;
                }
            }

            var key = DataModel.dictionary.FirstOrDefault(x => x.Value.Contains(checkbox.Text)).Key;


            var groupCheckbox = flowLayoutPanel1.Controls.OfType<FlowLayoutPanel>()
                                            .SelectMany(x => x.Controls.OfType<CheckBox>())
                                            .FirstOrDefault(x => x.Text?.ToString() == key);
            if (groupCheckbox != null)
                groupCheckbox.Checked = true;
            Console.WriteLine(key);
        }

        private void TotalCheckChanged(object sender, EventArgs e)
        {
            CheckBox totalCheck = (CheckBox)sender;
            FlowLayoutPanel panel = totalCheck.Parent as FlowLayoutPanel;
            var checkBoxes = panel.Controls.OfType<CheckBox>().Where(x => x != totalCheck).ToList();
            foreach (var checkBox in checkBoxes)
            {
                checkBox.Checked = !checkBox.Checked;

            }
        }



        private void button1_Click(object sender, EventArgs e)
        {

            flowLayoutPanel2.Controls.Clear();
            DateTime date1 = dateTimePicker1.Value;
            DateTime date2 = dateTimePicker2.Value;

            this.Debounce(() =>
            {
                chartAnalysisPresenter.GetChart(date1, date2, (ChartEnum)comboBox1.SelectedValue);
                //if (comboBox1.Text == "堆疊圖")
                //{
                //    chartAnalysisPresenter.GetStackedChart(date1, date2);
                //    // 動態資料群組 => 建立匿名類別
                //}
                //if (comboBox1.Text == "圓餅圖")
                //{
                //    chartAnalysisPresenter.GetPieChart(date1, date2);
                //}
                //if (comboBox1.Text == "折線圖")
                //{
                //    chartAnalysisPresenter.GetLineChart(date1, date2);
                //}
            });
        }



        public void PieChartResponse(List<RecordModel> model)
        {
            var res = model.GroupBy(x => x.Type).Select(x => new
            {
                x.Key,
                Price = x.Sum(y => int.Parse(y.Price))
            }).ToList();

            Chart chart1 = new Chart { ChartAreas = { new ChartArea() }, Legends = { new Legend { Docking = Docking.Top } } };
            Series series = new Series { ChartType = SeriesChartType.Pie };
            chart1.Series.Add(series);
            series.Points.DataBind(res, "Key", "Price", null);

            foreach (var point in series.Points)
            {
                point.Label = "#VALY";       // 在饼图上显示 Price（数值）
                point.LegendText = "#VALX";  // 在图例中显示 Key（类别）
            }

            chart1.Legends[0].Alignment = StringAlignment.Center;
            flowLayoutPanel2.Controls.Add(chart1);
            //var res = model.GroupBy(x => x.Type).Select(x => new
            //{
            //    x.Key,
            //    Price = x.Sum(y => int.Parse(y.Price))
            //}).ToList();
            //Chart chart1 = new Chart();
            //ChartArea chartArea1 = new ChartArea();
            //chartArea1.Name = "ChartArea1";
            //chart1.ChartAreas.Add(chartArea1);
            //Series series = new Series();
            //series.ChartType = SeriesChartType.Pie;
            //Legend legend = new Legend();
            //chart1.Legends.Add(legend);
            //chart1.Series.Add(series);
            //series.Points.DataBind(res, "Key", "Price", null);

            //foreach (var point in series.Points)
            //{
            //    point.Label = "#VALY";       // 在饼图上显示 Price（数值）
            //    point.LegendText = "#VALX";  // 在图例中显示 Key（类别）
            //}
            //flowLayoutPanel2.Controls.Add(chart1);
        }

        public void LineChartResponse(ChartModel model)
        {
            #region 折線圖
            Chart chart1 = new Chart();
            ChartArea chartArea1 = new ChartArea();
            chartArea1.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea1);
            Legend legend1 = new Legend();
            Legend legend2 = new Legend();
            var res = model.NowadaysModel.GroupBy(x => x.Type).Select(x => new
            {
                x.Key,
                Price = x.Sum(y => int.Parse(y.Price))
            }).ToList();

            Series series1 = new Series();
            chart1.ChartAreas[0].AxisX.TitleFont = new Font("微软雅黑", 14f, FontStyle.Regular);
            series1.XValueType = ChartValueType.String;
            //设置X轴上的值类型
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Points.DataBindXY(res.Select(x => x.Key).ToList(), res.Select(x => x.Price).ToList());
            series1.IsValueShownAsLabel = true;
            series1.Name = dateTimePicker1.Value.ToString("MM,dd") + "--" + dateTimePicker2.Value.ToString("MM,dd");
            chart1.Series.Add(series1);


            Series series2 = new Series();
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            var res2 = model.formerModel.GroupBy(x => x.Type).Select(x => new
            {
                x.Key,
                Price = x.Sum(y => int.Parse(y.Price))
            }).ToList();

            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.XValueType = ChartValueType.String;
            //设置X轴上的值类型
            series2.Points.DataBindXY(res2.Select(x => x.Key).ToList(), res2.Select(x => x.Price).ToList());
            series2.IsValueShownAsLabel = true;
            series2.Name = dateTimePicker1.Value.AddMonths(-1).ToString("MM,dd") + " --" + dateTimePicker2.Value.AddMonths(-1).ToString("MM,dd");
            chart1.Series.Add(series2);

            chart1.Legends.Add(legend1);
            chart1.Legends.Add(legend2);

            chart1.Legends[0].Docking = Docking.Top;  // 移到頂部
            chart1.Legends[1].Docking = Docking.Right; // 移到右側

            flowLayoutPanel2.Controls.Add(chart1);
            #endregion
        }

        public void search_Response(Chart model)
        {
            flowLayoutPanel2.Controls.Add(model);
        }
    }
}
