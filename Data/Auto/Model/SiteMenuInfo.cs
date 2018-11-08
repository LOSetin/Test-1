using System;
using Arrow.Framework;

namespace TMS
{
    public partial class SiteMenuInfo : EntityBase
    {
        public SiteMenuInfo (){}

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
        public string StartTag { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string Url { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int ParentID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string Remarks { set; get; }

        public static readonly string TableOrViewName = "SiteMenu";
        public static readonly string AllFields = "ID,Name,StartTag,Url,ParentID,Remarks,ExtraFields";
        public static readonly string TablePrimaryKey = "ID";
    }
}
