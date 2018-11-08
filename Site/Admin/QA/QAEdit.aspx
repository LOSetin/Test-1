<%@ Page Title="" Language="C#" MasterPageFile="../Master/MasterPage.master" AutoEventWireup="true" CodeFile="QAEdit.aspx.cs" Inherits="PromotionEdit" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../../tinymce/tinymce.min.js"></script>
    <script>
  tinymce.init({
      selector: '#tbContent',
      width: 750,
      height: 300,
      language: 'zh_CN',
      toolbar: [
    'undo redo | bold italic | link dotnetimages | forecolor backcolor | alignleft aligncenter alignright | code'
      ],
      menubar: false,  // removes the menubar
      plugins: [
  'advlist autolink link image dotnetimages lists charmap print preview hr anchor pagebreak spellchecker',
  'searchreplace wordcount visualblocks visualchars code fullscreen insertdatetime media nonbreaking',
  'save table contextmenu directionality emoticons template paste textcolor'
      ],
  });
  </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="Info"><asp:Literal ID="ltTips" runat="server"></asp:Literal> </div>

    <div class="H20"></div>
        
    <div >
        <table class="EditForm">
            <tr>
                <td class="txt">标题：</td>
                <td>
                    <asp:TextBox ID="tbName" runat="server" CssClass="EditForm_textbox" Width="250px" ></asp:TextBox>&nbsp;&nbsp;<span class="EditForm_red">*必填</span></td>
            </tr>

          

               <tr>
                <td class="txt">分类：</td>
                <td>
                    <asp:DropDownList ID="ddlCat" runat="server" ></asp:DropDownList>
                    </td>
            </tr>

               <tr>
                <td class="txt">内容：</td>
                <td>

                    <asp:TextBox ID="tbContent" runat="server" TextMode="MultiLine" ClientIDMode="Static"></asp:TextBox>

                   </td>
            </tr>

            
            <tr>
                <td class="txt"></td>
                <td style="padding-top: 10px;">
                    <asp:Button ID="btnSubmit" runat="server" Text="确定添加" CssClass="btnPrime" OnClick="btnSubmit_Click" />
                    &nbsp;
                    <asp:Button ID="btnClean" runat="server" Text="返回" CssClass="btnGray" OnClick="btnClean_Click" />
                </td>
            </tr>
        </table>
    </div>
     
    <div class="H20"></div>


</asp:Content>

