<%@ Page Title="" Language="C#" MasterPageFile="../Master/MasterPage.master" AutoEventWireup="true" CodeFile="OrderManager.aspx.cs" Inherits="OrderManager" %>
<%@ Import Namespace="Arrow.Framework.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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

    <div class="Info">在这里，您可以对订单进行管理。<br />
        您可以随时修改参团者资料。<br />
        买家提交订单后，需要您确认，对方才能付款。<br />
        操作人为空时，表示该订单还没被员工处理，操作人只记录第一个操作的人，即使后面换其他人操作，操作人仍然是第一个操作的人。
    </div>

    <div class="H20"></div>
        <div style="text-align:center">
            <table style="width:100%;padding:0; margin:0; ">
                <tr>
                    <td style=" text-align:center;"> 下单时间：
                    <asp:TextBox ID="tbBegin" runat="server" CssClass="EditForm_textbox" bj="cBj" Width="100px" ></asp:TextBox>&nbsp;
                    至&nbsp;
                    <asp:TextBox ID="tbEnd" runat="server" CssClass="EditForm_textbox" bj="cBj" Width="100"></asp:TextBox>&nbsp;
                     订单状态：<asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList>&nbsp;&nbsp;
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

