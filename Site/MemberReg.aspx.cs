using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework;
using Arrow.Framework.Extensions;
using TMS;

public partial class MemberReg : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnReg_Click(object sender, EventArgs e)
    {
        string userName = tbUserName.Value.Trim();
        string pwd1 = tbPwd1.Value.Trim();
        string pwd2 = tbPwd2.Value.Trim();
        string sex = "";
        string inviteNum = tbInviteNum.Value.Trim();
        string qq = "";
        string weChat = "";
        string mobile = "";
        string realName = "";

        if (userName.IsNullOrEmpty())
        {
            JsBox.Show("请输入用户名！");
            return;
        }

        if (pwd1.IsNullOrEmpty())
        {
            JsBox.Show("请输入密码！");
            return;
        }

        if (pwd2.IsNullOrEmpty())
        {
            JsBox.Show("请输入重复密码！");
            return;
        }

        if (inviteNum.IsNullOrEmpty())
        {
            JsBox.Show("请输入邀请码！");
            return;
        }

        if(pwd1!=pwd2)
        {
            JsBox.Show("两次输入的密码不一致！");
            return;
        }


        var member = MemberBLL.Select(userName);
        if(member!=null)
        {
            JsBox.Show("该用户名已存在！请选择其他用户名！");
            return;
        }

        var admin = SiteUserBLL.SelectUserByInviteNum(inviteNum);
        if (admin==null)
        {
            JsBox.Show("邀请码不存在！");
            return;
        }

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
        MessageBox.Show("注册成功！", "MemberIndex.aspx");

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        Response.Redirect("MemberLogin.aspx");
    }
}