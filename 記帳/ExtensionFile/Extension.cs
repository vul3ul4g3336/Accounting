using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 記帳
{
    internal static class Extension
    {
        //public static void SetLabelText(this Form form, string input)
        //{
        //    form.label1.Text = input;
        //}
        static System.Threading.Timer timer = null;
        static Form tempForm = null;

        public static void Debounce(this Form form, Action func, int delay = 500)
        {
            tempForm = form;
            tempForm.Tag = func;

            if (timer != null)
            {
                timer.Change(delay, -1);
                return;
            }
            timer = new System.Threading.Timer(Execute, null, delay, -1);

        }
        private static void Execute(object state)
        {
            Action action = (Action)tempForm.Tag;
            tempForm.Invoke(action);

        }

    }
}
