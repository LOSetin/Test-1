using System;
using Arrow.Framework;

namespace TMS
{
    public partial class CostHistoryInfo : EntityBase
    {
        public CostHistoryInfo (){}

        /// <summary>
        /// 
        /// </summary>
        public int ID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string UserName { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int PointsBefore { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int PointsAfter { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int PointsCost { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int GoodsID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int GoodsNum { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string GoodsName { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string OrderNum { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public decimal MoneyCost { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public decimal MoneyBefore { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public decimal MoneyAfter { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string CostType { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string Remarks { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime AddTime { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string ExchangeStatus { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string ExpressName { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string ExpressNum { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime SendTime { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime FinishTime { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string LinkMan { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string LinkAddress { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string LinkPhone { set; get; }

        public static readonly string TableOrViewName = "CostHistory";
        public static readonly string AllFields = "ID,UserName,PointsBefore,PointsAfter,PointsCost,GoodsID,GoodsNum,GoodsName,OrderNum,MoneyCost,MoneyBefore,MoneyAfter,CostType,Remarks,AddTime,ExtraFields,ExchangeStatus,ExpressName,ExpressNum,SendTime,FinishTime,LinkMan,LinkAddress,LinkPhone";
        public static readonly string TablePrimaryKey = "ID";
    }
}
