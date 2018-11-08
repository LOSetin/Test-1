using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework;

public partial class GoodsDetail : UIBase
{
    protected TMS.GoodsInfo CurrentGoods = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        CurrentGoods = GoodsBLL.SelectGoods(GetUrlInt("id"));
        if (CurrentGoods == null || CurrentGoods.IsOut == 1)
            MessageBox.Show("该商品不存在或已下架！", true);

        ddlNum.Items.Clear();
        for(int i=1;i<=CurrentGoods.Num;i++)
        {
            ddlNum.Items.Add(i.ToString());
        }
            
    }
}