using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 記帳
{
    internal class Student : IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("記憶體被回收了!");
        }
    }
}
