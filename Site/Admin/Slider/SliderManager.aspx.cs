using Arrow.Framework.WebControls;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework.Extensions;
using Arrow.Framework;

public partial class SliderManager : AdminBase
{ 
    protected void Page_Load(object sender, EventArgs e)
    {
        SetNav("幻灯管理");
        btnUpdateAll.Attributes.Add("onclick", "return confirm('确定更新所有吗？')");
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
            condition = condition + " And Name like '%" + keyword + "%'";
        }


        AdminSetting.CreateWebPagerForGridView(gvData, ArrowControlPageIndex);

        WebQuery query = new WebQuery();
        query.Ascending = false;
        query.Fields = "*";
        query.OrderBy = "SortOrder asc";
        query.PrimaryKey = "ID";
        query.SqlCreateType = ControlSqlCreateType.RowNum;
        query.TableName = "Slider";
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
            ((LinkButton)e.Row.FindControl("lbtnDel")).Attributes.Add("onclick", "return confirm('确定删除该幻灯吗？');");
        }
    }

    protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string commandName = e.CommandName;
        int id = e.CommandArgument.ToArrowInt();
        TMS.Slider bll = new TMS.Slider();
        if (e.CommandName == "UpdateData")
        {
            GridViewRow drv = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent));
            string picName = (gvData.Rows[drv.RowIndex].FindControl("tbName") as TextBox).Text;
            int sort = (gvData.Rows[drv.RowIndex].FindControl("tbSort") as TextBox).Text.ToArrowInt();
            var model = bll.Select(id);
            if(model!=null)
            {
                model.Name = picName;
                model.SortOrder = sort;
                bll.Update(model);
            }
        }
        else if (e.CommandName == "DelData")
        {
            //删除
            new TMS.Slider().Delete(id);
        }
       
        BindData();
        MessageBox.Show("操作成功！");
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        Response.Redirect("SliderManager.aspx?keyword=" + Server.UrlEncode(tbKeyWord.Text.Trim()));
    }



    protected void btnUpdateAll_Click(object sender, EventArgs e)
    {
        TMS.Slider bll = new TMS.Slider();
        foreach (GridViewRow row in gvData.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                int id = (row.FindControl("hfID") as HiddenField).Value.Trim().ToArrowInt();
                string name = (row.FindControl("tbName") as TextBox).Text.Trim();
                int order = (row.FindControl("tbSort") as TextBox).Text.Trim().ToArrowInt();
                var model = bll.Select(id);
                if (model != null)
                {
                    model.Name = name;
                    model.SortOrder = order;
                    bll.Update(model);
                }
            }
        }

        BindData();
        MessageBox.Show("更新成功！");
    }
}