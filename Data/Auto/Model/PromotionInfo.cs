using System;
using Arrow.Framework;

namespace TMS
{
    public partial class PromotionInfo : EntityBase
    {
        public PromotionInfo (){}

        /// <summary>
        /// 
        /// </summary>
        public int ID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string CoverPath { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string PromotionDesc { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string PromotionType { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Discount { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public decimal TotalPayOneTime { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int TotalPayOneTimeJoinNum { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public decimal FullCutTotal { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public decimal FullCutMinus { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public decimal SecondKillPrice { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime StartTime { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime EndTime { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int IsDel { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string Tag { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string Remarks { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string AddUserName { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string AddUserRealName { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime AddTime { set; get; }

        public static readonly string TableOrViewName = "Promotion";
        public static readonly string AllFields = "ID,Name,CoverPath,PromotionDesc,PromotionType,Discount,TotalPayOneTime,TotalPayOneTimeJoinNum,FullCutTotal,FullCutMinus,SecondKillPrice,StartTime,EndTime,IsDel,Tag,Remarks,AddUserName,AddUserRealName,AddTime,ExtraFields";
        public static readonly string TablePrimaryKey = "ID";
    }
}
