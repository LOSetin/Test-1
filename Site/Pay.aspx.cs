using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pay : MemberBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(CurrentMember==null)
        {
            Response.Write("<script>history.go(-1);</script>");
        }
        else
        {
            string orderNum = Request.QueryString["OrderNum"];
            var order = OrderBLL.SelectOrder(orderNum);
            if (order == null)
            {
                JsBox.Jump("-1");
            }
            else
            {
                string msg = "";
                bool success = OrderBLL.PaySuccessHandler(orderNum, CurrentMember.UserName, order.TotalMoney, out msg);
                if (success)
                {
                    JsBox.Alert("支付成功！", "MemberOrder.aspx");
                }
                else
                {
                    Response.Write(msg);
                }
            }
        }
    }
}