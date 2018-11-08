using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TMS;
using Arrow.Framework.Extensions;
using Arrow.Framework;
using Arrow.Framework.WebControls;

public partial class MemberOrderPeopleEdit : MemberBase
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

        gvData.ShowHorizionalScroll = false;
        gvData.Db = TMS.Db.Helper;
        gvData.Query = query;
        gvData.CreateDataSource();
        gvData.DataBind();

    }

    protected override void OnInit(EventArgs e)
    {
        gvData.ShowHorizionalScroll = false;
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
            BindData();
    }

    protected void tbUpdate_Click(object sender, EventArgs e)
    {
        List<OrderDetailInfo> details = OrderBLL.GetAllDetails(OrderNum, CurrentMember.UserName);

        bool isOK = true; ;
        foreach (GridViewRow row in gvData.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                int id = (row.FindControl("hfID") as HiddenField).Value.ToArrowInt();
                string realName = (row.FindControl("tbRealName") as TextBox).Text.Trim();
                string sex = (row.FindControl("ddlSex") as DropDownList).SelectedValue.ToArrowString();
                string idNum = (row.FindControl("tbIDNum") as TextBox).Text.Trim();
                string mobile = (row.FindControl("tbPhone") as TextBox).Text.Trim();

                if(realName.IsNullOrEmpty()||sex.IsNullOrEmpty()||idNum.IsNullOrEmpty()||mobile.IsNullOrEmpty())
                {
                    isOK = false;
                    break;
                }

                OrderDetailInfo model = details.Find(s => s.ID == id);
                if (model != null)
                {
                    model.RealName = realName;
                    model.Sex = sex;
                    model.IDNum = idNum;
                    model.MobileNum = mobile;
                }

            }
        }

        //检查填写是否完整
        if (!isOK)
        {
            MessageBox.Show("资料填写不完整！");
        }
        else
        {
            //更新资料
            //订单状态设为等待商家确认
            string errMsg = "";
            bool success = OrderBLL.UpdateTeammateAndSubmit(OrderNum, CurrentMember.UserName, details, out errMsg);
            if(success)
            {
                MessageBox.Show("操作成功，请等待商家确认！", "MemberOrder.aspx");
            }
            else
            {
                MessageBox.Show(errMsg);
            }

        }
    }
}