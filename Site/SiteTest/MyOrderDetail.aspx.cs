using Arrow.Framework.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TMS;
using Arrow.Framework.Extensions;

public partial class SiteTest_MyOrderDetail : SiteBase
{
    protected string OrderNum { get { return GetUrlString("OrderNum"); } }

    protected void BindData()
    {
        int pageIndex = GetUrlInt("p");
        if (pageIndex <= 0) pageIndex = 1;
        WebPager pager = SiteSetting.CreateWebPagerForGridView(gvData, pageIndex);
        pager.PageCSSName = "NotShowPage";
        pager.PageSize = 10000;

        WebQuery query = new WebQuery();
        query.Fields = OrderDetailInfo.AllFields;
        query.OrderBy = "ID";
        query.PrimaryKey = OrderDetailInfo.TablePrimaryKey;
        query.SqlCreateType = ControlSqlCreateType.RowNum;
        query.TableName = OrderDetailInfo.TableOrViewName;
        query.Condition = "AddMemberName='" + CurrentMember.UserName + "' And OrderNum='" + OrderNum + "'";

        gvData.Db = TMS.Db.Helper;
        gvData.Query = query;
        gvData.CreateDataSource();
        gvData.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ShowTitle("订单详细信息");
        if (!Page.IsPostBack)
            BindData();
        V_OrderInfo model = OrderBLL.GetOrderView(OrderNum);
        if(model!=null)
        {
            ltGoDate.Text = model.GoDate.ToDateOnlyString();
            ltLineName.Text = model.LineName;
            ltPhone.Text = model.AddMemberMobile;
            ltPromotionName.Text = model.PromotionName.IsNullOrEmpty()?"无":model.PromotionName;
            ltRealName.Text = model.AddMemberRealName;
            ltStatus.Text = model.OrderStatus;
            ltTotal.Text = model.TotalMoney.ToString();
        }
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("MyOrder.aspx");
    }
}