<%@ Page Title="" Language="C#" MasterPageFile="~/SiteTest/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="SiteTest_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div style="text-align:center; font-size:20px; margin-top:200px; font-weight:bold">这是首页--------<%= DateTime.Now.ToString() %></div>

</asp:Content>

