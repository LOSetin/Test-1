<%@ Page Title="" Language="C#" MasterPageFile="../Master/MasterPage.master" AutoEventWireup="true" CodeFile="GoodsCat.aspx.cs" Inherits="GoodsCat" %>
<%@ Import Namespace="Arrow.Framework.Extensions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="Info">在这里，您可以管理商品分类<br />
        排序号越小越靠前
    </div>

    <div class="H20"></div>
        <div style="text-align:center">
            分类名称：<asp:TextBox ID="tbName" runat="server"   Width="150px"  CssClass="EditForm_textbox"></asp:TextBox> <span class="red">*</span>&nbsp;&nbsp;
            排序号：<asp:TextBox ID="tbSortOrder" runat="server" Width="100px" CssClass="EditForm_textbox"></asp:TextBox> &nbsp;&nbsp;
            <asp:Button ID="btnConfirm" runat="server" Text="添加" CssClass="btnPrime" OnClick="btnConfirm_Click" />
        </div>
    <div class="Line"></div>
    <div>
       <div class="H10"></div>

        <Arrow:GridView  ID="gvData" runat="server" CssClass="gridview" Width="95%"
            HorizontalAlign="Center" GridLines="None" CellPadding="5" AutoGenerateColumns="False" OnRowDataBound="gvData_RowDataBound" OnRowCommand="gvData_RowCommand"
           AllowPaging="False"
            PagerStyle-CssClass="gridpage" AlternatingRowStyle-BackColor="#f8f8f8" DataKeyNames="ID" Ascending="False" ButtonCount="10" Condition="" ConnectionString="" FirstPageWord="首页" LastPageWord="末页" NextPageWord="下页" OrderBy="" PageIndex="1" PageSize="10" PageStyle="Full" PrevPageWord="上页" PrimaryKey="" QueryFields="*" SqlCreateType="TopN" TableName="" UrlQuery="">
            <alternatingrowstyle backcolor="#F8F8F8"></alternatingrowstyle>
            <columns>
            <asp:BoundField DataField="ID" HeaderText="分类ID" />
             <asp:TemplateField HeaderText="分类名">
                    <ItemTemplate>
                       <asp:TextBox ID="tbName" runat="server" Width="150" CssClass="EditForm_textbox" Text='<%# Eval("Name") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="排序号">
                    <ItemTemplate>
                       <asp:TextBox ID="tbOrder" runat="server" Width="100" CssClass="EditForm_textbox" Text='<%# Eval("SortOrder") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
               
                 <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        
                      <asp:LinkButton ID="lbtnUpdate" runat="server" Text="修改" CommandArgument='<%# Eval("ID") %>' CommandName="UpdateData"></asp:LinkButton>
                      <asp:LinkButton ID="lbtnDel" runat="server" Text="删除" CommandArgument='<%# Eval("ID") %>' CommandName="DelData" ></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
        </columns>
           
            <pagerstyle cssclass="gridpage"></pagerstyle>

        </Arrow:GridView>

</div>
    <div class="H20"></div>


</asp:Content>

