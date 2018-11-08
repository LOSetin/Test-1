<%@ Page Title="" Language="C#" MasterPageFile="~/SiteTest/MasterPage.master" AutoEventWireup="true" CodeFile="Order.aspx.cs" Inherits="SiteTest_Order" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../js/jquery-1.11.3.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align:center;margin:0 0 50px 0; font-size:16px; font-weight:bold;">订单流程：提交订单=》商家确认=》网上支付=》交易完成</div>
    <div>参团信息：<asp:Literal ID="ltLineName" runat="server"></asp:Literal><br />
              出发日期：<asp:Literal ID="ltGoDate" runat="server"></asp:Literal><br />
              促销信息：<asp:Literal ID="ltPromotion" runat="server"></asp:Literal>
    </div>
    <div>
        <p>联系人真名：<asp:TextBox ID="tbRealName" runat="server" /> *必填</p>
        <p>联系人电话：<asp:TextBox ID="tbPhone" runat="server" /> *必填</p>
    </div>
    <div>单价：<asp:Literal ID="ltPrice" runat="server"></asp:Literal> </div>
    <div>购买团位：<asp:DropDownList ID="ddlBuyNum" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBuyNum_SelectedIndexChanged"></asp:DropDownList></div>
    <div>总价：<asp:Literal ID="ltTotal" runat="server"></asp:Literal> </div>
   <br />
    <div><asp:Button ID="btnSubmit" runat="server" Text="快速提交" OnClick="btnSubmit_Click" /> 提交后再编辑参团者资料，然后等待商家联系确认 </div>
</asp:Content>

