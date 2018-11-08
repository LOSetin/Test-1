<%@ Page Title="" Language="C#" MasterPageFile="../Master/MasterPage.master" AutoEventWireup="true" CodeFile="MemberCostHistory.aspx.cs" Inherits="MemberCostHistory" %>
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

    <div class="Info">在这里，您可以查询会员消费信息。<br />
    </div>

    <div class="H20"></div>
        <div style="text-align:center">
            <table style="width:100%;padding:0; margin:0; ">
                <tr><td style="text-align:left; width:150px;"><asp:Button ID="btnReturn" runat="server" Text="返回" CssClass="btnGray" OnClick="btnReturn_Click" /> </td>
                    <td style="text-align:center; padding-right:150px;"> 消费时间：
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
            PagerStyle-CssClass="gridpage" AlternatingRowStyle-BackColor="#f8f8f8" DataKeyNames="UserName" Ascending="False" ButtonCount="10" Condition="" ConnectionString="" FirstPageWord="首页" LastPageWord="末页" NextPageWord="下页" OrderBy="" PageIndex="1" PageSize="10" PageStyle="Full" PrevPageWord="上页" PrimaryKey="" QueryFields="*" SqlCreateType="TopN" TableName="" UrlQuery="">
            <alternatingrowstyle backcolor="#F8F8F8"></alternatingrowstyle>
            <columns>
            <asp:BoundField DataField="UserName" HeaderText="用户名" />
                <asp:BoundField DataField="PromotionName" HeaderText="参与促销" />
                 <asp:BoundField DataField="LineName" HeaderText="路线" />
                <asp:TemplateField HeaderText="出发日期">
                    <ItemTemplate><%# Eval("GoDate").ToArrowDateTime().ToDateOnlyString() %></ItemTemplate>
                </asp:TemplateField>
                  <asp:BoundField DataField="BuyNum" HeaderText="购买数量" />
                  <asp:BoundField DataField="TotalMoney" HeaderText="支付总额" />
                <asp:BoundField DataField="MoneyPayed" HeaderText="已支付" />
                <asp:BoundField DataField="PointsCost" HeaderText="点数增加" />
                   <asp:BoundField DataField="AddTime" HeaderText="消费时间" />
               
                
        </columns>
           
            <pagerstyle cssclass="gridpage"></pagerstyle>

        </Arrow:GridView>

    <div class="H20"></div>


</asp:Content>

