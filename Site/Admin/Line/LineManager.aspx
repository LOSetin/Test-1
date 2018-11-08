<%@ Page Title="" Language="C#" MasterPageFile="../Master/MasterPage.master" AutoEventWireup="true" CodeFile="LineManager.aspx.cs" Inherits="LineManager" %>
<%@ Import Namespace="Arrow.Framework.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="Info">在这里，您可以对线路进行管理。<br />
        行程管理：您可以添加和管理该线路每一天的详细行程。<br />
        旅游团管理：您可以添加和管理走该线路的旅行团<br />
        选中左侧的热卖，点击“批量设置”，可设置当前页面指定的线路是否热卖
    </div>

    <div class="H20"></div>
        <div style="text-align:center">
            <table style="width:100%;padding:0; margin:0; ">
                <tr><td style="width:150px; text-align:left;"><asp:Button ID="btnAdd" runat="server" Text="添加" CssClass="btnAdd" OnClick="btnAdd_Click" /> </td>
                    <td style="padding-right:150px"> 线路关键字：<asp:TextBox ID="tbKeyWord" runat="server" CssClass="EditForm_textbox" Width="150"></asp:TextBox> &nbsp;
                     <asp:DropDownList ID="ddlCat" runat="server"></asp:DropDownList>&nbsp;&nbsp;
                        <asp:Button ID="btnConfirm" runat="server" Text="搜索" CssClass="btnPrime" OnClick="btnConfirm_Click" />
                        <asp:Button ID="btnUpdateAll" runat="server" Text="批量设置" CssClass="btnBlue" OnClick="btnUpdateAll_Click" />
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
                <asp:TemplateField HeaderText="热卖">
                    <ItemTemplate><asp:CheckBox ID="chkHot" runat="server" Checked='<%# Eval("IsHot").ToArrowInt()==1? true :false %>' /> 
                        <asp:HiddenField ID="hfID" runat="server" Value='<%#Eval("ID") %>' />
                    </ItemTemplate>
                    
                </asp:TemplateField>
            <asp:BoundField DataField="Name" HeaderText="线路名" />
                <asp:TemplateField HeaderText="缩略图">
                    <ItemTemplate>
                        <%# SiteUtility.ShowImage(Eval("CoverPath").ToArrowString(),30,30,true) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="所属分类">
                    <ItemTemplate>
                       <%# LineBLL.ShowCatName(allCats,Eval("FirstCatID").ToArrowInt(),Eval("SecondCatID").ToArrowInt()) %>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:BoundField DataField="TravelDays" HeaderText="旅游天数" />
                 <asp:BoundField DataField="StartAddress" HeaderText="出发地" />
                  <asp:BoundField DataField="TargetAddress" HeaderText="目的地" />
                  <asp:BoundField DataField="AddUserRealName" HeaderText="添加人" />
                   <asp:BoundField DataField="AddTime" HeaderText="添加时间" />
               
                 <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        
                      <asp:LinkButton ID="lbtnUpdate" runat="server" Text="修改" CommandArgument='<%# Eval("ID") %>' CommandName="UpdateData"></asp:LinkButton>
                      <asp:LinkButton ID="lbtnDel" runat="server" Text="删除" CommandArgument='<%# Eval("ID") %>' CommandName="DelData" ></asp:LinkButton>
                         <asp:LinkButton ID="lbtnDetail" runat="server" Text="行程管理" CommandArgument='<%# Eval("ID") %>' CommandName="DetailData" ></asp:LinkButton>
                        <asp:LinkButton ID="lbtnGroup" runat="server" Text="旅游团管理" CommandArgument='<%# Eval("ID") %>' CommandName="GroupData" ></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
        </columns>
           
            <pagerstyle cssclass="gridpage"></pagerstyle>

        </Arrow:GridView>

    <div class="H20"></div>


</asp:Content>

