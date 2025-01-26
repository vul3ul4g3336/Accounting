using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 記帳.Forms;

namespace 記帳
{
    public class FormBox
    {
        private static Dictionary<string, Form> formBox = new Dictionary<string, Form>();
        static Form form = null;

        public static Form GetForm(string formType)
        {
            if (form != null)
            {
                form.Hide(); //記帳本

            }
            if (!formBox.ContainsKey(formType))
            {
                // 嘗試使用反射讓他自動創建
                //switch (formType)
                //{
                //    case "記一筆":
                //        form = new 記一筆();

                //        break;
                //    case "帳本分析":
                //        form = new 帳本分析();

                //        break;
                //    case "記帳本":
                //        form = new 記帳本();

                //        break;
                //    case "圖表分析":
                //        form = new 圖表分析();

                //        break;
                //    case "系統設定":
                //        form = new 系統設定();

                //        break;

                Type t = Type.GetType("記帳.Forms." + formType);
                object obj = Activator.CreateInstance(t);
                form = (Form)obj;
                formBox.Add(formType, form);

            }



            form = formBox[formType];

            FieldInfo[] info = form.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            NavBar navBar = (NavBar)info.FirstOrDefault(x => x.FieldType == typeof(NavBar)).GetValue(form);
            navBar.buttonEnabled(formType);
            return form;

        }
    }

}

