using System;
using Arrow.Framework;

namespace TMS
{
    public partial class GoodsInfo : EntityBase
    {
        public GoodsInfo (){}

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
        public int CatID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int Num { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int Points { set; get; }

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
        public int IsOut { set; get; }

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

        public static readonly string TableOrViewName = "Goods";
        public static readonly string AllFields = "ID,Name,CatID,Num,Points,CoverPath,BigPicPath,IsOut,Remarks,AddUserName,AddUserRealName,AddTime,ExtraFields";
        public static readonly string TablePrimaryKey = "ID";
    }
}
