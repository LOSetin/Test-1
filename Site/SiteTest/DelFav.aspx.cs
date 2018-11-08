using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TMS;

public partial class DelFav : SiteBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (CurrentMember == null)
            Response.Redirect("Login.aspx");
        else
        {
            int id = GetUrlInt("LineID");
            MemberBLL.DelFav(CurrentMember.UserName, FavType.Line, id.ToString());
            Response.Redirect("MyFav.aspx");

        }
    }
}