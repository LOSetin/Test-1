using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Arrow.Framework;
using Arrow.Framework.Extensions;
using System.Web.UI.WebControls;
using System.Text;

/// <summary>
///会员后台基类
/// </summary>
public class MemberBase : UIBase
{
   

    protected override void OnLoad(EventArgs e)
    {
        if(CurrentMember==null)
        {
            Response.Redirect("Default.aspx");
        }
        base.OnLoad(e);
    }

    /// <summary>
    /// 显示左侧菜单
    /// </summary>
    /// <returns></returns>
    protected string ShowMenu(string url = "")
    {
        string menuFormat = "<li class='{0}'><a href='{1}'>{2}</a></li>";
        List<MemberMenuInfo> menus = new List<MemberMenuInfo>() {
            new MemberMenuInfo("我的信息","MemberIndex.aspx"),
            new MemberMenuInfo("我的订单","MemberOrder.aspx"),
            new MemberMenuInfo("我的兑换","MemberExchange.aspx"),
            new MemberMenuInfo("我的消费","MemberCost.aspx"),
            new MemberMenuInfo("线路收藏","MemberFav.aspx"),
            new MemberMenuInfo("修改资料","MemberModifyData.aspx"),
            new MemberMenuInfo("修改密码","MemberModifyPwd.aspx"),
            new MemberMenuInfo("退出登录","MemberLogout.aspx")
        };

        string pathAndQuery = HttpHelper.GetCurrentUrlPathAndQuery().Split('?')[0];
        string currentUrl = pathAndQuery.Substring(pathAndQuery.LastIndexOf("/") + 1).ToLower();
        string returnUrl = "";
        if (!CurrentReturnUrl.IsNullOrEmpty())
            returnUrl = CurrentReturnUrl.Substring(CurrentReturnUrl.LastIndexOf("/") + 1).ToLower();
        
        StringBuilder sb = new StringBuilder();
        foreach (MemberMenuInfo menu in menus)
        {
            string cls = "";
            if (currentUrl.StartsWith(menu.Url.ToLower()) || returnUrl.StartsWith(menu.Url.ToLower()))
                cls = "no";
            sb.AppendLine(string.Format(menuFormat, cls, menu.Url, menu.Title));
        }


        return sb.ToString();
    }

}