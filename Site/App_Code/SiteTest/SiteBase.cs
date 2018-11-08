using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Arrow.Framework;
using Arrow.Framework.Extensions;
using System.Web.UI.WebControls;

/// <summary>
/// SiteBase 的摘要说明
/// </summary>
public class SiteBase : PageBase
{
    /// <summary>
    /// 当前登录用户信息，关闭浏览器则消失
    /// </summary>
    public MemberInfo CurrentMember
    {
        set { MemberBLL.SetLoginInfo(value); }
        get { return MemberBLL.GetLoginInfo(); }
    }

    /// <summary>
    /// 显示用户信息
    /// </summary>
    protected void ShowUser()
    {
        if (CurrentMember != null)
        {
            string space = "&nbsp;&nbsp;&nbsp;";
            string link = "<a href='{0}'>{1}</a>";
            string msg= "欢迎：" + CurrentMember.UserName + space;
            msg += string.Format(link, "myfav.aspx", "线路收藏")+space;
            msg += string.Format(link, "myorder.aspx", "我的订单") + space;
            msg += string.Format(link, "myChange.aspx", "我的兑换") + space;
            msg += string.Format(link, "myCost.aspx", "我的消费") + space;
            msg += string.Format(link, "myMsg.aspx", "我的资料") + space;
            msg += string.Format(link, "LogOut.aspx", "退出登录") + space;
            ((Literal)Master.FindControl("ltUserInfo")).Text = msg;
        }
    }

    protected void ShowTitle(string title)
    {
        ((Literal)Master.FindControl("ltTitle")).Text ="当前页面："+ title;
    }

    protected override void OnLoad(EventArgs e)
    {
        ShowUser();
        base.OnLoad(e);
    }

}