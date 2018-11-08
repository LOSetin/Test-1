using Arrow.Framework.WebControls;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework.Extensions;
using Arrow.Framework;
using TMS;
using System.Data;
using System.Collections.Generic;

public partial class PromotionGroupManager : AdminBase
{
    protected PromotionInfo MyPromotion { get { return PromotionBLL.Select(GetUrlInt("PromotionID")); } }

    protected void Page_Load(object sender, EventArgs e)
    {
        SetNav("促销团管理");
        AddSubNav("促销管理-PromotionManager.aspx");
        if(!Page.IsPostBack)
        {
            BindData();
            ltTips.Text = MyPromotion == null ? "" : MyPromotion.Name;
            ltTitle.Text = ltTips.Text;
        }
    }


    protected void BindData()
    {
        string condition = "PromotionID=" + MyPromotion.ID;

        DateTime dtBegin = GetUrlDateTime("bdate");
        DateTime dtEnd = GetUrlDateTime("edate");
        if(dtBegin!=DateTime.MinValue && dtEnd!=DateTime.MinValue && dtEnd>dtBegin)
        {
            condition = condition + " And GoDate>='" + dtBegin.ToStartString() + "' And GoDate<='" + dtEnd.ToEndString() + "'";
        }

        AdminSetting.CreateWebPagerForGridView(gvData, ArrowControlPageIndex);

        WebQuery query = new WebQuery();
        query.Fields = "*";
        query.OrderBy = "ID";
        query.PrimaryKey = "ID";
        query.SqlCreateType = ControlSqlCreateType.RowNum;
        query.TableName = "V_Promotion_Group";
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
        }
    }

    protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string commandName = e.CommandName;
        int id = e.CommandArgument.ToArrowInt();
        if (e.CommandName == "UpdateData")
        {
            GridViewRow drv = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent));
            int num = (gvData.Rows[drv.RowIndex].FindControl("tbNum") as TextBox).Text.Trim().ToArrowInt();
            if ((num == 0).ValidateSuccess("促销的团位数无效！"))
            {
                BindData();
                return;
            }
            PromotionBLL.UpdateTotalNumOfPromotionGroup(num, id);
            BindData();
            MessageBox.Show("修改成功！");
        }
        else if (e.CommandName == "DelData")
        {
            //删除，判断是否已经有人报名
            PromotionGroupInfo model = PromotionBLL.SelectGroupOfPromotion(id);
            if(model==null)
            {
                BindData();
                return;
            }
            if((model.SelledNum>0).ValidateSuccess("已经有人报名了，不可删除该促销！"))
            {
                BindData();
                return;
            }
            PromotionBLL.DelGroupOfPromotion(id);
            BindData();
            MessageBox.Show("删除成功！");
        }

    }

    


    protected void btnReturn_Click(object sender, EventArgs e)
    {
        RedirectToReturnUrl("PromotionManager.aspx");
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        Response.Redirect("PromotionGroupManager.aspx?PromotionID=" + MyPromotion.ID + "&bdate=" + Server.UrlEncode(tbBegin.Text.Trim()) + "&edate=" + Server.UrlEncode(tbEnd.Text.Trim()));
    }
}