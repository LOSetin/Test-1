<%@ Page Title="" Language="C#" MasterPageFile="~/SiteTest/MasterPage.master" AutoEventWireup="true" CodeFile="MyOrder.aspx.cs" Inherits="MyOrder" %>
<%@ Import Namespace="Arrow.Framework.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align:center;font-size:14px; margin:50px 0 10px 0"><asp:Literal ID="ltLevel1" runat="server"></asp:Literal></div>
    <div style="text-align:center;font-size:12px; margin:10px 0 50px 0"><asp:Literal ID="ltLevel2" runat="server"></asp:Literal> </div>
    <div>操作说明：<br />
        1.订单未提交时，可以修改参团人资料，也可直接提交，让商家输入参团人资料<br />
        2.直接提交后，需要等待商家联系确认,确认后，不可再修改参团人资料。<br />
        3.确认后，才可以支付<br />
        4.在支付成功之前，均可以取消订单。<br />
        5.支付成功后，交易完成，可获得与订单价值相等的点数，1元=1点
    </div>
    <br /><br />
    <div style="font-size:16px; font-weight:bold;">订单流程：未提交=》已提交（等待商家确认）=》已确认（商家已联系确认）=》网上支付=》完成</div>
    <br /><br />
    <div>
       <Arrow:GridView ID="gvData" runat="server"
           AutoGenerateColumns="false" OnRowCommand="gvData_RowCommand" OnDataBound="gvData_DataBound" OnRowDataBound="gvData_RowDataBound" >
           <Columns>
               <asp:TemplateField HeaderText="订单号">
                   <ItemTemplate><%# Eval("OrderNum")   %></ItemTemplate>
               </asp:TemplateField>
                <asp:TemplateField HeaderText="订单状态">
                   <ItemTemplate><%# Eval("OrderStatus")   %></ItemTemplate>
               </asp:TemplateField>
                <asp:TemplateField HeaderText="付款总额">
                   <ItemTemplate><%# Eval("TotalMoney")   %></ItemTemplate>
               </asp:TemplateField>
                <asp:TemplateField HeaderText="购买团位">
                   <ItemTemplate><%# Eval("BuyNum")   %></ItemTemplate>
               </asp:TemplateField>
                <asp:TemplateField HeaderText="参加的团">
                   <ItemTemplate><%# Eval("LineName")   %></ItemTemplate>
               </asp:TemplateField>
                <asp:TemplateField HeaderText="出发日期">
                   <ItemTemplate><%# Eval("GoDate")   %></ItemTemplate>
               </asp:TemplateField>
                <asp:TemplateField HeaderText="参与促销">
                   <ItemTemplate><%# Eval("PromotionName")   %></ItemTemplate>
               </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                   <ItemTemplate>
                       <asp:LinkButton ID="lbtnUpdate" runat="server" Text="编辑参团人" CommandArgument='<%# Eval("OrderNum") %>' CommandName="EditData" Visible='<%# OrderBLL.SetEditButtonVisible(Eval("OrderStatus").ToArrowString()) %>'></asp:LinkButton>
                       &nbsp;&nbsp;
                        <asp:LinkButton ID="lbtnCommit" runat="server" Text="提交订单" CommandArgument='<%# Eval("OrderNum") %>' CommandName="SumitData" Visible='<%# OrderBLL.SetSubmitButtonVisible(Eval("OrderStatus").ToArrowString()) %>'></asp:LinkButton>
                       &nbsp;&nbsp;
                       <asp:LinkButton ID="lbtnCancel" runat="server" Text="取消订单" CommandArgument='<%# Eval("OrderNum") %>' CommandName="CancelData"  Visible='<%# OrderBLL.SetCancelButtonVisible(Eval("OrderStatus").ToArrowString()) %>'></asp:LinkButton>
                       &nbsp;&nbsp;
                       <asp:LinkButton ID="lbtnPay" runat="server" Text="我要支付" CommandArgument='<%# Eval("OrderNum") %>' CommandName="PayData" Visible='<%# OrderBLL.SetPayButtonVisible(Eval("OrderStatus").ToArrowString()) %>'></asp:LinkButton>
                       &nbsp;&nbsp;
                       <asp:LinkButton ID="lbtnDetail" runat="server" Text="查看详细" CommandArgument='<%# Eval("OrderNum") %>' CommandName="DetailData" />
                       &nbsp;&nbsp;
                   </ItemTemplate>
               </asp:TemplateField>
           </Columns>
       </Arrow:GridView>
    </div>
</asp:Content>

