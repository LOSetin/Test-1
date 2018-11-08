<%@ Page Title="" Language="C#" MasterPageFile="../Master/MasterPage.master" AutoEventWireup="true" CodeFile="PhotoManager.aspx.cs" Inherits="PhotoManager" %>
<%@ Import Namespace="Arrow.Framework.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="Info">在这里，您可以对照片进行管理。<br />
        点击缩略图可查看大图。
    </div>

    <div class="H20"></div>
        <div style="text-align:center">
            <table style="width:100%;padding:0; margin:0; ">
                <tr>
                    <td style="text-align:center"> 照片关键字：<asp:TextBox ID="tbKeyWord" runat="server" CssClass="EditForm_textbox" Width="150"></asp:TextBox>
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
            PagerStyle-CssClass="gridpage" AlternatingRowStyle-BackColor="#f8f8f8" DataKeyNames="ID" Ascending="False" ButtonCount="10" Condition="" ConnectionString="" FirstPageWord="首页" LastPageWord="末页" NextPageWord="下页" OrderBy="" PageIndex="1" PageSize="10" PageStyle="Full" PrevPageWord="上页" PrimaryKey="" QueryFields="*" SqlCreateType="TopN" TableName="" UrlQuery="">
            <alternatingrowstyle backcolor="#F8F8F8"></alternatingrowstyle>
            <columns>
                <asp:TemplateField HeaderText="照片名">
                    <ItemTemplate>
                        <asp:TextBox ID="tbName" runat="server"  CssClass="EditForm_textbox" Text='<%# Eval("Name") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="缩略图">
                    <ItemTemplate>
                        <%# SiteUtility.ShowImage(Eval("CoverPath").ToArrowString(),50,50,true) %>
                    </ItemTemplate>
                </asp:TemplateField>
             
                  <asp:BoundField DataField="AddUserRealName" HeaderText="添加人" />
                   <asp:BoundField DataField="AddTime" HeaderText="添加时间" />
               
                 <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        
                      <asp:LinkButton ID="lbtnUpdate" runat="server" Text="修改名称" CommandArgument='<%# Eval("ID") %>' CommandName="UpdateData"></asp:LinkButton>
                      <asp:LinkButton ID="lbtnDel" runat="server" Text="删除照片" CommandArgument='<%# Eval("ID") %>' CommandName="DelData" ></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
        </columns>
           
            <pagerstyle cssclass="gridpage"></pagerstyle>

        </Arrow:GridView>

    <div class="H20"></div>


</asp:Content>

