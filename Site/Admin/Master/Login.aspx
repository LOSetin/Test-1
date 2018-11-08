<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Admin_Master_Login" %>

<!DOCTYPE html>
<html>
	<head>
		<!-- Meta -->
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<!-- End of Meta -->
		
		<!-- Page title -->
		<title>后台管理登陆</title>
		<!-- End of Page title -->
		
		<!-- Libraries -->
		<link type="text/css" href="../Resources/css/login.css" rel="stylesheet" />
		<!-- End of Libraries -->	
	</head>
	<body>
	<div id="container">
		<div class="logo">
			<a href="#"><img src="../Resources/img/login_logo.png" alt="" /></a>
		</div>
		<div id="box">
			<form id="form1" runat="server">
                <table style="width:400px;">
                    <tr><td class="rowleft">用户名：</td><td style="text-align:left;"><asp:TextBox ID="tbUserName" runat="server" CssClass="textbox"></asp:TextBox></td></tr>
                    <tr><td class="rowleft">密码：</td><td style="text-align:left;"><asp:TextBox ID="tbPwd" runat="server" TextMode="Password" CssClass="textbox"></asp:TextBox></td></tr>
                    <tr><td class="rowleft">验证码：</td><td style="text-align:left;"><asp:TextBox ID="tbValidate" runat="server" CssClass="textbox"></asp:TextBox ><img id="imgVerify" style="cursor:pointer; vertical-align:middle;"  src="VerifyCode.aspx?" alt="看不清？点击更换" title="看不清？点击更换" onclick="this.src=this.src+'?'" /></td></tr>
                    <tr><td class="rowleft"></td><td style="text-align:left;"><asp:Button ID="btnLogin" runat="server" Text="登陆" CssClass="loginbutton" OnClick="btnLogin_Click" />
                       &nbsp; <input type="reset" value="清空" class="resetbutton" />
                                                 </td></tr>

                </table>

			</form>
		</div>
	</div>

	</body>
</html>
