using System;
using Arrow.Framework;

namespace TMS
{
    public partial class V_Promotion_GroupInfo : EntityBase
    {
        public V_Promotion_GroupInfo (){}

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
        public DateTime AddTime { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string PromotionName { set; get; }

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
        public DateTime BackDate { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string GruopLeader { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string TravelGuide { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string GatheringTime { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string GatheringPlace { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string TransferPlace { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string CoverPath { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string StartAddress { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string TargetAddress { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string GoTravel { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string BackTravel { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string SignUpNotice { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string LineDesc { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string WarmTips { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string OtherNotice { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string ProductNum { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string TravelDays { set; get; }

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
        public decimal RawInnerPrice { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public decimal RawOuterPrice { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int RemainNum { set; get; }

        public static readonly string TableOrViewName = "V_Promotion_Group";
        public static readonly string AllFields = "ID,PromotionID,GroupID,LineID,TotalNum,SelledNum,AddTime,ExtraFields,PromotionName,LineName,GoDate,BackDate,GruopLeader,TravelGuide,GatheringTime,GatheringPlace,TransferPlace,CoverPath,StartAddress,TargetAddress,GoTravel,BackTravel,SignUpNotice,LineDesc,WarmTips,OtherNotice,ProductNum,TravelDays,PromotionType,Discount,TotalPayOneTime,TotalPayOneTimeJoinNum,FullCutTotal,FullCutMinus,SecondKillPrice,StartTime,EndTime,IsDel,RawInnerPrice,RawOuterPrice,RemainNum";
    }
}
