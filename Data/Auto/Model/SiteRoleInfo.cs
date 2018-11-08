using System;
using Arrow.Framework;

namespace TMS
{
    public partial class SiteRoleInfo : EntityBase
    {
        public SiteRoleInfo (){}

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
        public string MenuIDList { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string Remarks { set; get; }

        public static readonly string TableOrViewName = "SiteRole";
        public static readonly string AllFields = "ID,Name,MenuIDList,Remarks,ExtraFields";
        public static readonly string TablePrimaryKey = "ID";
    }
}
