using Arrow.Framework.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TMS;
using Arrow.Framework.Extensions;
using Arrow.Framework;

public partial class MyOrder : SiteBase
{
   
    protected void BindData()
    {
        int pageIndex = GetUrlInt("p");
        if (pageIndex <= 0) pageIndex = 1;
        SiteSetting.CreateWebPagerForGridView(gvData, pageIndex);

        WebQuery query = new WebQuery();
        query.Fields = "*";
        query.OrderBy = "AddTime";
        query.PrimaryKey = "OrderNum";
        query.SqlCreateType = ControlSqlCreateType.RowNum;
        query.TableName = "V_Order";
        query.Condition = "AddMemberName='" + CurrentMember.UserName + "'";

        gvData.Db = TMS.Db.Helper;
        gvData.Query = query;
        gvData.CreateDataSource();
        gvData.DataBind();


    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (CurrentMember == null)
            Response.Redirect("Login.aspx");
        ShowTitle("我的订单");
        if(!Page.IsPostBack)
        {
            BindData();
        }

    }

    protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string command = e.CommandName.ToString();
        string orderNum = e.CommandArgument.ToString();
        var model = OrderBLL.SelectOrder(orderNum);
        if (model == null)
        {
            BindData();
            return;
        }
        if(command== "EditData")
        {
            Response.Redirect("OrderDetailEdit.aspx?OrderNum=" + orderNum);
        }
        else if(command== "SumitData")
        {
            model.OrderStatus = OrderStatus.Submited;
            OrderBLL.UpdateOrder(model);
            MessageBox.Show("提交成功！");
            
        }
        else if(command== "CancelData")
        {
            model.OrderStatus = OrderStatus.Canceled;
            OrderBLL.UpdateOrder(model);
            MessageBox.Show("取消成功！");
        }
        else if(command== "PayData")
        {
            //支付成功
            //设置订单状态，已付款
            //写入消费历史
            //增加会员点数
            string msg = "";
            bool success = OrderBLL.PaySuccessHandler(orderNum, CurrentMember.UserName, model.TotalMoney, out msg);
            if(success)
            {
                MessageBox.Show("支付成功！");
            }
            else
            {
                //MessageBox.Show("支付失败！");
                Response.Write(msg);
            }

        }
        else if(command=="DetailData")
        {
            Response.Redirect("MyOrderDetail.aspx?OrderNum=" + orderNum);
        }
        BindData();
    }

    protected void gvData_DataBound(object sender, EventArgs e)
    {
       
    }

    protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType==DataControlRowType.DataRow)
        {
            (e.Row.FindControl("lbtnCommit") as LinkButton).Attributes.Add("onclick", "return confirm('确定提交订单吗？');");
            (e.Row.FindControl("lbtnCancel") as LinkButton).Attributes.Add("onclick", "return confirm('确定取消订单吗？');");
            (e.Row.FindControl("lbtnPay") as LinkButton).Attributes.Add("onclick", "return confirm('确定支付吗？');");
        }
    }
}