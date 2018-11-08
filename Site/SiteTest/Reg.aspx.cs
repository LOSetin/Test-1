using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework.Extensions;
using TMS;
using Arrow.Framework;

public partial class SiteTest_Reg : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string userName = tbUserName.Text.Trim();
        string pwd1 = tbPwd1.Text.Trim();
        string pwd2 = tbPwd2.Text.Trim();
        string sex = ddlSex.SelectedValue;
        string inviteNum = tbInviteNum.Text.Trim();
        string qq = tbQQ.Text.Trim();
        string weChat = tbWeChat.Text.Trim();
        string mobile = tbMobile.Text.Trim();
        string realName = tbRealName.Text.Trim();

        string[] fields = { userName, pwd1, pwd2, inviteNum,realName,mobile };
        string[] names = { "用户名", "密码", "重复密码", "邀请码","真实姓名","联系电话" };
        if (fields.ValidateHasNullOrEmptyString(names))
            return;
        if (pwd1.ValidateIsNotEqualTo(pwd2, "两次输入的密码不一致！"))
            return;

        var member = MemberBLL.Select(userName);
        if (member.ValidateIsNotNull("该用户名已存在！请选择其他用户名！"))
            return;

        var admin = SiteUserBLL.SelectUserByInviteNum(inviteNum);
        if (admin.ValidateIsNull("邀请码不存在！"))
            return;

        string pwd = MemberBLL.Encrypt(pwd1);

        var model = new SiteMemberInfo();
        model.AddTime = DateTime.Now;
        model.Email = "";
        model.HeadPicPath = "";
        model.IDNum = "";
        model.InviteNum = inviteNum;
        model.InviterRealName = admin.RealName;
        model.InviterUserName = admin.Name;
        model.MobileNum = mobile;
        model.QQ = qq;
        model.RealName = realName;
        model.Remarks = "";
        model.Sex = sex;
        model.TotalCost = 0M;
        model.TotalPoints = 0;
        model.UsedPoints = 0;
        model.UserName = userName;
        model.UserPwd = pwd;
        model.WeChat = weChat;

        MemberBLL.AddMember(model);
        MemberInfo mi = new MemberInfo();
        mi.InviteNum = inviteNum;
        mi.RealName = "";
        mi.UserName = userName;
        MemberBLL.SetLoginInfo(mi);
        MessageBox.Show("注册成功！","default.aspx");

    }
}