using Arrow.Framework.WebControls;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework.Extensions;
using Arrow.Framework;
using TMS;
using System.Data;
using System.Collections.Generic;

public partial class ScheduleManager : AdminBase
{
    protected LineInfo MyLine { get { return LineBLL.SelectLine(GetUrlInt("ID")); } }

    protected void Page_Load(object sender, EventArgs e)
    {
        SetNav("行程管理");
        AddSubNav("线路管理-LineManager.aspx");
        if(!Page.IsPostBack)
        {
            BindData();
            ltTips.Text = MyLine == null ? "" : MyLine.Name;
            ltTips1.Text = ltTips.Text;
        }
    }


    protected void BindData()
    {
        string condition = "LineID="+MyLine.ID;

        WebPager pager= AdminSetting.CreateWebPagerForGridView(gvData, ArrowControlPageIndex);
        //不分页
        pager.PageSize = 1000;

        WebQuery query = new WebQuery();
        query.Ascending = false;
        query.Fields = LineDetailInfo.AllFields;
        query.OrderBy = "SortOrder asc";
        query.PrimaryKey = LineDetailInfo.TablePrimaryKey;
        query.SqlCreateType = ControlSqlCreateType.RowNum;
        query.TableName = LineDetailInfo.TableOrViewName;
        query.Condition = condition;

        gvData.Db = TMS.Db.Helper;
        gvData.Query = query;
        gvData.CreateDataSource();
        gvData.DataBind();


    }

    protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((LinkButton)e.Row.FindControl("lbtnDel")).Attributes.Add("onclick", "return confirm('确定删除该行程吗？');");
        }
    }

    protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string commandName = e.CommandName;
        int id = e.CommandArgument.ToArrowInt();
        if (e.CommandName == "UpdateData")
        {
            Response.Redirect("ScheduleEdit.aspx?LineID="+ MyLine.ID+"&id=" + id + CreateReturnUrl("&"));
        }
        else if (e.CommandName == "DelData")
        {
            //删除
            LineBLL.DelDetail(id);
            BindData();
            MessageBox.Show("删除成功！");
        }
    }

    

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("ScheduleEdit.aspx?LineID=" + MyLine.ID);
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        RedirectToReturnUrl("LineManager.aspx");
    }
}