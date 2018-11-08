using Arrow.Framework.WebControls;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework.Extensions;
using Arrow.Framework;

public partial class PromotionManager : AdminBase
{ 
    protected void Page_Load(object sender, EventArgs e)
    {
        SetNav("促销管理");
        if(!Page.IsPostBack)
        {
            BindData();
        }
    }


    protected void BindData()
    {
        string condition = "IsDel=0";
        string keyword = GetUrlString("keyword");
        if(!keyword.IsNullOrEmpty())
        {
            condition = condition + " And Name like '%" + keyword + "%'";
        }


        AdminSetting.CreateWebPagerForGridView(gvData, ArrowControlPageIndex);

        WebQuery query = new WebQuery();
        query.Ascending = false;
        query.Fields = TMS.PromotionInfo.AllFields;
        query.OrderBy = "ID desc";
        query.PrimaryKey = TMS.PromotionInfo.TablePrimaryKey;
        query.SqlCreateType = ControlSqlCreateType.RowNum;
        query.TableName = TMS.PromotionInfo.TableOrViewName;
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
            ((LinkButton)e.Row.FindControl("lbtnDel")).Attributes.Add("onclick", "return confirm('确定删除该促销吗？');");
        }
    }

    protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int id = e.CommandArgument.ToArrowInt();
        if (e.CommandName == "UpdateData")
        {
            Response.Redirect("PromotionEdit.aspx?id=" + id + CreateReturnUrl("&"));
        }
        else if (e.CommandName == "DelData")
        {
            //删除
            PromotionBLL.SetIsDel(id, 1);
            BindData();
            MessageBox.Show("删除成功！");
        }
       else if(e.CommandName== "AddPromotion")
        {
            Response.Redirect("GroupList.aspx?PromotionID=" + id + CreateReturnUrl("&"));
        }
       else if(e.CommandName== "GroupManager")
        {
            Response.Redirect("PromotionGroupManager.aspx?PromotionID="+id+CreateReturnUrl("&"));
        }
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        Response.Redirect("PromotionManager.aspx?keyword=" + Server.UrlEncode(tbKeyWord.Text.Trim()));
    }

   
}