using Arrow.Framework.WebControls;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework.Extensions;
using Arrow.Framework;
using TMS;
using System.Data;
using System.Collections.Generic;

public partial class UnConfirmOrder : AdminBase
{
    protected List<LineCatInfo> allCats = LineBLL.SelectCatList("1=1");

    protected void Page_Load(object sender, EventArgs e)
    {
        SetNav("未确认订单管理");
        if(!Page.IsPostBack)
        {
            BindData();
        }
    }


    protected void BindData()
    {
        string condition = "OrderStatus='" + OrderStatus.WaitingConfirm + "'";
        DateTime dtBegin = GetUrlDateTime("bdate");
        DateTime dtEnd = GetUrlDateTime("edate");


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
            MessageBox.Show("确认成功！", CurrentUrl);
        }
       
        
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        Response.Redirect("UnConfirmOrder.aspx?bdate=" + tbBegin.Text.Trim()+"&edate="+tbEnd.Text.Trim());
    }

    
}