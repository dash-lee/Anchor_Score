using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Anchor_Score
{
    internal static class CalScore
    {
        internal static double Score()
        {
            double score = 0.0;

            //计算方法
            //分为X个板块计算：
            //1.付费转化次数
            //2.有效接听率：包含总的接听次数，有效的接听次数
            //3.AIB下发次数以及自身的AIB接听次数
            //4.通话时长
            //5.收益金币：包含通话收益金币和礼物收益金币
            //6.数据波动情况（主要是上线情况的一个核查，或者是说前一天的一个表现情况和今天数据的表情情况的一个对比）

            //分配计算分值的各项占比

            //付费转化次数


            return score;
        }

        //付费转化次数计算权重分(参数：付费转化次数)
        internal static double TransFeeCountToday(int paidCountToday)
        {
            ModuleSupport.Today_New_Pay_User = 800;
            ModuleSupport.Today_Active_Anchor_Count = 300;

            double score = Math.Log(paidCountToday, ModuleSupport.TODAY_AVERAGE_PAY_COUNT) * ModuleSupport.TODAY_PAY_TIMES_BASIC_COUNT;
            if (score >= 30)
            {
                return 30;
            }
            else
            {
                return score;
            }
        }

        //接听率计算权重分（参数：总通话次数、有效通话次数）
        internal static double EffectiveCountToday(int totalCountToday,int EffectiveCountToday)
        {
            int score;
            double effectiveRate = EffectiveCountToday / totalCountToday;   //当前的通话有效率

            

        }
    }
}
