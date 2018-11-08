<%@ Page Title="" Language="C#" MasterPageFile="../Master/MasterPage.master" AutoEventWireup="true" CodeFile="ScheduleManager.aspx.cs" Inherits="ScheduleManager" %>
<%@ Import Namespace="Arrow.Framework.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../../css/admin.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="Info">在这里，您可以对线路：<span class="red bold"><asp:Literal ID="ltTips1" runat="server"></asp:Literal></span> 的行程进行管理<br />
        点击返回：可返回线路管理界面
    </div>

    <div class="H20"></div>
        <div style="text-align:center">
            <table style="width:100%;padding:0; margin:0; ">
                <tr><td style="width:250px; text-align:left;"><asp:Button ID="btnReturn" runat="server" Text="返回" CssClass="btnGray" OnClick="btnReturn_Click" /> <asp:Button ID="btnAdd" runat="server" Text="添加" CssClass="btnAdd" OnClick="btnAdd_Click" /> </td>
                    <td style="padding-right:250px"><span class="subTitle">当前线路：<asp:Literal ID="ltTips" runat="server"></asp:Literal> </span>
                    </td>
                </tr>
            </table>
        </div>
    <div class="Line"></div>
    <div class="H10"></div>
      <Arrow:GridView  ID="gvData" runat="server" CssClass="gridview" Width="100%"
            HorizontalAlign="Center" GridLines="None" CellPadding="5" AutoGenerateColumns="False" OnRowDataBound="gvData_RowDataBound" OnRowCommand="gvData_RowCommand"
           AllowPaging="False"
            PagerStyle-CssClass="gridpage" AlternatingRowStyle-BackColor="#f8f8f8" DataKeyNames="ID" Ascending="False" ButtonCount="10" Condition="" ConnectionString="" FirstPageWord="首页" LastPageWord="末页" NextPageWord="下页" OrderBy="" PageIndex="1" PageSize="10" PageStyle="Full" PrevPageWord="上页" PrimaryKey="" QueryFields="*" SqlCreateType="TopN" TableName="" UrlQuery="">
            <alternatingrowstyle backcolor="#F8F8F8"></alternatingrowstyle>
            <columns>
                <asp:TemplateField HeaderText="行程次序">
                    <ItemTemplate>
                        <%# "第" + Eval("SortOrder")+"天" %>
                    </ItemTemplate>
                </asp:TemplateField>
            
                <asp:TemplateField HeaderText="行程标题">
                    <ItemTemplate>
                        <%# IconHelper.Replace(Eval("Title").ToArrowString()) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="包含缩略图">
                    <ItemTemplate>
                        <%# SiteUtility.ShowAllImage(Eval("DayPics").ToArrowString(),30,30,true) %>
                    </ItemTemplate>
                </asp:TemplateField>

                  <asp:BoundField DataField="AddUserRealName" HeaderText="添加人" />
                   <asp:BoundField DataField="AddTime" HeaderText="添加时间" />
               
                 <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        
                      <asp:LinkButton ID="lbtnUpdate" runat="server" Text="修改" CommandArgument='<%# Eval("ID") %>' CommandName="UpdateData"></asp:LinkButton>
                      <asp:LinkButton ID="lbtnDel" runat="server" Text="删除" CommandArgument='<%# Eval("ID") %>' CommandName="DelData" ></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
        </columns>
           
            <pagerstyle cssclass="gridpage"></pagerstyle>

        </Arrow:GridView>

    <div class="H20"></div>


</asp:Content>

