using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TMS;

public partial class SiteTest_SetFav : SiteBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (CurrentMember == null)
            Response.Redirect("Login.aspx");
        else
        {
            int id = GetUrlInt("lineID");
            var list = MemberBLL.SelectLineFavs(id, CurrentMember.UserName);
            if(list.Count==0)
            {
                MemberFavInfo model = new MemberFavInfo();
                model.AddTime = DateTime.Now;
                model.FavObjID = id.ToString();
                model.FavType = FavType.Line;
                model.UserName = CurrentMember.UserName;
                MemberBLL.AddFav(model);
            }
            Response.Redirect("MyFav.aspx");

        }
    }
}