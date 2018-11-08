using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework.WebControls;
using Arrow.Framework.Extensions;
using Arrow.Framework;
using System.Data;

public partial class LineCat : AdminBase
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        SetNav("线路分类管理");
        btnUpdateAll.Attributes.Add("onclick", "return confirm('保存所有设置吗？')");
        if (!Page.IsPostBack)
        {
            BindShow();
            BindParent();
            BindData();
           
        }
    }

    protected void BindParent()
    {
        ddlParent.DataSource = LineBLL.SelectLineCat("ParentID=0 And IsDel=0 order by SortOrder");
        ddlParent.DataTextField = "Name";
        ddlParent.DataValueField = "ID";
        ddlParent.DataBind();
        ddlParent.Items.Insert(0, new ListItem("作为根分类", "0"));
    }

    protected void BindShow()
    {
        ddlShow.DataSource = LineBLL.SelectLineCat("ParentID=0 And IsDel=0 order by SortOrder");
        ddlShow.DataTextField = "Name";
        ddlShow.DataValueField = "ID";
        ddlShow.DataBind();
        ddlShow.Items.Insert(0, new ListItem("显示一级菜单", "-1"));
        ddlShow.Items.Insert(0, new ListItem("显示所有", "0"));
        
    }

    protected void BindData()
    {
        string condition = "";
        int rootID = 0;
        int parentID = ddlShow.SelectedValue.ToArrowInt();
        if (parentID == 0)
        {
            condition = "IsDel=0 order by SortOrder";
        }
        else if(parentID==-1)
        {
            rootID = 0;
            condition = "IsDel=0 And ParentID=0 Order by SortOrder";
        }
        else {
            rootID = parentID;
            condition = "IsDel=0 And ParentID=" + parentID + " order by SortOrder";
        }
        TreeListView.TreeImageFolder = "../Resources/TreeListView/";
        this.gvData.RootNodeFlag = rootID.ToString();
        gvData.DataSource = LineBLL.SelectLineCat(condition);
        gvData.DataBind();
    }

    protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       if(e.Row.RowType==DataControlRowType.DataRow)
        {
            ((LinkButton)e.Row.FindControl("lbtnDel")).Attributes.Add("onclick", "return confirm('确定删除分类吗？');");
        }
    }

    protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = e.CommandArgument.ToArrowInt();
        string command = e.CommandName;
        if(command== "DataUpdate")
        {
            GridViewRow drv = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent));
            string catName = (gvData.Rows[drv.RowIndex].FindControl("tbName") as TextBox).Text;
            string order = (gvData.Rows[drv.RowIndex].FindControl("tbSort") as TextBox).Text;

            var model = LineBLL.SelectLineCat(id);
            if (model != null)
            {
                if (!catName.IsNullOrEmpty())
                    model.Name = catName;
                model.SortOrder = order.ToArrowInt();
                LineBLL.UpdateLineCat(model);
            }

        }
        else if(command== "DataDel")
        {
            var model = LineBLL.SelectLineCat(id);
            if (model != null)
            {
                model.IsDel = 1;
                LineBLL.UpdateLineCat(model);
            }
        }

        BindData();
        MessageBox.Show("操作成功！");

    }



    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        int parentID = ddlParent.SelectedValue.ToArrowInt();
        string catName = tbName.Text.Trim();
        int sortOrder = tbSortOrder.Text.Trim().ToArrowInt();
        if (catName.ValidateIsNullOrEmpty("请输入分类名称！"))
            return;

        var model = new TMS.LineCatInfo();
        model.IsDel = 0;
        model.Name = catName;
        model.Remarks = "";
        model.SortOrder = sortOrder;
        model.ParentID = parentID;
        model.IsHotSell = 0;
        model.IsHotSort = 0;
        model.IsSearchWord = 0;
        model.IsShowIndex = 0;
        model.HitTimes = 0;
        LineBLL.AddLineCat(model);

        tbName.Text = "";
        tbSortOrder.Text = "";
        string id = ddlParent.SelectedValue;
        BindParent();
        ddlParent.SelectedValue = id;
        BindData();
        MessageBox.Show("添加成功！");

    }

    protected void ddlShow_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void btnUpdateAll_Click(object sender, EventArgs e)
    {
        foreach(GridViewRow row in gvData.Rows)
        {
            if(row.RowType==DataControlRowType.DataRow)
            {
                string newCatName = (row.FindControl("tbName") as TextBox).Text.Trim();
                int order = (row.FindControl("tbSort") as TextBox).Text.Trim().ToArrowInt();
                int id = (row.FindControl("hfID") as HiddenField).Value.Trim().ToArrowInt();
                int isHotSell = (row.FindControl("chkHot") as CheckBox).Checked ? 1 : 0;
                int isShowIndex= (row.FindControl("chkIndexShow") as CheckBox).Checked ? 1 : 0;
                int isHotSort = (row.FindControl("chkHotSort") as CheckBox).Checked ? 1 : 0;

                var model = LineBLL.SelectLineCat(id);
                if(model!=null)
                {
                    if (!newCatName.IsNullOrEmpty())
                        model.Name = newCatName;
                    model.SortOrder = order;
                    model.IsHotSell = isHotSell;
                    model.IsShowIndex = isShowIndex;
                    model.IsHotSort = isHotSort;
                    LineBLL.UpdateLineCat(model);
                }
            }
        }

        BindData();
        MessageBox.Show("更新成功！");
       

    }
}