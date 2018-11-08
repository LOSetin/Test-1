<%@ Page Title="" Language="C#" MasterPageFile="../Master/MasterPage.master" AutoEventWireup="true" CodeFile="GroupManager.aspx.cs" Inherits="GroupManager" %>
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
    
    <div class="Info">在这里，您可以对线路：<span class="red bold"><asp:Literal ID="ltTips" runat="server"></asp:Literal></span> 的旅行团进行管理<br />
        点击返回：可返回线路管理界面<br />
        复制添加：会增加一个新团，新团出团日期和返回日期均自动增加一天。
    </div>

    <div class="H20"></div>
        <div style="text-align:center">
            <table style="width:100%;padding:0; margin:0; ">
                <tr><td style="width:250px; text-align:left;"><asp:Button ID="btnReturn" runat="server" Text="返回" CssClass="btnGray" OnClick="btnReturn_Click" /> <asp:Button ID="btnAdd" runat="server" Text="添加" CssClass="btnAdd" OnClick="btnAdd_Click" /> </td>
                    <td style="padding-right:250px">
                        出团日期：
                    <asp:TextBox ID="tbBegin" runat="server" CssClass="EditForm_textbox" bj="cBj" Width="100px" ></asp:TextBox>&nbsp;&nbsp;
                    至&nbsp;&nbsp;
                    <asp:TextBox ID="tbEnd" runat="server" CssClass="EditForm_textbox" bj="cBj" Width="100"></asp:TextBox>&nbsp;&nbsp;<asp:Button ID="btnConfirm" runat="server" CssClass="btnPrime" Text="搜索" OnClick="btnConfirm_Click" />
                    </td>
                </tr>
            </table>
        </div>
    <div class="Line"></div>
    <div style="text-align:center;">        <span class="subTitle">当前线路：<asp:Literal ID="ltTitle" runat="server"></asp:Literal></span>
</div>
    <div class="H20"></div>
      <Arrow:GridView  ID="gvData" runat="server" CssClass="gridview" Width="100%"
            HorizontalAlign="Center" GridLines="None" CellPadding="5" AutoGenerateColumns="False" OnRowDataBound="gvData_RowDataBound" OnRowCommand="gvData_RowCommand"
           AllowPaging="False"
            PagerStyle-CssClass="gridpage" AlternatingRowStyle-BackColor="#f8f8f8" DataKeyNames="ID" Ascending="False" ButtonCount="10" Condition="" ConnectionString="" FirstPageWord="首页" LastPageWord="末页" NextPageWord="下页" OrderBy="" PageIndex="1" PageSize="10" PageStyle="Full" PrevPageWord="上页" PrimaryKey="" QueryFields="*" SqlCreateType="TopN" TableName="" UrlQuery="">
            <alternatingrowstyle backcolor="#F8F8F8"></alternatingrowstyle>
            <columns>
                <asp:TemplateField HeaderText="出团日期">
                    <ItemTemplate>
                        <%#  Eval("GoDate").ToArrowDateTime().ToDateOnlyString()%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="返回日期">
                    <ItemTemplate>
                        <%#  Eval("BackDate").ToArrowDateTime().ToDateOnlyString()%>
                    </ItemTemplate>
                </asp:TemplateField>
            <asp:BoundField DataField="GruopLeader" HeaderText="带团人" />
            <asp:BoundField DataField="TotalNum" HeaderText="普通团位总数" />
                 <asp:BoundField DataField="JoinNum" HeaderText="报名人数" />
                 <asp:BoundField DataField="InnerPrice" HeaderText="会员价" />
                 <asp:BoundField DataField="OuterPrice" HeaderText="普通价" />
                 <asp:BoundField DataField="Deposit" HeaderText="订金" />

                  <asp:BoundField DataField="AddUserRealName" HeaderText="添加人" />
                   <asp:BoundField DataField="AddTime" HeaderText="添加时间" />
               
                 <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        
                      <asp:LinkButton ID="lbtnUpdate" runat="server" Text="修改" CommandArgument='<%# Eval("ID") %>' CommandName="UpdateData"></asp:LinkButton>
                      <asp:LinkButton ID="lbtnDel" runat="server" Text="删除" CommandArgument='<%# Eval("ID") %>' CommandName="DelData" ></asp:LinkButton>
                         <asp:LinkButton ID="lbtnAdd" runat="server" Text="复制添加" CommandArgument='<%# Eval("ID") %>' CommandName="AddData" ></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
        </columns>
           
            <pagerstyle cssclass="gridpage"></pagerstyle>

        </Arrow:GridView>

    <div class="H20"></div>


</asp:Content>

