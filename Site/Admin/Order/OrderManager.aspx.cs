using Arrow.Framework.WebControls;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework.Extensions;
using Arrow.Framework;
using TMS;
using System.Data;
using System.Collections.Generic;

public partial class OrderManager : AdminBase
{
    protected List<LineCatInfo> allCats = LineBLL.SelectCatList("1=1");

    protected void Page_Load(object sender, EventArgs e)
    {
        SetNav("订单管理");
        if(!Page.IsPostBack)
        {
            typeof(OrderStatus).BindToDropDownList(ddlStatus);
            ddlStatus.Items.Insert(0, new ListItem("所有", "0"));
            BindData();
        }
    }


    protected void BindData()
    {
        string condition = "1=1";
        DateTime dtBegin = GetUrlDateTime("bdate");
        DateTime dtEnd = GetUrlDateTime("edate");
        string status = GetUrlString("status");
        if(!status.IsNullOrEmpty() && status!="0")
        {
            condition = condition + " And OrderStatus = '" + status + "'";
        }

       if(dtBegin!=DateTime.MinValue && dtEnd!=DateTime.MinValue && dtBegin<=dtEnd)
        {
            condition = condition + " And AddTime>='" + dtBegin.ToStartString() + "' And AddTime<='" + dtEnd.ToEndString() + "'";
        }

        AdminSetting.CreateWebPagerForGridView(gvData, ArrowControlPageIndex);

        WebQuery query = new WebQuery();
        query.Fields = "*";
        query.OrderBy = "OrderNum desc";
        query.PrimaryKey = "OrderNum";
        query.SqlCreateType = ControlSqlCreateType.RowNum;
        query.TableName = "V_Order";
        query.Condition = condition;

        gvData.Db = TMS.Db.Helper;
        gvData.Query = query;
        gvData.CreateDataSource();
        gvData.DataBind();

        ddlStatus.SelectedValue = status;
        tbBegin.Text = dtBegin == DateTime.MinValue ? "" : dtBegin.ToDateOnlyString();
        tbEnd.Text = dtEnd == DateTime.MinValue ? "" : dtEnd.ToDateOnlyString();
    }

    protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((LinkButton)e.Row.FindControl("lbtnConfirm")).Attributes.Add("onclick", "return confirm('订单确认后，用户可以支付，确认确定订单？');");
        }
    }

    protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string commandName = e.CommandName;
        string orderNum = e.CommandArgument.ToArrowString();
        if (e.CommandName == "UpdateData")
        {
            GridViewRow drv = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent));
            string memberUserName = (gvData.Rows[drv.RowIndex].FindControl("hfMemberUserName") as HiddenField).Value;
            Response.Redirect("OrderMemberEdit.aspx?OrderNum=" + orderNum + "&MemberUserName=" + memberUserName + CreateReturnUrl("&"));
        }
        else if (e.CommandName == "ConfirmData")
        {
            GridViewRow drv = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent));
            string memberUserName = (gvData.Rows[drv.RowIndex].FindControl("hfMemberUserName") as HiddenField).Value;
            OrderBLL.ConfirmOrder(orderNum, memberUserName, CurrentAdmin);
            BindData();
            MessageBox.Show("确认成功！");
        }
       
        
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        Response.Redirect("OrderManager.aspx?status=" + Server.UrlEncode(ddlStatus.SelectedValue) + "&bdate=" + tbBegin.Text.Trim()+"&edate="+tbEnd.Text.Trim());
    }

    
}