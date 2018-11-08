using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TMS;
using Arrow.Framework.Extensions;
using Arrow.Framework;

public partial class ConfirmChange : SiteBase
{
   


    protected void Page_Load(object sender, EventArgs e)
    {
        if (CurrentMember == null)
            Response.Redirect("Login.aspx");
        ShowTitle("兑换");
        if(!Page.IsPostBack)
        {
            var model = MemberBLL.Select(CurrentMember.UserName);
            if(model!=null)
            {
                ltPoints.Text = (model.TotalPoints - model.UsedPoints).ToString();
            }
            var goods = GoodsBLL.SelectGoods(GetUrlInt("GoodsID"));
            if(goods!=null)
            {
                ltGoodsName.Text = goods.Name;
                ltPointsCost.Text = goods.Points.ToString();
                ltGoodsNum.Text = goods.Num.ToString();
            }
        }

    }

    protected void Change_Click(object sender, EventArgs e)
    {
        int num = tbNum.Text.Trim().ToArrowInt();
        if(num<=0)
        {
            MessageBox.Show("购买数量不正确！");
            return;
        }
        string msg = "";
        bool success = MemberBLL.ExchangeGoods(CurrentMember.UserName, GetUrlInt("GoodsID"), num,"","","", out msg);
        if(success)
        {
            MessageBox.Show("兑换成功！", CurrentUrl);
        }
        else
        {
            Response.Write(msg);
        }
           

    }

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        Response.Redirect("MyChange.aspx");
    }
}