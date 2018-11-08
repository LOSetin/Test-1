<%@ Page Title="" Language="C#" MasterPageFile="../Master/MasterPage.master" AutoEventWireup="true" CodeFile="GoodsManager.aspx.cs" Inherits="GoodsManager" %>
<%@ Import Namespace="Arrow.Framework.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="Info">在这里，您可以对商品进行管理。<br />
       如果商品下架，将不会显示在页面。<br />
        点击缩略图可查看大图。
    </div>

    <div class="H20"></div>
        <div style="text-align:center">
            <table style="width:100%;padding:0; margin:0; ">
                <tr><td style="width:150px; text-align:left;"><asp:Button ID="btnAdd" runat="server" Text="添加" CssClass="btnAdd" OnClick="btnAdd_Click" /> </td>
                    <td style="padding-right:150px"> 商品关键字：<asp:TextBox ID="tbKeyWord" runat="server" CssClass="EditForm_textbox" Width="150"></asp:TextBox> &nbsp;
                     <asp:DropDownList ID="ddlCat" runat="server"></asp:DropDownList>&nbsp;&nbsp;
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
            <asp:BoundField DataField="ID" HeaderText="商品ID" />
                <asp:TemplateField HeaderText="缩略图">
                    <ItemTemplate>
                        <%# SiteUtility.ShowImage(Eval("CoverPath").ToArrowString(),30,30,true) %>
                    </ItemTemplate>
                </asp:TemplateField>
             <asp:TemplateField HeaderText="商品名">
                    <ItemTemplate>
                        <%# Eval("Name") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="所属分类">
                    <ItemTemplate>
                        <%# Eval("CatName") %>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:BoundField DataField="Num" HeaderText="数量" />
                  <asp:BoundField DataField="Points" HeaderText="兑换点数" />
                 <asp:TemplateField HeaderText="是否下架">
                    <ItemTemplate>
                        <%# Eval("IsOut").ToArrowString()=="0"?"否":"是" %>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:BoundField DataField="AddUserRealName" HeaderText="添加人" />
                   <asp:BoundField DataField="AddTime" HeaderText="添加时间" />
               
                 <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        
                      <asp:LinkButton ID="lbtnUpdate" runat="server" Text="修改" CommandArgument='<%# Eval("ID") %>' CommandName="UpdateData"></asp:LinkButton>
                      <asp:LinkButton ID="lbtnDel" runat="server" Text="下架" CommandArgument='<%# Eval("ID") %>' CommandName="SetOut" ></asp:LinkButton>
                        <asp:LinkButton ID="lbtnRestore" runat="server" Text="上架" CommandArgument='<%# Eval("ID") %>' CommandName="Restore" ></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
        </columns>
           
            <pagerstyle cssclass="gridpage"></pagerstyle>

        </Arrow:GridView>

    <div class="H20"></div>


</asp:Content>

