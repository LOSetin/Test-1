<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Reg.aspx.cs" Inherits="SiteTest_Reg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <p>用户名：<asp:TextBox ID="tbUserName" runat="server" ></asp:TextBox> *必填</p>
        <p>密码：<asp:TextBox ID="tbPwd1" runat="server" TextMode="Password"></asp:TextBox> *必填</p>
        <p>重复密码：<asp:TextBox ID="tbPwd2" runat="server" TextMode="Password"></asp:TextBox> *必填</p>
        <p>真实姓名：<asp:TextBox ID="tbRealName" runat="server"></asp:TextBox> *必填</p>
        <p>电话：<asp:TextBox ID="tbMobile" runat="server"></asp:TextBox> *必填</p>
        <p>性别：<asp:DropDownList ID="ddlSex" runat="server">
            <asp:ListItem Value="男" Text="男"></asp:ListItem>
            <asp:ListItem Value="女" Text="女"></asp:ListItem>
              </asp:DropDownList> </p>
        <p>邀请码：<asp:TextBox ID="tbInviteNum" runat="server"></asp:TextBox>  *必填</p>
        <p>QQ： <asp:TextBox ID="tbQQ" runat="server"></asp:TextBox>  </p>
        <p>微信：<asp:TextBox ID="tbWeChat" runat="server"></asp:TextBox> </p>
        <p style="padding-left:50px;"><asp:Button ID="btnAdd" runat="server" Text="注册" OnClick="btnAdd_Click"  /> </p>
    </div>
    </form>
</body>
</html>
