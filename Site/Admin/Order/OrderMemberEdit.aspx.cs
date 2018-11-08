using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework;
using Arrow.Framework.Extensions;
using TMS;
using System.Data;
using Arrow.Framework.WebControls;

public partial class GroupMemberEdit : AdminBase
{
    protected string OrderNum { get { return GetUrlString("OrderNum"); } }
    protected string MemberUserName { get { return GetUrlString("MemberUserName"); } }
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            BindData();
            AddSubNav("订单管理-OrderManager.aspx");
            var model = OrderBLL.GetOrderView(OrderNum);
            if(model!=null)
            {
                ltMsg.Text = "当前处理的订单号：" + model.OrderNum + "&nbsp;&nbsp;下单人：" + model.AddMemberRealName + "&nbsp;&nbsp;参团人数：" + model.BuyNum;
            }

        }
       
    }



    protected void BindData()
    {
        int pageIndex = GetUrlInt("p");
        if (pageIndex <= 0) pageIndex = 1;
        WebPager pager = SiteSetting.CreateWebPagerForGridView(gvData, pageIndex);
        pager.PageCSSName = "hide";
        pager.PageSize = 10000;

        WebQuery query = new WebQuery();
        query.Fields = OrderDetailInfo.AllFields;
        query.OrderBy = "ID";
        query.PrimaryKey = OrderDetailInfo.TablePrimaryKey;
        query.SqlCreateType = ControlSqlCreateType.RowNum;
        query.TableName = OrderDetailInfo.TableOrViewName;
        query.Condition = "OrderNum='" + OrderNum + "'";

        gvData.Db = TMS.Db.Helper;
        gvData.Query = query;
        gvData.CreateDataSource();
        gvData.DataBind();

    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        List<OrderDetailInfo> details = OrderBLL.GetAllDetails(OrderNum,MemberUserName);
        if (details.Count == 0)
        {
            BindData();
            return;
        }

        foreach (GridViewRow row in gvData.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                int id = (row.FindControl("hfID") as HiddenField).Value.ToArrowInt();
                string realName = (row.FindControl("tbRealName") as TextBox).Text.Trim();
                string sex = (row.FindControl("ddlSex") as DropDownList).SelectedValue.ToArrowString();
                string idNum = (row.FindControl("tbIDNum") as TextBox).Text.Trim();
                string mobile = (row.FindControl("tbPhone") as TextBox).Text.Trim();

                OrderDetailInfo model = details.Find(s => s.ID == id);
                if (model != null)
                {
                    model.RealName = realName;
                    model.Sex = sex;
                    model.IDNum = idNum;
                    model.MobileNum = mobile;
                    OrderBLL.UpdateDetail(model);
                }

            }
        }

        MessageBox.Show("更新成功！");
        BindData();
    }

    protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string sex = (e.Row.FindControl("hfSex") as HiddenField).Value.ToArrowString();
            (e.Row.FindControl("ddlSex") as DropDownList).SelectedValue = sex;
        }
    }






    protected void btnReturn_Click(object sender, EventArgs e)
    {
        RedirectToReturnUrl("OrderManager.aspx");
    }
}