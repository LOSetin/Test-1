using System;
using Arrow.Framework;

namespace TMS
{
    public partial class LineDetailInfo : EntityBase
    {
        public LineDetailInfo (){}

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
        public string Title { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string DayDesc { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string DayPics { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int SortOrder { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string WarmTips { set; get; }

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

        public static readonly string TableOrViewName = "LineDetail";
        public static readonly string AllFields = "ID,LineID,Title,DayDesc,DayPics,SortOrder,WarmTips,Remarks,AddUserName,AddUserRealName,AddTime,ExtraFields";
        public static readonly string TablePrimaryKey = "ID";
    }
}
