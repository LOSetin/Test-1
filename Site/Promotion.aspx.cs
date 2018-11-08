using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TMS;

public partial class Promotion : UIBase
{
    protected List<PromotionInfo> GetPromotion(string strWhere, string orderBy, int pageIndex, int pageSize)
    {
        return new TMS.Promotion().SelectList(strWhere, orderBy, pageIndex, pageSize);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}