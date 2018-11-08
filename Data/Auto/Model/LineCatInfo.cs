using System;
using Arrow.Framework;

namespace TMS
{
    public partial class LineCatInfo : EntityBase
    {
        public LineCatInfo (){}

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
        public int ParentID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int IsDel { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int SortOrder { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string Remarks { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int IsShowIndex { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int IsHotSell { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int IsHotSort { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int IsSearchWord { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int HitTimes { set; get; }

        public static readonly string TableOrViewName = "LineCat";
        public static readonly string AllFields = "ID,Name,ParentID,IsDel,SortOrder,Remarks,ExtraFields,IsShowIndex,IsHotSell,IsHotSort,IsSearchWord,HitTimes";
        public static readonly string TablePrimaryKey = "ID";
    }
}
