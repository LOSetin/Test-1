using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework;
using Arrow.Framework.Extensions;

public partial class Common_ChangePwd : AdminBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SetNav("修改密码");
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        this.tbOldPwd.Attributes.Add("value", tbOldPwd.Text.Trim());
        this.tbNewPwd1.Attributes.Add("value", tbNewPwd1.Text.Trim());
        this.tbNewPwd2.Attributes.Add("value", tbNewPwd2.Text.Trim());
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string oldPwd = tbOldPwd.Text.Trim();
        string pwd1 = tbNewPwd1.Text.Trim();
        string pwd2 = tbNewPwd2.Text.Trim();
        string[] arrString = new string[] { oldPwd, pwd1, pwd2 };
        string[] arrFields = new string[] {"旧密码","新密码","重复密码" };
        if (arrString.ValidateHasNullOrEmptyString(arrFields))
            return;

        if (pwd1.ValidateIsNotEqualTo(pwd2, "两次输入的密码不一致！"))
            return;


        var model = SiteUserBLL.Select(CurrentAdmin.UserName);
        if (model.ValidateIsNull("该用户不存在！"))
            return;

        string md5 = SiteUserBLL.AdminEncrypt(oldPwd);
        if (md5.ValidateIsNotEqualTo(model.Pwd, "旧密码不正确！"))
            return;

        if (SiteUserBLL.ChangePwd(model, pwd1))
            MessageBox.Show("修改成功！请重新登陆！", "Login.aspx", "parent");
        else
            MessageBox.Show("修改失败！用户不存在！");

        

    }

    protected void btnClean_Click(object sender, EventArgs e)
    {
        tbOldPwd.Text = "";
        tbNewPwd1.Text = "";
        tbNewPwd2.Text = "";
    }
}