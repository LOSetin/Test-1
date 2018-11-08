using Arrow.Framework;
using Arrow.Framework.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework.Extensions;

public partial class MemberModifyPwd : MemberBase
{
    protected void InitMsg()
    {
        ltMsg1.Text = "*必填";
        ltMsg2.Text = "*必填";
        ltMsg3.Text = "*必填";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
       if(!Page.IsPostBack)
        {
            InitMsg();
        }
    }



    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        if (CurrentMember == null) return;
        ltScript.Text = "";
        InitMsg();
        string oldPwd = tbOldPwd.Text.Trim();
        string newPwd1 = tbNewPwd1.Text.Trim();
        string newPwd2 = tbNewPwd2.Text.Trim();

        if (oldPwd.IsNullOrEmpty())
        {
            ltMsg1.Text = "请输入旧密码！";
            return;
        }

        if(newPwd1.IsNullOrEmpty())
        {
            ltMsg2.Text = "请输入新密码！";
            return;
        }

        if(newPwd2.IsNullOrEmpty())
        {
            ltMsg3.Text = "请输入重复密码！";
            return;
        }

        if(newPwd1!=newPwd2)
        {
            ltMsg3.Text = "两次输入的新密码不一致！";
            return;
        }

        //判断旧密码是否正确
        var member = MemberBLL.Select(CurrentMember.UserName);
        if(member.UserPwd!=MemberBLL.Encrypt(oldPwd))
        {
            ltMsg1.Text = "旧密码不正确！";
            return;
        }

        //更新新密码
        string newPwd = MemberBLL.Encrypt(newPwd1);
        member.UserPwd = newPwd;
        MemberBLL.Update(member);
        ltScript.Text= JqueryMsg.CreateScript("修改密码成功，建议重新登陆！");

    }
}