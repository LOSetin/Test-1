using System;
using Arrow.Framework;

namespace TMS
{
    public partial class MemberFavInfo : EntityBase
    {
        public MemberFavInfo (){}

        /// <summary>
        /// 
        /// </summary>
        public int ID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string UserName { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string FavType { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string FavObjID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime AddTime { set; get; }

        public static readonly string TableOrViewName = "MemberFav";
        public static readonly string AllFields = "ID,UserName,FavType,FavObjID,AddTime,ExtraFields";
        public static readonly string TablePrimaryKey = "ID";
    }
}
