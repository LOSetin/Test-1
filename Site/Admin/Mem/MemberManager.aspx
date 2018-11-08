<%@ Page Title="" Language="C#" MasterPageFile="../Master/MasterPage.master" AutoEventWireup="true" CodeFile="MemberManager.aspx.cs" Inherits="MemberManager" %>
<%@ Import Namespace="Arrow.Framework.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="Info">在这里，您可以查询会员信息。<br />
    </div>

    <div class="H20"></div>
        <div style="text-align:center">
            <table style="width:100%;padding:0; margin:0; ">
                <tr><td style="text-align:center;"> 账号：<asp:TextBox ID="tbKeyWord" runat="server" CssClass="EditForm_textbox" Width="150"></asp:TextBox> &nbsp;
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
                <asp:TemplateField HeaderText="真名">
                    <ItemTemplate>
                        <%# Eval("RealName") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="性别">
                    <ItemTemplate>
                       <%# Eval("Sex")%>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:BoundField DataField="MobileNum" HeaderText="电话" />
                 <asp:BoundField DataField="IDNum" HeaderText="身份证号" />
                  <asp:BoundField DataField="TotalCost" HeaderText="消费总额" />
                  <asp:BoundField DataField="TotalPoints" HeaderText="总点数" />
                   <asp:TemplateField HeaderText="剩余点数">
                    <ItemTemplate>
                       <%# Eval("TotalPoints").ToArrowInt()-Eval("UsedPoints").ToArrowInt()%>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:BoundField DataField="InviterRealName" HeaderText="邀请人" />
                   <asp:BoundField DataField="AddTime" HeaderText="注册时间" />
               
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnCostHistory" Text="消费记录" CommandArgument='<%# Eval("UserName") %>' CommandName="CostHistory" runat="server"></asp:LinkButton>
                         <asp:LinkButton ID="lbtnExchangeHistory" Text="兑换记录" CommandArgument='<%# Eval("UserName") %>' CommandName="ExchangeHistory" runat="server"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
        </columns>
           
            <pagerstyle cssclass="gridpage"></pagerstyle>

        </Arrow:GridView>

    <div class="H20"></div>


</asp:Content>

