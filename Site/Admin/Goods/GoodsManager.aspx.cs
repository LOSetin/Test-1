using Arrow.Framework.WebControls;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework.Extensions;
using Arrow.Framework;

public partial class GoodsManager : AdminBase
{ 
    protected void Page_Load(object sender, EventArgs e)
    {
        SetNav("商品管理");
        if(!Page.IsPostBack)
        {
            GoodsBLL.BindGoodsCat(ddlCat);
            BindData();
        }
    }


    protected void BindData()
    {
        string condition = "1=1";
        string keyword = GetUrlString("keyword");
        int catID = GetUrlInt("CatID");
        if(!keyword.IsNullOrEmpty())
        {
            condition = condition + " And Name like '%" + keyword + "%'";
        }

        if(catID>0)
        {
            condition = condition + " And CatID=" + catID;
        }

        AdminSetting.CreateWebPagerForGridView(gvData, ArrowControlPageIndex);

        WebQuery query = new WebQuery();
        query.Ascending = false;
        query.Fields = "*";
        query.OrderBy = "ID desc,IsOut asc";
        query.PrimaryKey = "ID";
        query.SqlCreateType = ControlSqlCreateType.RowNum;
        query.TableName = "V_Goods";
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
            ((LinkButton)e.Row.FindControl("lbtnDel")).Attributes.Add("onclick", "return confirm('确定下架该商品吗？');");
            ((LinkButton)e.Row.FindControl("lbtnRestore")).Attributes.Add("onclick", "return confirm('确定上架该商品吗？');");
        }
    }

    protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string commandName = e.CommandName;
        int id = e.CommandArgument.ToArrowInt();
        if (e.CommandName == "UpdateData")
        {
            Response.Redirect("GoodsEdit.aspx?id=" + id + CreateReturnUrl("&"));
        }
        else if (e.CommandName == "SetOut")
        {
            //下架
            GoodsBLL.ChangeGoodsStatus(id, 1);
        }
        else if (e.CommandName == "Restore")
        {
            //上架
            GoodsBLL.ChangeGoodsStatus(id, 0);
        }
        BindData();
        MessageBox.Show("操作成功！");
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        Response.Redirect("GoodsManager.aspx?keyword=" + Server.UrlEncode(tbKeyWord.Text.Trim()) + "&CatID=" + ddlCat.SelectedValue);
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        Response.Redirect("GoodsEdit.aspx");
    }
}