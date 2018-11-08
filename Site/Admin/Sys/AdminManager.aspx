<%@ Page Title="" Language="C#" MasterPageFile="../Master/MasterPage.master" AutoEventWireup="true" CodeFile="AdminManager.aspx.cs" Inherits="Station_DealRecord" %>
<%@ Import Namespace="Arrow.Framework.Extensions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="Info">在这里，您可以管理管理员<br />
        修改真名：可修改用户真实姓名<br />
        重置密码：用户密码重置为admin<br />
        启用：用户状态设为启用，可登陆使用系统<br />
        禁用：用户状态设为禁用，不可登陆使用系统
    </div>

    <div class="H20"></div>
        <div style="text-align:center">
            真名：<asp:TextBox ID="tbName" runat="server"   Width="150px"  CssClass="EditForm_textbox"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="确定" CssClass="btnPrime" OnClick="btnSearch_Click" />
        </div>
    <div class="Line"></div>
    <div>
       <div class="H10"></div>

        <Arrow:GridView  ID="gvData" runat="server" CssClass="gridview" Width="95%"
            HorizontalAlign="Center" GridLines="None" CellPadding="5" AutoGenerateColumns="False" OnRowDataBound="gvData_RowDataBound" OnRowCommand="gvData_RowCommand"
           AllowPaging="False"
            PagerStyle-CssClass="gridpage" AlternatingRowStyle-BackColor="#f8f8f8" DataKeyNames="Name" Ascending="False" ButtonCount="10" Condition="" ConnectionString="" FirstPageWord="首页" LastPageWord="末页" NextPageWord="下页" OrderBy="" PageIndex="1" PageSize="10" PageStyle="Full" PrevPageWord="上页" PrimaryKey="" QueryFields="*" SqlCreateType="TopN" TableName="" UrlQuery="">
            <alternatingrowstyle backcolor="#F8F8F8"></alternatingrowstyle>
            <columns>
            <asp:BoundField DataField="Name" HeaderText="用户名" />
             <asp:TemplateField HeaderText="真名">
                    <ItemTemplate>
                       <asp:TextBox ID="tbRealName" runat="server" Width="100" CssClass="EditForm_textbox" Text='<%# Eval("RealName") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="角色">
                    <ItemTemplate>
                        <%# Eval("RoleIDs").ToArrowString()=="0"?"超级管理员":"普通管理员" %>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:BoundField DataField="UserStatus" HeaderText="状态" />
                <asp:BoundField DataField="LastLoginIP" HeaderText="上次登陆IP" />
                <asp:BoundField DataField="LastLoginTime" HeaderText="上次登录时间" />
            <asp:BoundField DataField="ThisLoginIP" HeaderText="本次登陆IP" />
            <asp:BoundField DataField="ThisLoginTime" HeaderText="本次登陆时间" />
            <asp:BoundField DataField="InviteNum" HeaderText="邀请码" />
                 <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        
                      <asp:LinkButton ID="lbtnUpdate" runat="server" Text="修改真名" CommandArgument='<%# Eval("Name") %>' CommandName="UpdateRealName"></asp:LinkButton>
                        <asp:LinkButton ID="lbtnReset" runat="server" Text="重置密码" CommandArgument='<%# Eval("Name") %>' CommandName="ReSetPwd"></asp:LinkButton>
                        <asp:LinkButton ID="lbtnEnable" runat="server" Text="启用" CommandArgument='<%# Eval("Name") %>' CommandName="Enable" Visible='<%# Eval("RoleIDs").ToArrowString()=="0"?false:true %>'></asp:LinkButton>
                      <asp:LinkButton ID="lbtnDisabled" runat="server" Text="禁用" CommandArgument='<%# Eval("Name") %>' CommandName="Disabled" Visible='<%# Eval("RoleIDs").ToArrowString()=="0"?false:true %>'></asp:LinkButton>
                        <asp:LinkButton ID="lbtnDel" runat="server" Text="删除" CommandArgument='<%# Eval("Name") %>' CommandName="Del" Visible='<%# Eval("RoleIDs").ToArrowString()=="0"?false:true %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
        </columns>
           
            <pagerstyle cssclass="gridpage"></pagerstyle>

        </Arrow:GridView>

</div>
    <div class="H20"></div>


</asp:Content>

