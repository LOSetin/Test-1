using System;
using Arrow.Framework;

namespace TMS
{
    public partial class TravelGroupInfo : EntityBase
    {
        public TravelGroupInfo (){}

        /// <summary>
        /// 
        /// </summary>
        public int ID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int LineID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int TotalNum { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int RemainNum { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int JoinNum { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int PromotionNum { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string GroupNum { set; get; }

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
        public string GoTravel { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string BackTravel { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public decimal OuterPrice { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public decimal InnerPrice { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Deposit { set; get; }

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
        public int IsDel { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int IsPublish { set; get; }

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

        public static readonly string TableOrViewName = "TravelGroup";
        public static readonly string AllFields = "ID,LineID,Name,TotalNum,RemainNum,JoinNum,PromotionNum,GroupNum,GoDate,BackDate,GatheringTime,GatheringPlace,TransferPlace,GoTravel,BackTravel,OuterPrice,InnerPrice,Deposit,GruopLeader,TravelGuide,IsDel,IsPublish,Remarks,AddUserName,AddUserRealName,AddTime,ExtraFields";
        public static readonly string TablePrimaryKey = "ID";
    }
}
