using System;
using Arrow.Framework;

namespace TMS
{
    public partial class SiteUserInfo : EntityBase
    {
        public SiteUserInfo (){}

        /// <summary>
        /// 
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string Pwd { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string RealName { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string RoleIDs { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime LastLoginTime { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string LastLoginIP { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ThisLoginTime { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string ThisLoginIP { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string InviteNum { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string Remarks { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string UserStatus { set; get; }

        public static readonly string TableOrViewName = "SiteUser";
        public static readonly string AllFields = "Name,Pwd,RealName,RoleIDs,LastLoginTime,LastLoginIP,ThisLoginTime,ThisLoginIP,InviteNum,Remarks,UserStatus,ExtraFields";
        public static readonly string TablePrimaryKey = "Name";
    }
}
