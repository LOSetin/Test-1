using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TMS;

public partial class MyFav : SiteBase
{
   

    protected List<LineInfo> GetLines()
    {
        string condition = "IsDel=0";
        List<MemberFavInfo> myFavs = MemberBLL.SelectLineFavs(CurrentMember.UserName);
        string ids = "";
        foreach(MemberFavInfo mode in myFavs)
        {
            ids = ids + mode.FavObjID + ",";
        }
        if(ids.EndsWith(","))
        {
            ids = ids.Substring(0, ids.Length - 1);
            condition = condition + " And ID in(" + ids + ")";
        }
        else
        {
            condition = condition + " And 1=2";
        }
        return LineBLL.SelectLineList(condition);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (CurrentMember == null)
            Response.Redirect("Login.aspx");
        ShowTitle("我的收藏");

    }
}