using System;
using Arrow.Framework;

namespace TMS
{
    public partial class PicWallInfo : EntityBase
    {
        public PicWallInfo (){}

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
        public string BigPicPath { set; get; }

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
        public int SortOrder { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int IsTop { set; get; }

        public static readonly string TableOrViewName = "PicWall";
        public static readonly string AllFields = "ID,Name,CoverPath,BigPicPath,Remarks,AddUserName,AddUserRealName,AddTime,SortOrder,IsTop,ExtraFields";
        public static readonly string TablePrimaryKey = "ID";
    }
}
