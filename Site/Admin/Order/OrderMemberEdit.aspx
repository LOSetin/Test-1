<%@ Page Title="" Language="C#" MasterPageFile="../Master/MasterPage.master" AutoEventWireup="true" CodeFile="OrderMemberEdit.aspx.cs" Inherits="GroupMemberEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="Info">
       在这里，您可以修改订单中参团者的资料。

    </div>

    <div class="H20"></div>
        <div style="text-align:center">
            <table style="width:100%;padding:0; margin:0; ">
                <tr>
                    <td style="width:200px; text-align:left;">
                        <asp:Button ID="btnReturn" runat="server" Text="返回" CssClass="btnGray" OnClick="btnReturn_Click" />
                        <asp:Button ID="btnConfirm" runat="server" Text="更新" CssClass="btnPrime" OnClick="btnUpdate_Click" /></td>
                    <td style=" text-align:center; padding-right:200px; color:red; font-weight:bold;">
                        <asp:Literal ID="ltMsg" runat="server"></asp:Literal>
                    </td>
                </tr>
            </table>
        </div>

    <div class="Line"></div>
    <div class="H10"></div>
    <div>
       <Arrow:GridView ID="gvData" runat="server"  PagerStyle-CssClass="gridpage" AlternatingRowStyle-BackColor="#f8f8f8" CssClass="gridview" Width="100%"
             HorizontalAlign="Center" GridLines="None" CellPadding="5"
           AutoGenerateColumns="false" OnRowDataBound="gvData_RowDataBound" >
           <Columns>
               <asp:TemplateField HeaderText="真实姓名">
                   <ItemTemplate>
                       <asp:HiddenField ID="hfID" runat="server" Value='<%# Eval("ID") %>' />
                       <asp:TextBox ID="tbRealName" runat="server" Text='<%# Eval("RealName") %>' CssClass="EditForm_textbox"></asp:TextBox>
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
                         <asp:TextBox ID="tbIDNum" runat="server" Text='<%# Eval("IDNum") %>' CssClass="EditForm_textbox"></asp:TextBox>
                   </ItemTemplate>
               </asp:TemplateField>
                <asp:TemplateField HeaderText="联系电话">
                   <ItemTemplate>
                         <asp:TextBox ID="tbPhone" runat="server" Text='<%# Eval("MobileNum") %>' CssClass="EditForm_textbox"></asp:TextBox>
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
    <div class="H20"></div>


</asp:Content>

