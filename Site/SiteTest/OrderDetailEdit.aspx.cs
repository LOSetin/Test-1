using Arrow.Framework.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TMS;
using Arrow.Framework.Extensions;
using Arrow.Framework;

public partial class OrderDetailEdit : SiteBase
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
        query.TableName = OrderDetailInfo.TableOrViewName ;
        query.Condition = "AddMemberName='" + CurrentMember.UserName + "' And OrderNum='" + OrderNum + "'";

        gvData.Db = TMS.Db.Helper;
        gvData.Query = query;
        gvData.CreateDataSource();
        gvData.DataBind();
      
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (CurrentMember == null)
            Response.Redirect("Login.aspx");
        ShowTitle("编辑参团人");
        if(!Page.IsPostBack)
        {
            BindData();
            TravelOrderInfo order = OrderBLL.SelectOrder(OrderNum);
            if(order!=null)
            {
                LineInfo line = LineBLL.SelectLine(order.LineID);
                if(line!=null)
                {
                    TravelGroupInfo group = GroupBLL.SelectGroup(order.GroupID);
                    if(group!=null)
                    {
                        ltBuyNum.Text = order.BuyNum.ToString();
                        ltLineName.Text = line.Name.ToString();
                        ltPrice.Text = group.InnerPrice.ToString();
                        ltTotal.Text = order.TotalMoney.ToString();
                        PromotionInfo promotion = PromotionBLL.Select(order.PromotionID);
                        if(promotion!=null)
                        {
                            ltPromotionName.Text = promotion.Name;
                        }
                        else
                        {
                            ltPromotionName.Text = "无";
                        }
                    }
                }
            }
        }

    }

    protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string command = e.CommandName.ToString();
        int id = e.CommandArgument.ToArrowInt();
       
        
      
        BindData();
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        List<OrderDetailInfo> details = OrderBLL.GetAllDetails(OrderNum, CurrentMember.UserName);
        if(details.Count==0)
        {
            BindData();
            return;
        }
        
        foreach(GridViewRow row in gvData.Rows)
        {
            if(row.RowType==DataControlRowType.DataRow)
            {
                int id = (row.FindControl("hfID") as HiddenField).Value.ToArrowInt();
                string realName = (row.FindControl("tbRealName") as TextBox).Text.Trim();
                string sex = (row.FindControl("ddlSex") as DropDownList).SelectedValue.ToArrowString();
                string idNum = (row.FindControl("tbIDNum") as TextBox).Text.Trim();
                string mobile = (row.FindControl("tbPhone") as TextBox).Text.Trim();

                OrderDetailInfo model = details.Find(s => s.ID == id);
                if(model!=null)
                {
                    model.RealName = realName;
                    model.Sex = sex;
                    model.IDNum = idNum;
                    model.MobileNum = mobile;
                    //OrderBLL.UpdateDetail(model);
                }

            }
        }

       

        MessageBox.Show("更新成功！");
        BindData();
    }

    protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if(e.Row.RowType==DataControlRowType.DataRow)
        {
            string sex = (e.Row.FindControl("hfSex") as HiddenField).Value.ToArrowString();
            (e.Row.FindControl("ddlSex") as DropDownList).SelectedValue = sex;
        }
    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("MyOrder.aspx");
    }
}