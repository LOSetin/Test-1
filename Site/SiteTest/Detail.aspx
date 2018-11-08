<%@ Page Title="" Language="C#" MasterPageFile="~/SiteTest/MasterPage.master" AutoEventWireup="true" CodeFile="Detail.aspx.cs" Inherits="SiteTest_Detail" %>
<%@ Import Namespace="Arrow.Framework.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <div style="text-align:center;font-weight:bold;font-size:30px; color:blue;">线路名称：<asp:Literal ID="ltLineName" runat="server"></asp:Literal> </div>
    <table>
        <tr><td>
            <asp:Literal ID="ltImage" runat="server"></asp:Literal>
            </td>
            <td>
                <p>
                    出团日期：<asp:DropDownList ID="ddlGroup" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
                    返程日期：<asp:Literal ID="ltBackDate" runat="server"></asp:Literal>&nbsp;&nbsp;&nbsp;&nbsp;
                </p>
                <p>价格：<asp:Literal ID="ltPrice" runat="server"></asp:Literal> </p>
                <p>团人数：<asp:Literal ID="ltNum" runat="server"></asp:Literal>&nbsp;&nbsp;&nbsp;&nbsp;
                     剩余位置：<asp:Literal ID="ltRemain" runat="server"></asp:Literal>
                </p>
                <p>集合时间：<asp:Literal ID="ltTime" runat="server"></asp:Literal>&nbsp;&nbsp;&nbsp;&nbsp;
                    集合地点：<asp:Literal ID="ltPlace" runat="server"></asp:Literal>
                </p>
                <p>去程交通：<asp:Literal ID="ltGoTravel" runat="server"></asp:Literal>&nbsp;&nbsp;&nbsp;&nbsp;
                    返程交通：<asp:Literal ID="ltBackTravel" runat="server"></asp:Literal>
                </p>
            </td>
        </tr>
        <tr><td></td><td><asp:Button ID="btnOrder" runat="server" Text="立即预定" OnClick="btnOrder_Click" />&nbsp;&nbsp;<asp:Button ID="btnImport" runat="server" Text="导出行程" OnClick="btnImport_Click" /> &nbsp;&nbsp;<asp:Button ID="btnShare" runat="server" Text="在线分享" OnClick="btnShare_Click" /> </td></tr>
        <tr><td></td><td>
            <p>报名须知：<asp:Literal ID="ltNotice" runat="server"></asp:Literal> </p>
            <p>线路说明：<asp:Literal ID="ltDesc" runat="server"></asp:Literal> </p>
                     </td></tr>
        <tr><td></td><td>
            <p style="text-align:center; font-weight:bold; font-size:16px;">详细行程</p>
            <p>
                <asp:Literal ID="ltDetail" runat="server"></asp:Literal>
            </p>
                     </td></tr>
    </table>

</asp:Content>

