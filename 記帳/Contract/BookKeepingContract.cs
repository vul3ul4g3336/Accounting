using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 記帳.Models;

namespace 記帳.Contract
{
    internal class BookKeepingContract
    {
        public interface BookKeepingIView
        {
            void search_Response(DataGridView dataGridView);
            void showPicTure(string URL);
        }
        public interface BookKeepingIPresenter
        {
            void search_Events(DataGridView dataGridView, DateTime dateTimePicker1,
               DateTime dateTimePicker2);

            void EditData(DataGridView dataGridView, int columnIndex, int rowIndex);

            void CellDoubleClick(DataGridView dataGridView, int columnIndex, int rowIndex);



        }
    }
}
