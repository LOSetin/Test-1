using Arrow.Framework.WebControls;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework.Extensions;
using Arrow.Framework;
using TMS;
using System.Data;
using System.Collections.Generic;

public partial class MemberManager : AdminBase
{
    protected List<LineCatInfo> allCats = LineBLL.SelectCatList("1=1");

    protected void Page_Load(object sender, EventArgs e)
    {
        SetNav("会员查询");
        if(!Page.IsPostBack)
        {

            BindData();
        }
    }


    protected void BindData()
    {
        string condition = "1=1";
        string keyword = GetUrlString("keyword");
        if(!keyword.IsNullOrEmpty())
        {
            condition = condition + " And UserName like '%" + keyword + "%'";
        }


        AdminSetting.CreateWebPagerForGridView(gvData, ArrowControlPageIndex);

        WebQuery query = new WebQuery();
        query.Fields = SiteMemberInfo.AllFields;
        query.OrderBy = "AddTime desc";
        query.PrimaryKey = SiteMemberInfo.TablePrimaryKey;
        query.SqlCreateType = ControlSqlCreateType.RowNum;
        query.TableName = SiteMemberInfo.TableOrViewName;
        query.Condition = condition;

        gvData.Db = TMS.Db.Helper;
        gvData.Query = query;
        gvData.CreateDataSource();
        gvData.DataBind();

        tbKeyWord.Text = keyword;

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
        string userName = e.CommandArgument.ToArrowString();
        string command = e.CommandName;
        if(command== "CostHistory")
        {
            Response.Redirect("MemberCostHistory.aspx?UserName=" + userName + CreateReturnUrl("&"));
        }else if(command== "ExchangeHistory")
        {
            Response.Redirect("MemberExchangeHistory.aspx?UserName=" + userName + CreateReturnUrl("&"));
        }
        
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        Response.Redirect("MemberManager.aspx?keyword=" + Server.UrlEncode(tbKeyWord.Text.Trim()));
    }

   
}