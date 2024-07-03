using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.Pkcs;
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

        /// <summary>
        /// 不同模块所影响的数据评分占比权重
        /// </summary>
        //分配计算权重分的常量
        public static double TOTAL_SCROE = 100.00;

        /// <summary>
        /// 1.付费转化次数占比权重
        /// 2.有效接听率：包含总的接听次数，有效的接听次数
        /// 3.AIB下发次数以及自身的AIB接听次数
        /// 4.通话时长
        /// 5.收益金币：包含通话收益金币和礼物收益金币
        /// 6.数据波动情况（主要是上线情况的一个核查，或者是说前一天的一个表现情况和今天数据的表情情况的一个对比）
        /// </summary>
        public static List<int> TOTAL_WEIGHT = new List<int>()
        {
            30,
            20,
            20,
            10,
            10,
            10
        };

        /// <summary>
        /// 这里是主播计算评分相关的数据，需要区分不同的大区（印尼大区、英语大区），要分开进行计算
        /// </summary>
        //设定的新增付费用户数量
        public static int Today_New_Pay_User {  get; set; } 
        //设定当天活跃的主播数量
        public static int Today_Active_Anchor_Count {  get; set; }
        //当天主播的人均付费人数
        public static double TODAY_AVERAGE_PAY_COUNT = Today_New_Pay_User / Today_Active_Anchor_Count;
        //1.付费转化次数中达标的分数基底
        public static int TODAY_PAY_TIMES_BASIC_COUNT = TOTAL_WEIGHT[0];

        //当天总的有效通话次数
        public static int Today_
    }
}
