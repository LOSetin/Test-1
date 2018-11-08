using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework.Extensions;
using TMS;

public partial class Admin_Master_Login : AdminBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    private void DoLogin()
    {
        string userName = tbUserName.Text.Trim();
        string userPwd = tbPwd.Text.Trim();
        string code = tbValidate.Text.Trim();
        if (Session["VerifyCode"].ValidateIsNull("验证码已过时！"))
            return;

        string[] arrString = new string[] { userName, userPwd, code };
        string[] arrFieldNames = new string[] { "用户名", "密码", "验证码" };

        if (arrString.ValidateHasNullOrEmptyString(arrFieldNames))
            return;

        if (code.ValidateIsNotEqualTo(Session["VerifyCode"].ToArrowString(), "验证码不正确！"))
            return;

        LoginInfo li = SiteUserBLL.DoLogin(userName, userPwd);
        if (li.ValidateIsNull("账号或密码不正确！"))
            return;

        Response.Redirect("main.htm");
    }

    private void TestLogin()
    {
        LoginInfo li = SiteUserBLL.DoLogin("admin", "admin");
        if (li.ValidateIsNull("账号或密码不正确！"))
            return;

        Response.Redirect("main.htm");
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        //TestLogin();
        DoLogin();
    }
}