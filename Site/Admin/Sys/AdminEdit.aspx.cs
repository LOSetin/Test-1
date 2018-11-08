using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework;
using Arrow.Framework.Extensions;

public partial class AdminEdit : AdminBase
{

    protected void Page_Load(object sender, EventArgs e)
    {
        SetNav("添加管理员");
        ltTips.Text = "在这里，您可以添加管理员";
        btnSubmit.Text = "确定添加";
        ltPwdTips.Text = "* 设定初始密码为admin，用户添加成功后，密码由用户自行修改";
        tbPwd.Text = "admin";
        btnClean.Visible = false;

    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        this.tbPwd.Attributes.Add("value", tbPwd.Text.Trim());
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string userName = tbUserName.Text.Trim();
        string pwd = tbPwd.Text.Trim();
        string realName = tbRealName.Text.Trim();
        string inviteNum = tbInviteNum.Text.Trim();

        string[] arr = { userName, pwd, realName, inviteNum };
        string[] name = { "用户名", "密码", "真名", "邀请码" };

        if (arr.ValidateHasNullOrEmptyString(name))
            return;

        //判断用户名和邀请码是否唯一
        var user = SiteUserBLL.Select(userName);
        if (user.ValidateIsNotNull("该用户名已存在！请选择其他用户名！"))
            return;

        if(SiteUserBLL.InviteNumExist(inviteNum))
        {
            MessageBox.Show("该邀请码已存在！请选择其他验证码！");
            return;
        }

        TMS.SiteUserInfo model = new TMS.SiteUserInfo();
        model.InviteNum = inviteNum;
        model.LastLoginIP = "127.0.0.1";
        model.LastLoginTime = DateTime.Now;
        model.Name = userName;
        model.Pwd = SiteUserBLL.AdminEncrypt(pwd);
        model.RealName = realName;
        model.Remarks = "";
        model.RoleIDs = "1";
        model.ThisLoginIP = "127.0.0.1";
        model.ThisLoginTime = DateTime.Now;
        model.UserStatus = AdminStatus.Enable;

        SiteUserBLL.Add(model);

        MessageBox.Show("添加成功！","AdminEdit.aspx");


    }

    protected void btnClean_Click(object sender, EventArgs e)
    {
        RedirectToReturnUrl();
    }
}