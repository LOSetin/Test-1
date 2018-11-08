using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TMS;

public partial class MemberAction : MemberBase
{
    /// <summary>
    /// 启用过时
    /// </summary>
    public override bool EnablePageExpires
    {
        get
        {
            return true;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (CurrentMember == null)
        {
            Response.Redirect("Default.aspx");
            return;
        }

        string target = Request.QueryString["target"];
        string action = Request.QueryString["action"];
        if(target=="Order" && action=="Cancel")
        {
            //取消订单
            string orderNum = Request.QueryString["OrderNum"];
            var order = OrderBLL.SelectOrder(orderNum);
            if(order.AddMemberName==CurrentMember.UserName && order.OrderStatus!=OrderStatus.Finished)
            {
                //只有添加者能取消自己的订单，并且该订单并未交易完成
                OrderBLL.UpdateOrderStatus(orderNum, CurrentMember.UserName, OrderStatus.Canceled);
                Response.Write("<script>alert('操作成功！');window.location.href='MemberOrder.aspx';</script>");
            }
            else
            {
                Response.Write("<script>history.go(-1);</script>");
            }

        }
    }
}