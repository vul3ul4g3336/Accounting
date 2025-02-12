using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 記帳.Attributes;

namespace 記帳.Models
{
    public class RecordModel
    {
        [DisplayName("日期")]
        public string Date { get; set; }

        [DisplayName("價格")]
        public string Price { get; set; }

        [DisplayName("類型")]
        [ComboBoxColumn("")]
        //[Hide]
        public string Type { get; set; }

        [DisplayName("目的")]
        [ComboBoxColumn("Type")]
        //[Hide]
        public string Purpose { get; set; }
        //4,5
        [DisplayName("對象")] //4=>6=  
        public string Target { get; set; }//4
        [DisplayName("付款方式")]
        public string PayMethod { get; set; }//5


        [DisplayName("圖片1")]
        [Hide]
        public string PictureAddress1 { get; set; }//6
        [DisplayName("圖片2")]
        [Hide]
        public string PictureAddress2 { get; set; }//7
        [ImageColumn]
        public string resizedPictureAddress1 { get; set; }//8
        [ImageColumn]
        public string resizedPictureAddress2 { get; set; }//9


    }
}
