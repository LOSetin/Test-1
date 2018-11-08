using System;
using Arrow.Framework;

namespace TMS
{
    public partial class SiteSettingInfo : EntityBase
    {
        public SiteSettingInfo (){}

        /// <summary>
        /// 
        /// </summary>
        public int ID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string HotSearchWords { set; get; }

        public static readonly string TableOrViewName = "SiteSetting";
        public static readonly string AllFields = "ID,HotSearchWords,ExtraFields";
        public static readonly string TablePrimaryKey = "ID";
    }
}
