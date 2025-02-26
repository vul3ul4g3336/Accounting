using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 記帳.Models
{
    public class DataModel
    {



        public static List<string> Object = new List<string>() { "自用", "送禮", "代訂" };

        public static List<string> PaymentMethod = new List<string>() { "現金", "信用卡", "手機支付" };

        public static List<string> Type = new List<string>() { "食", "衣", "育樂", "行" };
        public static List<string> Purpose1 = new List<string>() { "便當", "飲料", "零食", "下午茶" };
        public static List<string> Purpose2 = new List<string>() { "上衣", "pants", "coat", "shoes" };
        public static List<string> Purpose3 = new List<string>() { "教育", "玩樂" };
        public static List<string> Purpose4 = new List<string>() { "交通費" };


        public static Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>
        {
                {"食",Purpose1},
                {"衣",Purpose2},
                {"育樂",Purpose3},
                {"行",Purpose4}

        };
    }
}
