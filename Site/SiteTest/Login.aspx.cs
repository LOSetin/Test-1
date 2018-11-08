using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework.Extensions;

public partial class SiteTest_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        string userName = tbUserName.Text.Trim();
        string userPwd = tbPwd1.Text.Trim();
        if (userName.ValidateIsNullOrEmpty("请输入用户名！"))
            return;
        if (userPwd.ValidateIsNullOrEmpty("请输入密码！"))
            return;

        var model = MemberBLL.Select(userName);
        if (model.ValidateIsNull("账号或密码不正确！"))
            return;

        if (model.UserPwd.ValidateIsNotEqualTo(MemberBLL.Encrypt(userPwd), "账号或密码不正确!"))
            return;

        MemberInfo mi = new MemberInfo();
        mi.UserName = model.UserName;
        mi.RealName = model.RealName;
        mi.InviteNum = model.InviteNum;
        mi.Phone = model.MobileNum;
        MemberBLL.SetLoginInfo(mi);
        Response.Redirect("default.aspx");

    }
}