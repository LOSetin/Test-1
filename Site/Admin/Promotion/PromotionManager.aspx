<%@ Page Title="" Language="C#" MasterPageFile="../Master/MasterPage.master" AutoEventWireup="true" CodeFile="PromotionManager.aspx.cs" Inherits="PromotionManager" %>
<%@ Import Namespace="Arrow.Framework.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="Info">在这里，您可以对促销进行管理。<br />
        点击缩略图可查看大图。<br />
        添加促销团：可以为该促销添加促销团。<br />
        促销团管理：可以修改或删除该促销的促销团设置
    </div>

    <div class="H20"></div>
        <div style="text-align:center">
            <table style="width:100%;padding:0; margin:0; ">
                <tr>
                    <td style="text-align:center"> 促销关键字：<asp:TextBox ID="tbKeyWord" runat="server" CssClass="EditForm_textbox" Width="150"></asp:TextBox>
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
                <asp:TemplateField HeaderText="促销名">
                    <ItemTemplate>
                        <asp:Literal ID="tbName" runat="server" Text='<%# Eval("Name") %>'></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="缩略图">
                    <ItemTemplate>
                        <%# SiteUtility.ShowImage(Eval("CoverPath").ToArrowString(),50,50,true) %>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="促销类型">
                    <ItemTemplate>
                      <%# Eval("PromotionType") %>
                    </ItemTemplate>
                </asp:TemplateField>

                  <asp:TemplateField HeaderText="促销描述">
                    <ItemTemplate>
                      <%# PromotionBLL.ShowPromotionDetail(Eval("PromotionType").ToArrowString(), Eval("Discount").ToArrowString(),Eval("TotalPayOneTime").ToArrowString(),Eval("TotalPayOneTimeJoinNum").ToArrowString())%>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        
                      <asp:LinkButton ID="lbtnUpdate" runat="server" Text="修改" CommandArgument='<%# Eval("ID") %>' CommandName="UpdateData"></asp:LinkButton>
                      <asp:LinkButton ID="lbtnDel" runat="server" Text="删除" CommandArgument='<%# Eval("ID") %>' CommandName="DelData" ></asp:LinkButton>
                        <asp:LinkButton ID="lbtnAddGroup" runat="server" Text="添加促销团" CommandArgument='<%# Eval("ID") %>' CommandName="AddPromotion"></asp:LinkButton> 
                         <asp:LinkButton ID="lbtnGroupManager" runat="server" Text="促销团管理" CommandArgument='<%# Eval("ID") %>' CommandName="GroupManager"></asp:LinkButton> 
                    </ItemTemplate>
                </asp:TemplateField>
        </columns>
           
            <pagerstyle cssclass="gridpage"></pagerstyle>

        </Arrow:GridView>

    <div class="H20"></div>


</asp:Content>

