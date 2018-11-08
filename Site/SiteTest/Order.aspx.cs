using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TMS;
using Arrow.Framework.Extensions;
using System.Data.Common;
using Arrow.Framework;

public partial class SiteTest_Order : SiteBase
{
    protected PromotionInfo MyPromotion = null;
    protected TravelGroupInfo MyGroup = null;
    protected LineInfo MyLine = null;

    protected void BindBuyNum()
    {
        ddlBuyNum.Items.Clear();
        for(int i=1;i<11;i++)
        {
            ddlBuyNum.Items.Add(i.ToString());
        }
    }

    protected override void OnInit(EventArgs e)
    {
        BindBuyNum();
        base.OnInit(e);
        int promotionID = GetUrlInt("PromotionID");
        int groupID = GetUrlInt("GroupID");
        int lineID = GetUrlInt("LineID");
        if(promotionID>0)
        {
            MyPromotion = PromotionBLL.Select(promotionID);
        }
        if (groupID > 0)
            MyGroup = GroupBLL.SelectGroup(groupID);
        if (lineID > 0)
            MyLine = LineBLL.SelectLine(lineID);

        if (promotionID == 0)
        {
            ltPrice.Text = MyGroup.InnerPrice.ToString();
            ltTotal.Text = ltPrice.Text;
        }
        else
        {
            if (MyPromotion.PromotionType == PromotionType.Discount)
            {
                ltPrice.Text = decimal.Round(MyGroup.InnerPrice * MyPromotion.Discount, 2).ToString();
                ltTotal.Text = ltPrice.Text;
            }
            else if (MyPromotion.PromotionType == PromotionType.Bundle)
            {
                ltPrice.Text = MyPromotion.TotalPayOneTime.ToString();
                ddlBuyNum.SelectedValue = MyPromotion.TotalPayOneTimeJoinNum.ToString();
                ddlBuyNum.Enabled = false;
                ltTotal.Text = MyPromotion.TotalPayOneTime.ToString();
            }
        }

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (CurrentMember == null)
            Response.Redirect("Login.aspx");
        ShowTitle("填写订单，步骤一");
        if(!Page.IsPostBack)
        {
            tbRealName.Text = CurrentMember.RealName;
            tbPhone.Text = CurrentMember.Phone;
            if (MyPromotion != null)
                ltPromotion.Text = MyPromotion.Name;
            else
                ltPromotion.Text = "无";
            ltLineName.Text = MyLine.Name;
            ltGoDate.Text = MyGroup.GoDate.ToString();

        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string realName = tbRealName.Text.Trim();
        string phone = tbPhone.Text.Trim();
        int buyNum = ddlBuyNum.SelectedValue.ToArrowInt();

        if (realName.ValidateIsNullOrEmpty("请输入联系人姓名！"))
            return;
        if (phone.ValidateIsNullOrEmpty("请输入联系电话！"))
            return;
        if ((buyNum == 0).ValidateSuccess("购买团位总数必须大于0"))
            return;

        SiteUserInfo ui = SiteUserBLL.SelectUserByInviteNum(CurrentMember.InviteNum);
        string inviteNum = ui == null ? "0001" : ui.InviteNum;
        string inviterRealName = ui == null ? "超级管理员" : ui.RealName;
        string inviterUserName = ui == null ? "admin" : ui.Name;

        TravelOrderInfo model = new TravelOrderInfo();
        model.AddMemberMobile = phone;
        model.AddMemberName = CurrentMember.UserName;
        model.AddMemberRealName = realName;
        model.AddMemberRemarks = "";
        model.AddTime = DateTime.Now;
        model.BuyNum = buyNum;
        model.CompanyRemarks = "";
        model.InviteNum = CurrentMember.InviteNum;
        model.InviterRealName = inviterRealName;
        model.InviterUserName = inviterUserName;
        model.MoneyPayed = 0M;
        model.MoneyReturn = 0M;
        model.OperatorRealName = "";
        model.OperatorUserName = "";
        model.OrderNum = OrderNumFactory.NextNum();
        model.OrderStatus = OrderStatus.Submited;
        model.OrderType = "";
        model.PromotionID = MyPromotion == null ? 0 : MyPromotion.ID;
        model.GroupID = MyGroup.ID;
        model.LineID = MyLine.ID;
        model.TotalMoney = ltTotal.Text.ToArrowDecimal();
        model.CanChangeNum = 1;
        if (MyPromotion != null && MyPromotion.PromotionType == PromotionType.Bundle)
            model.CanChangeNum = 0;

        string msg = "";
        bool success = false;
        using (DbConnection conn = Db.Helper.CreateConnection() )
        {
            conn.ConnectionString = Db.Helper.ConnectionString;
            conn.Open();
            using (DbTransaction tran = conn.BeginTransaction())
            {
                try
                {
                    OrderBLL.FastAddOrder(model, tran);
                    tran.Commit();
                    success = true;
                }
                catch(Exception ex)
                {
                    tran.Rollback();
                    msg = ex.Message;
                }
            }

        }

        if (success)
            Response.Redirect("MyOrder.aspx");
        else
            MessageBox.Show(msg);

    }

    protected void ddlBuyNum_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (MyPromotion == null)
            ltTotal.Text = decimal.Round(ltPrice.Text.ToArrowDecimal() * ddlBuyNum.SelectedValue.ToArrowDecimal(),2).ToString();
        else if(MyPromotion.PromotionType==PromotionType.Discount)
            ltTotal.Text = decimal.Round(ltPrice.Text.ToArrowDecimal() * ddlBuyNum.SelectedValue.ToArrowDecimal(), 2).ToString();

        //动态生成控件

    }
}