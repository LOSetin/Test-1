<%@ Page Title="" Language="C#" MasterPageFile="../Master/MasterPage.master" AutoEventWireup="true" CodeFile="AdminEdit.aspx.cs" Inherits="AdminEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="Info"><asp:Literal ID="ltTips" runat="server"></asp:Literal> </div>

    <div class="H20"></div>
        
      <div class="Edit">
          <table class="EditForm">
              <tr>
                  <td class="txt">用户名：</td>
                  <td>
                      <asp:TextBox ID="tbUserName" runat="server"  CssClass="EditForm_textbox" ClientIDMode="Static" ></asp:TextBox> <span class="EditForm_red">* 一旦设定，不能修改</span>
                  </td>
              </tr>
              <tr>
                  <td class="txt">密码：</td>
                  <td>
                      <asp:TextBox ID="tbPwd" runat="server"  CssClass="EditForm_textbox" ClientIDMode="Static"></asp:TextBox> <span class="EditForm_red"><asp:Literal ID="ltPwdTips" runat="server"></asp:Literal> </span>
                  </td>
              </tr>

              <tr>
                  <td class="txt">真名：</td>
                  <td>
                      <asp:TextBox ID="tbRealName" runat="server"  CssClass="EditForm_textbox" ClientIDMode="Static"></asp:TextBox> <span class="EditForm_red">* </span>
                  </td>
              </tr>

              <tr>
                  <td class="txt">邀请码：</td>
                  <td>
                      <asp:TextBox ID="tbInviteNum" runat="server"  CssClass="EditForm_textbox" ClientIDMode="Static" MaxLength="4"></asp:TextBox> <span class="EditForm_red">* 四位数字，超管邀请码为0001， 一旦设定，不能修改</span>
                  </td>
              </tr>
           
              <tr>
                  <td class="txt"></td>
                  <td style="padding-top: 10px;">
                      <asp:Button ID="btnSubmit" runat="server" Text="确定修改" CssClass="btnPrime" OnClick="btnSubmit_Click" />
                      &nbsp; <asp:Button ID="btnClean" runat="server" Text="返回" CssClass="btnGray" OnClick="btnClean_Click"/>
                  </td>
              </tr>
          </table>
    </div>
     
    <div class="H20"></div>


</asp:Content>

