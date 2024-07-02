using System;
using System.Collections.Generic;
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

        //分配计算权重分的常量
        public static double TOTAL_SCROE = 100.00;

        public static List<int> TOTAL_WEIGHT = new List<int>()
        {

        };

    }
}
