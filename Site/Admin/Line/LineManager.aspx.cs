using Arrow.Framework.WebControls;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework.Extensions;
using Arrow.Framework;
using TMS;
using System.Data;
using System.Collections.Generic;

public partial class LineManager : AdminBase
{
    protected List<LineCatInfo> allCats = LineBLL.SelectCatList("1=1");

    protected void Page_Load(object sender, EventArgs e)
    {
        SetNav("线路管理");
        btnUpdateAll.Attributes.Add("onclick", "return confirm('确定设置选中的线路为热卖吗？')");
        if(!Page.IsPostBack)
        {
            DataTable dt = LineBLL.SelectLineCat("IsDel=0").Tables[0];
            DataHelper.BindTree(ddlCat, dt, "ID", "Name", "ParentID");
            ddlCat.Items.Insert(0, new ListItem("所有分类", "0"));
            BindData();
        }
    }


    protected void BindData()
    {
        string condition = "IsDel=0";
        string keyword = GetUrlString("keyword");
        int catID = GetUrlInt("CatID");
        if(!keyword.IsNullOrEmpty())
        {
            condition = condition + " And Name like '%" + keyword + "%'";
        }

        if(catID>0)
        {
            LineCatInfo cat = allCats.Find(s => s.ID == catID);
            if (cat != null)
            {
                if (cat.ParentID == 0)
                    condition = condition + " And FirstCatID=" + catID;
                else
                    condition = condition + " And SecondCatID=" + catID;
            }
        }

        AdminSetting.CreateWebPagerForGridView(gvData, ArrowControlPageIndex);

        WebQuery query = new WebQuery();
        query.Fields = LineInfo.AllFields;
        query.OrderBy = "ID desc";
        query.PrimaryKey = LineInfo.TablePrimaryKey;
        query.SqlCreateType = ControlSqlCreateType.RowNum;
        query.TableName =LineInfo.TableOrViewName;
        query.Condition = condition;

        gvData.Db = TMS.Db.Helper;
        gvData.Query = query;
        gvData.CreateDataSource();
        gvData.DataBind();

        tbKeyWord.Text = keyword;
        ddlCat.SelectedValue = catID.ToString();

    }

    protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((LinkButton)e.Row.FindControl("lbtnDel")).Attributes.Add("onclick", "return confirm('确定删除该线路吗？');");
        }
    }

    protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string commandName = e.CommandName;
        int id = e.CommandArgument.ToArrowInt();
        if (e.CommandName == "UpdateData")
        {
            Response.Redirect("LineEdit.aspx?id=" + id + CreateReturnUrl("&"));
        }
        else if (e.CommandName == "DelData")
        {
            //删除
            LineBLL.SetLineIsDel(id, 1);
            BindData();
            MessageBox.Show("删除成功！");
        }
        else if(e.CommandName== "DetailData")
        {
            Response.Redirect("ScheduleManager.aspx?id=" + id + CreateReturnUrl("&"));
        }
        else if(e.CommandName=="GroupData")
        {
            Response.Redirect("GroupManager.aspx?id=" + id + CreateReturnUrl("&"));
        }

       
        
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        Response.Redirect("LineManager.aspx?keyword=" + Server.UrlEncode(tbKeyWord.Text.Trim()) + "&CatID=" + ddlCat.SelectedValue);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("LineEdit.aspx");
    }

    protected void btnUpdateAll_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvData.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                int id = (row.FindControl("hfID") as HiddenField).Value.Trim().ToArrowInt();
                int isHot = (row.FindControl("chkHot") as CheckBox).Checked ? 1 : 0;
                var model = LineBLL.SelectLine(id);
                if (model != null)
                {
                    model.IsHot = isHot;
                    LineBLL.UpdateLine(model);
                }
            }
        }
        BindData();
        MessageBox.Show("更新成功！");

    }
}