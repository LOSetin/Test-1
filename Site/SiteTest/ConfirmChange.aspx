<%@ Page Title="" Language="C#" MasterPageFile="~/SiteTest/MasterPage.master" AutoEventWireup="true" CodeFile="ConfirmChange.aspx.cs" Inherits="ConfirmChange" %>
<%@ Import Namespace="Arrow.Framework.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p>您当前的点数：<asp:Literal ID="ltPoints" runat="server"></asp:Literal></p>
    <p>兑换的商品名：<asp:Literal ID="ltGoodsName" runat="server"></asp:Literal> </p>
    <p>商品库存：<asp:Literal ID="ltGoodsNum" runat="server"></asp:Literal> </p>
    <p>需要的点数：<asp:Literal ID="ltPointsCost" runat="server"></asp:Literal> </p>
    <p>兑换的个数：<asp:TextBox ID="tbNum" runat="server"></asp:TextBox> </p>
    <p>操作：<asp:Button ID="Change" runat="server" Text="我要兑换" OnClick="Change_Click" />  
        <asp:Button ID="btnReturn" runat="server" Text="查看我的兑换记录" OnClick="btnReturn_Click" />
    </p>

</asp:Content>

