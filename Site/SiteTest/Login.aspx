<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="SiteTest_Login" %>

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
                <p style="padding-left:50px;"><asp:Button ID="btnConfirm" runat="server" Text="登陆" OnClick="btnConfirm_Click" /> </p>

    </div>
    </form>
</body>
</html>
