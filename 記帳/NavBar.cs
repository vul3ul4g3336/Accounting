using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 記帳
{
    public partial class NavBar : UserControl
    {
        public NavBar()
        {
            InitializeComponent();



            this.SizeChanged += NavBar_SizeChanged;
        }

        private void NavBar_SizeChanged(object sender, EventArgs e)
        {




            Console.WriteLine(this.Width);
            Console.WriteLine(flowLayoutPanel1.Width);
            this.flowLayoutPanel1.Width = this.Width;
            // 加載 Assembly（這裡假設是當前執行的 Assembly）
            Assembly assembly = Assembly.GetExecutingAssembly();

            // 或者加載指定的 Assembly，例如：Assembly.Load("YourAssemblyName")
            // Assembly assembly = Assembly.Load("YourAssemblyName");

            // 遍歷所有類型
            //  typeof(Form).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract
            var formTypes = assembly.GetTypes()
                            .Where(t => t.BaseType == typeof(Form))
                            .ToList();
            int buttonWidth = this.flowLayoutPanel1.Width / formTypes.Count;



            formTypes
                .Select(x => new Button { Text = x.Name, Width = buttonWidth, Height = 80, Margin = new Padding(0) })
                .ToList()
                .ForEach(x =>
                {
                    this.flowLayoutPanel1.Controls.Add(x);
                    x.Click += ChangeForm;
                });
            // 把這段程式碼稍微優化 改寫成自動根據Form的數量調整Button的寬度與margin
        }

        private void ChangeForm(object sender, EventArgs e)
        {
            Button btn = (Button)sender;


            Form form = FormBox.GetForm(btn.Text);



            form.Show(); // 任意抽換子類別，不會影響code正確性

        }

        public void buttonEnabled(string buttonName)   // Form -> NavBar 
        {
            foreach (Button button in flowLayoutPanel1.Controls)
            {
                if (button.Text == buttonName)
                {
                    button.Enabled = false;
                    break;
                }
            }
        }


    }
}
