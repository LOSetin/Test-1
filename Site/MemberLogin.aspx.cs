using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework.Extensions;
using Arrow.Framework;

public partial class MemberLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string userName = tbUserName.Value.Trim();
        string pwd = tbPwd.Value.Trim();
        string vcode = tbCode.Value.Trim();

        if(userName.IsNullOrEmpty())
        {
            JsBox.Show("请输入用户名！");
            return;
        }

        if(pwd.IsNullOrEmpty())
        {
            JsBox.Show("请输入密码！");
            return;
        }

        if(Session["MemberVerifyCode"]==null)
        {
            JsBox.Show("请输入验证码！");
            return;
        }

        if (Session["MemberVerifyCode"].ToString()!=vcode)
        {
            JsBox.Show("验证码不正确！");
            return;
        }


        var model = MemberBLL.Select(userName);
        if(model==null)
        {
            JsBox.Show("账号或密码不正确！");
            return;
        }

       

        if (model.UserPwd!=(MemberBLL.Encrypt(pwd)))
        {
            JsBox.Show("账号或密码不正确！");
            return;
        }

        MemberInfo mi = new MemberInfo();
        mi.UserName = model.UserName;
        mi.RealName = model.RealName;
        mi.InviteNum = model.InviteNum;
        mi.Phone = model.MobileNum;
        if (chkRember.Checked)
            MemberBLL.SetLoginInfo(mi, true);
        else
            MemberBLL.SetLoginInfo(mi);
        string returnUrl = Request.QueryString["ReturnUrl"];
        if (returnUrl.IsNullOrEmpty())
            Response.Redirect("MemberIndex.aspx");
        else
            Response.Redirect(returnUrl);

    }

    protected void btnReg_Click(object sender, EventArgs e)
    {
        Response.Redirect("MemberReg.aspx");
    }

   
}