using System;
using Arrow.Framework;

namespace TMS
{
    public partial class V_CostHistoryInfo : EntityBase
    {
        public V_CostHistoryInfo (){}

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
        public int LineID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int GroupID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int BuyNum { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int PromotionID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string LineName { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime GoDate { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string PromotionName { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public decimal TotalMoney { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public decimal MoneyPayed { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string ExchangeStatus { set; get; }

        public static readonly string TableOrViewName = "V_CostHistory";
        public static readonly string AllFields = "ID,UserName,PointsBefore,PointsAfter,PointsCost,GoodsID,GoodsNum,GoodsName,OrderNum,MoneyCost,MoneyBefore,MoneyAfter,CostType,Remarks,AddTime,ExtraFields,LineID,GroupID,BuyNum,PromotionID,LineName,GoDate,PromotionName,TotalMoney,MoneyPayed,ExchangeStatus";
    }
}
