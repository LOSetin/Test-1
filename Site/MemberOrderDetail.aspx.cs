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

public partial class MemberOrderDetail : MemberBase
{
    protected string OrderNum { get { return GetUrlString("OrderNum"); } }


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

}