<%@ Page Title="" Language="C#" MasterPageFile="~/SiteTest/MasterPage.master" AutoEventWireup="true" CodeFile="MyOrderDetail.aspx.cs" Inherits="SiteTest_MyOrderDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .NotShowPage{display:none;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div><asp:Button ID="btnReturn" runat="server" Text="返回订单列表" OnClick="btnReturn_Click" /> </div>
    <br />
    <p style="font-size:16px; font-weight:bold;">基本信息</p>
    <p>参加线路：<asp:Literal ID="ltLineName" runat="server"></asp:Literal> </p>
    <p>出发日期：<asp:Literal ID="ltGoDate" runat="server"></asp:Literal> </p>
    <p>总价：<asp:Literal ID="ltTotal" runat="server"></asp:Literal> </p>
    <p>参与促销：<asp:Literal ID="ltPromotionName" runat="server"></asp:Literal> </p>
    <p>订单状态：<asp:Literal ID="ltStatus" runat="server"></asp:Literal> </p>
    <br />
    <p style="font-size:16px; font-weight:bold;">联系人信息</p>
    <p>姓名：<asp:Literal ID="ltRealName" runat="server"></asp:Literal> </p>
    <p>电话：<asp:Literal ID="ltPhone" runat="server"></asp:Literal> </p>
    <br />
     <p style="font-size:16px; font-weight:bold;">参团者信息</p>
      <Arrow:GridView ID="gvData" runat="server"
           AutoGenerateColumns="false" >
           <Columns>
               <asp:TemplateField HeaderText="真实姓名">
                   <ItemTemplate> 
                       <asp:HiddenField ID="hfID" runat="server" Value='<%# Eval("ID") %>' />
                       <asp:Literal ID="tbRealName" runat="server" Text='<%# Eval("RealName") %>'></asp:Literal>
                   </ItemTemplate>
               </asp:TemplateField>
                <asp:TemplateField HeaderText="性别">
                   <ItemTemplate>
                       <%# Eval("Sex") %>
                       <asp:HiddenField ID="hfSex" runat="server" Value='<%# Eval("Sex") %>' />
                   </ItemTemplate>
               </asp:TemplateField>
                <asp:TemplateField HeaderText="身份证">
                   <ItemTemplate>
                         <asp:Literal ID="tbIDNum" runat="server" Text='<%# Eval("IDNum") %>'></asp:Literal>
                   </ItemTemplate>
               </asp:TemplateField>
                <asp:TemplateField HeaderText="联系电话">
                   <ItemTemplate>
                         <asp:Literal ID="tbPhone" runat="server" Text='<%# Eval("MobileNum") %>'></asp:Literal>
                   </ItemTemplate>
               </asp:TemplateField>
           </Columns>
       </Arrow:GridView>

</asp:Content>

