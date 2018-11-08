using System;
using Arrow.Framework;

namespace TMS
{
    public partial class TravelOrderInfo : EntityBase
    {
        public TravelOrderInfo (){}

        /// <summary>
        /// 
        /// </summary>
        public string OrderNum { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string OrderType { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string OrderStatus { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int GroupID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int LineID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public decimal TotalMoney { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public decimal MoneyPayed { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public decimal MoneyReturn { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int BuyNum { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int CanChangeNum { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public int PromotionID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string CompanyRemarks { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime AddTime { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string AddMemberName { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string AddMemberMobile { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string AddMemberRealName { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string AddMemberRemarks { set; get; }

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

        /// <summary>
        /// 
        /// </summary>
        public string OperatorUserName { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string OperatorRealName { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string OrderHistory { set; get; }

        public static readonly string TableOrViewName = "TravelOrder";
        public static readonly string AllFields = "OrderNum,OrderType,OrderStatus,GroupID,LineID,TotalMoney,MoneyPayed,MoneyReturn,BuyNum,CanChangeNum,PromotionID,CompanyRemarks,AddTime,AddMemberName,AddMemberMobile,AddMemberRealName,AddMemberRemarks,InviteNum,InviterUserName,InviterRealName,OperatorUserName,OperatorRealName,ExtraFields,OrderHistory";
        public static readonly string TablePrimaryKey = "OrderNum";
    }
}
