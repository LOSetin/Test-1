using System;
using Arrow.Framework;

namespace TMS
{
    public partial class LineInfo : EntityBase
    {
        public LineInfo (){}

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
        public int FirstCatID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int SecondCatID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string ProductNum { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string CoverPath { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string BigPicPath { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string TravelDays { set; get; }

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
        public string OtherNotice { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string WarmTips { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public decimal MinPrice { set; get; }

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

        /// <summary>
        /// 
        /// </summary>
        public int IsDel { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int IsTop { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int IsHot { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int HitTimes { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int IsPickup { set; get; }

        public static readonly string TableOrViewName = "Line";
        public static readonly string AllFields = "ID,Name,FirstCatID,SecondCatID,ProductNum,CoverPath,BigPicPath,TravelDays,StartAddress,TargetAddress,GoTravel,BackTravel,SignUpNotice,LineDesc,OtherNotice,WarmTips,MinPrice,Tag,Remarks,AddUserName,AddUserRealName,AddTime,IsDel,ExtraFields,IsTop,IsHot,HitTimes,IsPickup";
        public static readonly string TablePrimaryKey = "ID";
    }
}
