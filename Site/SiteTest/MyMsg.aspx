<%@ Page Title="" Language="C#" MasterPageFile="~/SiteTest/MasterPage.master" AutoEventWireup="true" CodeFile="MyMsg.aspx.cs" Inherits="MyMsg" %>
<%@ Import Namespace="Arrow.Framework.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p style="font-weight:bold;">说明：此页尚未实现功能，只是作为演示使用</p>
<p style="font-weight:bold;font-size:16px; color:red">修改信息</p>
    <p>真实姓名：<asp:TextBox ID="tbRealName" runat="server"></asp:TextBox> *必填</p>
        <p>电话：<asp:TextBox ID="tbMobile" runat="server"></asp:TextBox> *必填</p>
        <p>性别：<asp:DropDownList ID="ddlSex" runat="server">
            <asp:ListItem Value="男" Text="男"></asp:ListItem>
            <asp:ListItem Value="女" Text="女"></asp:ListItem>
              </asp:DropDownList> </p>
        <p>邀请码：<asp:TextBox ID="tbInviteNum" runat="server"></asp:TextBox>  *必填</p>
        <p>QQ： <asp:TextBox ID="tbQQ" runat="server"></asp:TextBox>  </p>
        <p>微信：<asp:TextBox ID="tbWeChat" runat="server"></asp:TextBox> </p>
        <p style="padding-left:50px;"><asp:Button ID="btnAdd" runat="server" Text="修改资料"   /> </p>
    <br /><br />
    <p  style="font-weight:bold;font-size:16px; color:red">修改密码</p>
      <p>旧密码：<asp:TextBox ID="tbOldPwd" runat="server" TextMode="Password"></asp:TextBox> *必填</p>
     <p>密码：<asp:TextBox ID="tbPwd1" runat="server" TextMode="Password"></asp:TextBox> *必填</p>
        <p>重复密码：<asp:TextBox ID="tbPwd2" runat="server" TextMode="Password"></asp:TextBox> *必填</p>
       <p style="padding-left:50px;"><asp:Button ID="Button1" runat="server" Text="修改密码"   /> </p>

</asp:Content>

