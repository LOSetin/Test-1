<%@ Page Title="" Language="C#" MasterPageFile="~/SiteTest/MasterPage.master" AutoEventWireup="true" CodeFile="OrderDetailEdit.aspx.cs" Inherits="OrderDetailEdit" %>
<%@ Import Namespace="Arrow.Framework.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .NotShowPage{display:none;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p>
        操作提示：您可以在此编辑参团人。
    </p>
    <p>线路名：<asp:Literal ID="ltLineName" runat="server"></asp:Literal> </p>
    <p>促销：<asp:Literal ID="ltPromotionName" runat="server"></asp:Literal>  </p>
    <p>单价：<asp:Literal id="ltPrice" runat="server"></asp:Literal> </p>
    <p>总价：<asp:Literal ID="ltTotal" runat="server"></asp:Literal> </p>
    <br /><br />
    <p>您购买了<asp:Literal ID="ltBuyNum" runat="server"></asp:Literal>个团位，请填写下表，填写完后，请点击下面的更新按钮 </p>
    <p> <asp:Button ID="btnUpdate" runat="server" Text="更新参团者资料" OnClick="btnUpdate_Click" /> 
        <asp:Button ID="btnReturn" runat="server" Text="返回订单列表" OnClick="btnReturn_Click" />
    </p>
    <div>
       <Arrow:GridView ID="gvData" runat="server"
           AutoGenerateColumns="false" OnRowCommand="gvData_RowCommand" OnRowDataBound="gvData_RowDataBound" >
           <Columns>
               <asp:TemplateField HeaderText="真实姓名">
                   <ItemTemplate>
                       <asp:HiddenField ID="hfID" runat="server" Value='<%# Eval("ID") %>' />
                       <asp:TextBox ID="tbRealName" runat="server" Text='<%# Eval("RealName") %>'></asp:TextBox>
                   </ItemTemplate>
               </asp:TemplateField>
                <asp:TemplateField HeaderText="性别">
                   <ItemTemplate>
                         <asp:DropDownList ID="ddlSex" runat="server" >
                             <asp:ListItem Value="男" Text="男"></asp:ListItem>
                             <asp:ListItem Value="女" Text="女"></asp:ListItem>
                         </asp:DropDownList>
                       <asp:HiddenField ID="hfSex" runat="server" Value='<%# Eval("Sex") %>' />
                   </ItemTemplate>
               </asp:TemplateField>
                <asp:TemplateField HeaderText="身份证">
                   <ItemTemplate>
                         <asp:TextBox ID="tbIDNum" runat="server" Text='<%# Eval("IDNum") %>'></asp:TextBox>
                   </ItemTemplate>
               </asp:TemplateField>
                <asp:TemplateField HeaderText="联系电话">
                   <ItemTemplate>
                         <asp:TextBox ID="tbPhone" runat="server" Text='<%# Eval("MobileNum") %>'></asp:TextBox>
                   </ItemTemplate>
               </asp:TemplateField>
                
               
                <asp:TemplateField HeaderText="操作" Visible="false">
                   <ItemTemplate>
                       <asp:LinkButton ID="lbtnUpdate" runat="server" Text="修改" CommandArgument='<%# Eval("ID") %>' CommandName="EditData"></asp:LinkButton>
                       &nbsp;&nbsp;
                        <asp:LinkButton ID="lbtnDel" runat="server" Text="删除" CommandArgument='<%# Eval("ID") %>' CommandName="DelData"></asp:LinkButton>
                    
                   </ItemTemplate>
               </asp:TemplateField>
           </Columns>
       </Arrow:GridView>
    </div>
</asp:Content>

