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

public partial class ConfirmExChange : MemberBase
{
    protected GoodsInfo MyGoods = null;

    protected void BindBuyNum()
    {
        ddlBuyNum.Items.Clear();
        for (int i = 1; i <= MyGoods.Num; i++)
        {
            ddlBuyNum.Items.Add(i.ToString());
        }
    }

    protected override void OnInit(EventArgs e)
    {
       
        base.OnInit(e);
        int goodsID = GetUrlInt("GoodsID");
        if (goodsID > 0)
        {
            MyGoods = GoodsBLL.SelectGoods(goodsID);
        }
        
        BindBuyNum();


    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (CurrentMember == null)
            Response.Redirect("MemberLogin.aspx?ReturnUrl=" + Server.UrlEncode(CurrentUrl));

        if (MyGoods == null)
            JsBox.Alert("商品不存在！", "-1");
        if (MyGoods.IsOut == 1)
            JsBox.Alert("商品已下架！", "-1");
     
        if (!Page.IsPostBack)
        {
            tbRealName.Text = CurrentMember.RealName;
            tbPhone.Text = CurrentMember.Phone;
            
        }
        
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string realName = tbRealName.Text.Trim();
        string phone = tbPhone.Text.Trim();
        string address = tbAddress.Text.Trim();
        int buyNum = ddlBuyNum.SelectedValue.ToArrowInt();

        if (realName.ValidateIsNullOrEmpty("请输入联系人姓名！"))
            return;
        if (phone.ValidateIsNullOrEmpty("请输入联系电话！"))
            return;
        if (address.ValidateIsNullOrEmpty("请输入收获地址！"))
            return;
        if ((buyNum == 0).ValidateSuccess("兑换数量必须大于0"))
            return;

        string msg = "";
        bool success = MemberBLL.ExchangeGoods(CurrentMember.UserName, MyGoods.ID, buyNum, address, realName, phone,out msg);
        if(success)
        {
            JsBox.Alert("申请兑换成功！", "MemberExchange.aspx");
        }
        else
        {
            JsBox.Alert(msg);
        }
      
    }
}