using Arrow.Framework.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TMS;
using Arrow.Framework.Extensions;

public partial class MyChange : SiteBase
{
   


    protected void Page_Load(object sender, EventArgs e)
    {
        if (CurrentMember == null)
            Response.Redirect("Login.aspx");
        ShowTitle("我的兑换记录");
        if (!Page.IsPostBack)
            BindData();
    }



    protected void BindData()
    {
        string condition = "CostType='" + CostType.Exchange + "' And UserName='"+CurrentMember.UserName+"'";

        DateTime dtBegin = GetUrlDateTime("bdate");
        DateTime dtEnd = GetUrlDateTime("edate");


        if (dtBegin != DateTime.MinValue && dtEnd != DateTime.MinValue && dtBegin <= dtEnd)
        {
            condition = condition + " And AddTime>='" + dtBegin.ToStartString() + "' And AddTime<='" + dtEnd.ToEndString() + "'";
        }

        SiteSetting.CreateWebPagerForGridView(gvData, ArrowControlPageIndex);

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
        Response.Redirect("MyChange.aspx?bdate=" + tbBegin.Text.Trim() + "&edate=" + tbEnd.Text.Trim());
    }



    


}