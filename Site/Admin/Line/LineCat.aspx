<%@ Page Title="" Language="C#" MasterPageFile="../Master/MasterPage.master" AutoEventWireup="true" CodeFile="LineCat.aspx.cs" Inherits="LineCat" %>
<%@ Import Namespace="Arrow.Framework.Extensions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="Info">在这里，您可以管理线路分类<br />
        排序号越小越靠前<br />
        <span class="red bold">是否热卖，是否首页显示，是否在热卖排行显示均为针对首页的设置。</span>
        <br />是否热卖：只能设置二级分类，最多设置5个
        <br />是否首页显示，是否热卖排行只能设置一级分类，是否首页显示最多设置2个，是否热卖排行最多设置3个
        <br /><a style="color:red;font-weight:bold"  href='../Resources/Help/help1.png' target="_blank">点击此图显示详细说明</a>
        <br />输入设置好后，点击“一键更新”，可保存所有设置
    </div>

    <div class="H20"></div>
        <div style="text-align:center">显示：<asp:DropDownList ID="ddlShow" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlShow_SelectedIndexChanged"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;&nbsp;
            上级分类：<asp:DropDownList ID="ddlParent" runat="server"></asp:DropDownList>
            分类名称：<asp:TextBox ID="tbName" runat="server"   Width="150px"  CssClass="EditForm_textbox"></asp:TextBox>
            排序号：<asp:TextBox ID="tbSortOrder" runat="server" Width="100px" CssClass="EditForm_textbox"></asp:TextBox> 
            <asp:Button ID="btnConfirm" runat="server" Text="添加" CssClass="btnPrime" OnClick="btnConfirm_Click" />
             <asp:Button ID="btnUpdateAll" runat="server" Text="一键更新" CssClass="btnBlue" OnClick="btnUpdateAll_Click" />
        </div>
    <div class="Line"></div>
    <div>
           
       <div class="H10"></div>

    

  <Arrow:TreeListView ID="gvData" runat="server"  Width="100%"  HorizontalAlign="Center" GridLines="None" CellPadding="0" AutoGenerateColumns="False"
  DataKeyNames="ID"    NodeColumnIndex="1" ParentKey="ParentID" CssClass="treelistview"
            ColumnKey="ID" SortKey="" ExpendDepth="1" ClientIDMode="Static"
           OnRowDataBound="gvData_RowDataBound" OnRowCommand="gvData_RowCommand"
           >
           <Columns>
               <asp:BoundField DataField="ID" HeaderText="分类编号" ItemStyle-Width="70" />
               <asp:BoundField DataField="Name" HeaderText="分类名称" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200" ItemStyle-CssClass="treelistcolumn"  />
              <asp:TemplateField HeaderText="新名称" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100" ItemStyle-CssClass="treelistcolumn" >
                  <ItemTemplate>
                      <asp:TextBox ID="tbName" runat="server" Width="150" CssClass="EditForm_textbox" Text=''></asp:TextBox>
                  </ItemTemplate>
              </asp:TemplateField>

                  <asp:TemplateField HeaderText="排序号" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100" ItemStyle-CssClass="treelistcolumn" >
                  <ItemTemplate>
                      <asp:TextBox ID="tbSort" runat="server" Width="50" CssClass="EditForm_textbox" Text='<%# Eval("SortOrder") %>'></asp:TextBox>
                      <asp:HiddenField ID="hfID" runat="server" Value='<%#Eval("ID") %>' />
                  </ItemTemplate>
              </asp:TemplateField>

               <asp:TemplateField HeaderText="是否热卖">
                   <ItemTemplate>
                       <asp:CheckBox ID="chkHot" runat="server" Visible='<%# Eval("ParentID").ToArrowInt()==0?false:true %>'  Checked='<%# Eval("IsHotSell").ToArrowInt()==1? true :false %>' />
                   </ItemTemplate>
               </asp:TemplateField>


                    <asp:TemplateField HeaderText="是否首页显示">
                          <ItemTemplate>
                       <asp:CheckBox ID="chkIndexShow" runat="server" Visible='<%# Eval("ParentID").ToArrowInt()==0?true:false %>'  Checked='<%# Eval("IsShowIndex").ToArrowInt()==1? true :false %>' />
                   </ItemTemplate>
                    </asp:TemplateField>

                 <asp:TemplateField HeaderText="是否在热卖排行显示">
                         <ItemTemplate>
                       <asp:CheckBox ID="chkHotSort" runat="server" Visible='<%# Eval("ParentID").ToArrowInt()==0?true:false %>'  Checked='<%# Eval("IsHotSort").ToArrowInt()==1? true :false %>' />
                   </ItemTemplate>
                 </asp:TemplateField>
          
                <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200">
                   <ItemTemplate>
                       <asp:LinkButton ID="lbtnUpdate" runat="server" Text="修改" CommandArgument='<%# Eval("ID")  %>' CommandName="DataUpdate"></asp:LinkButton>
                       <asp:LinkButton ID="lbtnDel" runat="server" Text="删除" CommandArgument='<%#Eval("ID")  %>' CommandName="DataDel"></asp:LinkButton>
                   </ItemTemplate>
               </asp:TemplateField>
           </Columns>
       <RowStyle HorizontalAlign="Center" />
      <HeaderStyle HorizontalAlign="Center" />
       </Arrow:TreeListView>

</div>
    <div class="H20"></div>
      <script src="../Resources/js/GridViewSitchColor.js"></script>

</asp:Content>

