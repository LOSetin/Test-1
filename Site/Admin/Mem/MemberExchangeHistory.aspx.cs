using Arrow.Framework.WebControls;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework.Extensions;
using Arrow.Framework;
using TMS;
using System.Data;
using System.Collections.Generic;

public partial class MemberExchangeHistory : AdminBase
{
    protected string UserName { get { return GetUrlString("UserName"); } }
    protected void Page_Load(object sender, EventArgs e)
    {
        SetNav("兑换记录查询");
        if(!Page.IsPostBack)
        {
            BindData();
            AddSubNav("会员管理-MemberManager.aspx");
        }
    }


    protected void BindData()
    {
        string condition = "CostType='" + CostType.Exchange + "' And UserName='" + UserName + "'";

        DateTime dtBegin = GetUrlDateTime("bdate");
        DateTime dtEnd = GetUrlDateTime("edate");


        if (dtBegin != DateTime.MinValue && dtEnd != DateTime.MinValue && dtBegin <= dtEnd)
        {
            condition = condition + " And AddTime>='" + dtBegin.ToStartString() + "' And AddTime<='" + dtEnd.ToEndString() + "'";
        }

        AdminSetting.CreateWebPagerForGridView(gvData, ArrowControlPageIndex);

        WebQuery query = new WebQuery();
        query.Fields = "*";
        query.OrderBy = "ID desc";
        query.PrimaryKey = "ID";
        query.SqlCreateType = ControlSqlCreateType.RowNum;
        query.TableName = "V_ExchangeHistory";
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
            //((LinkButton)e.Row.FindControl("lbtnDel")).Attributes.Add("onclick", "return confirm('确定删除该线路吗？');");
        }
    }

    protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
        
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        Response.Redirect("MemberExchangeHistory.aspx?UserName="+UserName +"&bdate=" + tbBegin.Text.Trim()+"&edate="+tbEnd.Text.Trim());
    }



    protected void btnReturn_Click(object sender, EventArgs e)
    {
        RedirectToReturnUrl("MemberManager.aspx");
    }
}