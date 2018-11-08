<%@ Page Title="" Language="C#" MasterPageFile="../Master/MasterPage.master" AutoEventWireup="true" CodeFile="UnConfirmOrder.aspx.cs" Inherits="UnConfirmOrder" %>
<%@ Import Namespace="Arrow.Framework.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <meta http-equiv="refresh"content="10; url=UnConfirmOrder.aspx"/>
       <script src="../Resources/js/jquery-1.3.2.min.js"></script>
    <script src="../Resources/js/jquery.calendar.js"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            $("#<%= tbBegin.ClientID %>").cld();
        $("#<%= tbEnd.ClientID %>").cld();
    }
)
        </script>
    <link href="../Resources/css/calendar.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="Info">在这里，显示所有已提交，但未确认的订单。<br />
        所有在这里显示的订单，都是需要即时处理的订单。<br />
        本页面每10秒刷新一次<br />
        上次刷新时间：<span class="red bold" ><%=DateTime.Now %></span>
    </div>

    <div class="H20"></div>
        <div style="text-align:center">
            <table style="width:100%;padding:0; margin:0; ">
                <tr>
                    <td style=" text-align:center;"> 下单时间：
                    <asp:TextBox ID="tbBegin" runat="server" CssClass="EditForm_textbox" bj="cBj" Width="100px" ></asp:TextBox>&nbsp;
                    至&nbsp;
                    <asp:TextBox ID="tbEnd" runat="server" CssClass="EditForm_textbox" bj="cBj" Width="100"></asp:TextBox>&nbsp;
             
                        <asp:Button ID="btnConfirm" runat="server" Text="搜索" CssClass="btnPrime" OnClick="btnConfirm_Click" />
                    </td>
                </tr>
            </table>
        </div>
    <div class="Line"></div>
    <div class="H10"></div>
      <Arrow:GridView  ID="gvData" runat="server" CssClass="gridview" Width="100%"
            HorizontalAlign="Center" GridLines="None" CellPadding="5" AutoGenerateColumns="False" OnRowDataBound="gvData_RowDataBound" OnRowCommand="gvData_RowCommand"
           AllowPaging="False"
            PagerStyle-CssClass="gridpage" AlternatingRowStyle-BackColor="#f8f8f8" DataKeyNames="OrderNum" Ascending="False" ButtonCount="10" Condition="" ConnectionString="" FirstPageWord="首页" LastPageWord="末页" NextPageWord="下页" OrderBy="" PageIndex="1" PageSize="10" PageStyle="Full" PrevPageWord="上页" PrimaryKey="" QueryFields="*" SqlCreateType="TopN" TableName="" UrlQuery="">
            <alternatingrowstyle backcolor="#F8F8F8"></alternatingrowstyle>
            <columns>
            <asp:BoundField DataField="OrderNum" HeaderText="订单号" />
                <asp:BoundField DataField="OrderStatus" HeaderText="订单状态" />
                 <asp:BoundField DataField="TotalMoney" HeaderText="应付总额" />
                <asp:BoundField DataField="MoneyPayed" HeaderText="已付总额" />
                <asp:BoundField DataField="BuyNum" HeaderText="购买团位" />
               <asp:BoundField DataField="LineName" HeaderText="参加线路" />
                    <asp:BoundField DataField="GoDate" HeaderText="出发日期" />
                 <asp:BoundField DataField="PromotionName" HeaderText="参与促销" />
                 <asp:BoundField DataField="AddMemberRealName" HeaderText="下单人" />
                 <asp:BoundField DataField="AddMemberMobile" HeaderText="下单人电话" />
                  <asp:BoundField DataField="AddTime" HeaderText="下单时间" />
                  <asp:BoundField DataField="InviterRealName" HeaderText="邀请人" />
                   <asp:BoundField DataField="OperatorRealName" HeaderText="操作人" />
               
                 <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfMemberUserName" runat="server" Value='<%# Eval("AddMemberName") %>' />
                      <asp:LinkButton ID="lbtnUpdate" runat="server" Text="修改参团者" CommandArgument='<%# Eval("OrderNum") %>' CommandName="UpdateData"></asp:LinkButton>
                      <asp:LinkButton ID="lbtnConfirm" runat="server" Text="确认订单" CommandArgument='<%# Eval("OrderNum") %>' CommandName="ConfirmData"  Visible='<%# OrderBLL.SetConfirmButtonVisible(Eval("OrderStatus").ToArrowString()) %>' ></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
        </columns>
           
            <pagerstyle cssclass="gridpage"></pagerstyle>

        </Arrow:GridView>

    <div class="H20"></div>


</asp:Content>

