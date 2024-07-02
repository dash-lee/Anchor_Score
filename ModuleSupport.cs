using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anchor_Score
{
    internal static class ModuleSupport
    {
        //暂存当前总的数据和分项的数据
        public static List<List<string>> DATA;
        public static List<List<string>> DATA_ALL;

        //Json文件的路径
        public static string jsonPath = @"D:\VS_BEGIN\Anchor_Score\Anchor.json";

        //分配计算权重分的常量
        public static double TOTAL_SCROE = 100.00;

        public static List<int> TOTAL_WEIGHT = new List<int>()
        {

        };

        //设定的当日活跃用户数
        public static int TODAY_ALIVE_USER = 80000;

        //付费次数的常量底数
        public static double LOG_INTER = TODAY_ALIVE_USER / 50;     

    }
}
