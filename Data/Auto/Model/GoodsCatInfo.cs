using System;
using Arrow.Framework;

namespace TMS
{
    public partial class GoodsCatInfo : EntityBase
    {
        public GoodsCatInfo (){}

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
        public int IsDel { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int SortOrder { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string Remarks { set; get; }

        public static readonly string TableOrViewName = "GoodsCat";
        public static readonly string AllFields = "ID,Name,IsDel,SortOrder,Remarks,ExtraFields";
        public static readonly string TablePrimaryKey = "ID";
    }
}
