using System;
using Arrow.Framework;

namespace TMS
{
    public partial class ShopingCarInfo : EntityBase
    {
        public ShopingCarInfo (){}

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
        public int LineID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string LineName { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int GroupID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime GoDate { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime BackDate { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int PromotionID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string PromotionName { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int BuyNum { set; get; }

        public static readonly string TableOrViewName = "ShopingCar";
        public static readonly string AllFields = "ID,UserName,LineID,LineName,GroupID,GoDate,BackDate,PromotionID,PromotionName,BuyNum";
        public static readonly string TablePrimaryKey = "ID";
    }
}
