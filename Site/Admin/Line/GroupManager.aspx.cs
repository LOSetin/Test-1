using Arrow.Framework.WebControls;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework.Extensions;
using Arrow.Framework;
using TMS;
using System.Data;
using System.Collections.Generic;

public partial class GroupManager : AdminBase
{
    protected LineInfo MyLine { get { return LineBLL.SelectLine(GetUrlInt("ID")); } }

    protected void Page_Load(object sender, EventArgs e)
    {
        SetNav("旅游团管理");
        AddSubNav("线路管理-LineManager.aspx");
        if(!Page.IsPostBack)
        {
            BindData();
            ltTips.Text = MyLine == null ? "" : MyLine.Name;
            ltTitle.Text = ltTips.Text;
        }
    }


    protected void BindData()
    {
        string condition = "IsDel=0 And LineID="+MyLine.ID;

        DateTime dtBegin = GetUrlDateTime("bdate");
        DateTime dtEnd = GetUrlDateTime("edate");
        if(dtBegin!=DateTime.MinValue && dtEnd!=DateTime.MinValue && dtEnd>dtBegin)
        {
            condition = condition + " And GoDate>='" + dtBegin.ToStartString() + "' And GoDate<='" + dtEnd.ToEndString() + "'";
        }

        AdminSetting.CreateWebPagerForGridView(gvData, ArrowControlPageIndex);

        WebQuery query = new WebQuery();
        query.Fields = TravelGroupInfo.AllFields;
        query.OrderBy = "ID";
        query.PrimaryKey = TravelGroupInfo.TablePrimaryKey;
        query.SqlCreateType = ControlSqlCreateType.RowNum;
        query.TableName = TravelGroupInfo.TableOrViewName;
        query.Condition = condition;

        gvData.Db = TMS.Db.Helper;
        gvData.Query = query;
        gvData.CreateDataSource();
        gvData.DataBind();

        if (dtBegin != DateTime.MinValue && dtEnd != DateTime.MinValue)
        {
            tbBegin.Text = dtBegin.ToDateOnlyString();
            tbEnd.Text = dtEnd.ToDateOnlyString();
        }

    }

    protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((LinkButton)e.Row.FindControl("lbtnDel")).Attributes.Add("onclick", "return confirm('确定删除该团吗？');");
            ((LinkButton)e.Row.FindControl("lbtnAdd")).Attributes.Add("onclick", "return confirm('确定复制添加该团吗？');");
        }
    }

    protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string commandName = e.CommandName;
        int id = e.CommandArgument.ToArrowInt();
        if (e.CommandName == "UpdateData")
        {
            Response.Redirect("GroupEdit.aspx?LineID="+ MyLine.ID+"&id=" + id + CreateReturnUrl("&"));
        }
        else if (e.CommandName == "DelData")
        {
            //删除
            GroupBLL.SetGroupIsDel(id, 1);
            BindData();
            MessageBox.Show("删除成功！");
        }
        else if(e.CommandName=="AddData")
        {
            var model = GroupBLL.SelectGroup(id);
            if(model!=null)
            {
                model.GoDate = model.GoDate.AddDays(1);
                model.BackDate = model.BackDate.AddDays(1);
                model.JoinNum = 0;
                model.AddTime = DateTime.Now;
                model.AddUserName = CurrentAdmin.UserName;
                model.AddUserRealName = CurrentAdmin.RealName;

                GroupBLL.AddGroup(model);
                BindData();

            }
        }
    }

    

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("GroupEdit.aspx?LineID=" + MyLine.ID);
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        RedirectToReturnUrl("LineManager.aspx");
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        Response.Redirect("GroupManager.aspx?id=" + MyLine.ID + "&bdate=" + Server.UrlEncode(tbBegin.Text.Trim()) + "&edate=" + Server.UrlEncode(tbEnd.Text.Trim()));
    }
}