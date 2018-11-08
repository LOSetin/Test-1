using System;
using Arrow.Framework;

namespace TMS
{
    public partial class OrderDetailInfo : EntityBase
    {
        public OrderDetailInfo (){}

        /// <summary>
        /// 
        /// </summary>
        public int ID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string OrderNum { set; get; }

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
        public string AddMemberName { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string RealName { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string Sex { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string IDNum { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string MobileNum { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime AddTime { set; get; }

        /// <summary>
        /// 
        /// </summary>
        public string Remarks { set; get; }

        public static readonly string TableOrViewName = "OrderDetail";
        public static readonly string AllFields = "ID,OrderNum,GroupID,LineID,AddMemberName,RealName,Sex,IDNum,MobileNum,AddTime,Remarks,ExtraFields";
        public static readonly string TablePrimaryKey = "ID";
    }
}
