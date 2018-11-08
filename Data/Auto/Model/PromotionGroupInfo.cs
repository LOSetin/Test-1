using System;
using Arrow.Framework;

namespace TMS
{
    public partial class PromotionGroupInfo : EntityBase
    {
        public PromotionGroupInfo (){}

        /// <summary>
        /// 
        /// </summary>
        public int ID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int PromotionID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int GroupID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int LineID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int TotalNum { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int SelledNum { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int RemainNum { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public decimal RawInnerPrice { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public decimal RawOuterPrice { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime BeginTime { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime AddTime { set; get; }

        public static readonly string TableOrViewName = "PromotionGroup";
        public static readonly string AllFields = "ID,PromotionID,GroupID,LineID,TotalNum,SelledNum,RemainNum,RawInnerPrice,RawOuterPrice,BeginTime,AddTime,ExtraFields";
        public static readonly string TablePrimaryKey = "ID";
    }
}
