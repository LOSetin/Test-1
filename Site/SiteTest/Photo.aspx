<%@ Page Title="" Language="C#" MasterPageFile="~/SiteTest/MasterPage.master" AutoEventWireup="true" CodeFile="Photo.aspx.cs" Inherits="SiteTest_Photo" %>
<%@ Import Namespace="Arrow.Framework.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align:center;padding:0 50px 0 50px">
    <Arrow:DataList ID="dtPhoto" runat="server" >
        <ItemTemplate>
            <p><%# SiteUtility.ShowImage(Eval("CoverPath").ToArrowString(),100,100, true) %></p>
            <p><%# Eval("Name") %></p>
        </ItemTemplate>
        <ItemStyle Width="200" HorizontalAlign="Center" />
    </Arrow:DataList>
        </div>
</asp:Content>

