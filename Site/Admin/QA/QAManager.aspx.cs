using Arrow.Framework.WebControls;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework.Extensions;
using Arrow.Framework;
using TMS;
using System.Data;
using System.Collections.Generic;

public partial class QAManager : AdminBase
{

    protected void Page_Load(object sender, EventArgs e)
    {
        SetNav("疑难解答管理");
        if(!Page.IsPostBack)
        {
            QACat.BindToDrowdownList(ddlCat, true);
            BindData();
        }
    }

   

    protected void BindData()
    {
        string condition = "1=1";
        string keyword = GetUrlString("keyword");
        string cat = GetUrlString("cat");
        if(!keyword.IsNullOrEmpty())
        {
            condition = condition + " And QATitle like '%" + keyword + "%'";
        }

        if(!cat.IsNullOrEmpty() && cat!="所有")
        {
            condition = condition + " And CatName='" + cat + "'";
        }
        

        AdminSetting.CreateWebPagerForGridView(gvData, ArrowControlPageIndex);

        WebQuery query = new WebQuery();
        query.Fields = QAInfo.AllFields;
        query.OrderBy = "ID desc";
        query.PrimaryKey = QAInfo.TablePrimaryKey;
        query.SqlCreateType = ControlSqlCreateType.RowNum;
        query.TableName = QAInfo.TableOrViewName;
        query.Condition = condition;

        gvData.Db = TMS.Db.Helper;
        gvData.Query = query;
        gvData.CreateDataSource();
        gvData.DataBind();

        tbKeyWord.Text = keyword;
        ddlCat.SelectedValue = cat;

    }

    protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((LinkButton)e.Row.FindControl("lbtnDel")).Attributes.Add("onclick", "return confirm('确定删除该文章吗？');");
        }
    }

    protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string commandName = e.CommandName;
        int id = e.CommandArgument.ToArrowInt();
        if (e.CommandName == "DelData")
        {
            //删除
            new TMS.QA().Delete(id);
            BindData();
            MessageBox.Show("删除成功！");
        }
        else if(e.CommandName== "UpdateData")
        {
            Response.Redirect("QAEdit.aspx?id=" + id + CreateReturnUrl("&"));
        }
      
       
        
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        Response.Redirect("QAManager.aspx?keyword=" + Server.UrlEncode(tbKeyWord.Text.Trim()) + "&Cat=" + ddlCat.SelectedValue);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("QAEdit.aspx");
    }

}