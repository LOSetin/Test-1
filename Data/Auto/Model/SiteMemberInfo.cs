using System;
using Arrow.Framework;

namespace TMS
{
    public partial class SiteMemberInfo : EntityBase
    {
        public SiteMemberInfo (){}

        /// <summary>
        /// 
        /// </summary>
        public string UserName { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string UserPwd { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string RealName { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string HeadPicPath { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string Sex { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string MobileNum { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string IDNum { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string Email { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string QQ { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string WeChat { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public decimal TotalCost { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int TotalPoints { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int UsedPoints { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime AddTime { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string Remarks { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string InviteNum { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string InviterUserName { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string InviterRealName { set; get; }

        public static readonly string TableOrViewName = "SiteMember";
        public static readonly string AllFields = "UserName,UserPwd,RealName,HeadPicPath,Sex,MobileNum,IDNum,Email,QQ,WeChat,TotalCost,TotalPoints,UsedPoints,AddTime,Remarks,InviteNum,InviterUserName,InviterRealName,ExtraFields";
        public static readonly string TablePrimaryKey = "UserName";
    }
}
