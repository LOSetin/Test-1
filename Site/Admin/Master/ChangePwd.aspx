<%@ Page Title="" Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="ChangePwd.aspx.cs" Inherits="Common_ChangePwd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="Info">在这里，您可以修改您的密码。</div>

    <div class="H20"></div>
        
      <div class="Edit">
          <table class="EditForm">
              <tr>
                  <td class="txt">旧密码：</td>
                  <td>
                      <asp:TextBox ID="tbOldPwd" runat="server" TextMode="Password" CssClass="EditForm_textbox" ClientIDMode="Static" ></asp:TextBox> <span class="EditForm_red">*</span>
                  </td>
              </tr>
              <tr>
                  <td class="txt">新密码：</td>
                  <td>
                      <asp:TextBox ID="tbNewPwd1" runat="server" TextMode="Password" CssClass="EditForm_textbox" ClientIDMode="Static"></asp:TextBox> <span class="EditForm_red">*</span>
                  </td>
              </tr>
              <tr>
               <td class="txt">重复新密码：</td>
                  <td>
                      <asp:TextBox ID="tbNewPwd2" runat="server" TextMode="Password" CssClass="EditForm_textbox" ClientIDMode="Static"></asp:TextBox> <span class="EditForm_red">*</span>
                  </td>
              </tr>

              <tr>
                  <td class="txt"></td>
                  <td style="padding-top: 10px;">
                      <asp:Button ID="btnSubmit" runat="server" Text="确定修改" CssClass="btnPrime" OnClick="btnSubmit_Click" />
                      &nbsp; <asp:Button ID="btnClean" runat="server" Text="清空" CssClass="btnGray" OnClick="btnClean_Click"/>
                  </td>
              </tr>
          </table>
    </div>
     
    <div class="H20"></div>


</asp:Content>

