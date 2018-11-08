using Arrow.Framework;
using Arrow.Framework.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arrow.Framework.Extensions;

public partial class MemberModifyData : MemberBase
{
    protected void BindData()
    {
        if(CurrentMember!=null)
        {
            var data = MemberBLL.Select(CurrentMember.UserName);
            if(data!=null)
            {
                tbRealName.Text = data.RealName;
                tbPhone.Text = data.MobileNum;
                tbQQ.Text = data.QQ;
                tbWeiXin.Text = data.WeChat;
                ddlSex.SelectedValue = data.Sex;
                ltInviteCode.Text = data.InviteNum;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
       if(!Page.IsPostBack)
        {
            BindData();
            ltMsg1.Text = "*必填";
            ltMsg2.Text = "*必填";
        }
    }



    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        if (CurrentMember == null) return;
        ltScript.Text = "";

        string realName = tbRealName.Text.Trim();
        string mobile = tbPhone.Text.Trim();
        string sex = ddlSex.SelectedValue;
        string qq = tbQQ.Text.Trim();
        string weixin = tbWeiXin.Text.Trim();

        if(realName.IsNullOrEmpty())
        {
            ltMsg1.Text = "请输入真实姓名！";
            return;
        }

        if(mobile.IsNullOrEmpty())
        {
            ltMsg2.Text = "请输入电话！";
            return;
        }

        var data = MemberBLL.Select(CurrentMember.UserName);
        if (data != null)
        {
            data.RealName = realName;
            data.MobileNum = mobile;
            data.Sex = sex;
            data.QQ = qq;
            data.WeChat = weixin;
        }
        MemberBLL.Update(data);

        ltMsg1.Text = "*必填";
        ltMsg2.Text = "*必填";
        ltScript.Text= JqueryMsg.CreateScript("修改成功！");

    }
}