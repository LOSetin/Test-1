using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TMS;

public partial class Mall : UIBase
{
    protected string ShowGoodCats()
    {
        return UIHelper.ShowGoodCats();
    }

    protected List<GoodsInfo> getGoods(string condition,string orderBy,int pageIndex,int pageSize)
    {
        return new Goods().SelectList(condition, orderBy, pageIndex, pageSize);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}