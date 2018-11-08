<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Top.aspx.cs" Inherits="Master_Top" %>
<html >
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        body { padding: 0; margin: 0; font-size:12px; }
        #top { height: 47px; background-image: url(../Resources/img/topbg.png); background-repeat: repeat-x;}
        a,a:visited { color: #f0f0f0; text-decoration: none; }
        a:hover { color: #a8cf72; text-decoration: underline; }

    </style>
    <script type="text/javascript" language="JavaScript">
        function logout() {
            if (confirm("您确定要退出控制面板吗？"))
                top.location = "Logout.aspx";
            return false;
        }
</script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="top" >
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td height="42px" width="200px" style="vertical-align: middle;padding-left:20px;" >
                        <img src="../Resources/img/logo.png" alt="logo"/>
                        </td>
                    <td style=" width:10px; vertical-align:middle;font-size:12px; color:#f0f0f0" ></td>
                    <td style="vertical-align:middle;font-size:12px; color:#f0f0f0; text-align:right; padding-right:20px;">
                         欢迎使用：<%=CurrentAdmin.RealName %>，您是<%=CurrentAdmin.IsSuper?"超级管理员":"普通管理员" %>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <a href="Right.aspx" target="Right" >后台首页</a>&nbsp;&nbsp;&nbsp;&nbsp;
                                 <a href="ChangePwd.aspx" target="Right" >修改密码</a>&nbsp;&nbsp;&nbsp;&nbsp;
                              <a href="#" target="_self" onclick="logout()">安全退出</a>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
