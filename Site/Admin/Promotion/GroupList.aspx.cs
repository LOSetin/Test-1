using Arrow.Framework.WebControls;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework.Extensions;
using Arrow.Framework;
using TMS;
using System.Data;
using System.Collections.Generic;

public partial class GroupList : AdminBase
{
    protected PromotionInfo MyPromotion { get { return PromotionBLL.Select(GetUrlInt("PromotionID")); } }

    protected void Page_Load(object sender, EventArgs e)
    {
        SetNav("添加促销团");
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

        string condition = "IsDel=0 And GoDate>'" + DateTime.Now.ToString() + "' And ID not in(Select GroupID from PromotionGroup Where PromotionID=" + MyPromotion.ID + ")";

        DateTime dtBegin = GetUrlDateTime("bdate");
        DateTime dtEnd = GetUrlDateTime("edate");
        if(dtBegin!=DateTime.MinValue && dtEnd!=DateTime.MinValue && dtEnd>dtBegin)
        {
            condition = condition + " And GoDate>='" + dtBegin.ToStartString() + "' And GoDate<='" + dtEnd.ToEndString() + "'";
        }

        AdminSetting.CreateWebPagerForGridView(gvData, ArrowControlPageIndex);

        WebQuery query = new WebQuery();
        query.Fields = "*";
        query.OrderBy = "ID desc";
        query.PrimaryKey = "ID";
        query.SqlCreateType = ControlSqlCreateType.RowNum;
        query.TableName = "V_Group_Line";
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
            ((LinkButton)e.Row.FindControl("lbtnAdd")).Attributes.Add("onclick", "return confirm('确定将该团加入到促销吗？');");
        }
    }

    protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string commandName = e.CommandName;
        int id = e.CommandArgument.ToArrowInt();
        if (e.CommandName == "AddData")
        {
            GridViewRow drv = ((GridViewRow)(((LinkButton)(e.CommandSource)).Parent.Parent));
            int num = (gvData.Rows[drv.RowIndex].FindControl("tbNum") as TextBox).Text.Trim().ToArrowInt();
            if ((num == 0).ValidateSuccess("促销的团位数无效！"))
            {
                BindData();
                return;
            }

            int lineID= (gvData.Rows[drv.RowIndex].FindControl("hfLineID") as HiddenField).Value.Trim().ToArrowInt();
            decimal rawInnerPrice = (gvData.Rows[drv.RowIndex].FindControl("hfInnerPrice") as HiddenField).Value.Trim().ToArrowDecimal();
            decimal rawOuterPrice = (gvData.Rows[drv.RowIndex].FindControl("hfOuterPrice") as HiddenField).Value.Trim().ToArrowDecimal();

            //判断该团是否已加入
            if (PromotionBLL.GroupIsInPromotion(id,MyPromotion.ID).ValidateSuccess("该团已在促销内！"))
            {
                BindData();
                return;
            }

            //开始加入
            var model = new TMS.PromotionGroupInfo();
            model.AddTime = DateTime.Now;
            model.GroupID = id;
            model.LineID = lineID;
            model.PromotionID = MyPromotion.ID;
            model.SelledNum = 0;
            model.TotalNum = num;
            model.RawInnerPrice = rawInnerPrice;
            model.RawOuterPrice = rawOuterPrice;
            model.BeginTime = DateTime.Now;
            model.RemainNum = model.TotalNum - model.SelledNum;
            
            PromotionBLL.AddGroupToPromotion(model);
            BindData();
            MessageBox.Show("添加成功！");
          
        }
     

    }

    

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        RedirectToReturnUrl("PromotionManager.aspx");
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        Response.Redirect("GroupList.aspx?PromotionID=" + MyPromotion.ID + "&bdate=" + Server.UrlEncode(tbBegin.Text.Trim()) + "&edate=" + Server.UrlEncode(tbEnd.Text.Trim()));
    }
}