<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditorTest.aspx.cs" Inherits="EditorTest" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="tinymce/tinymce.min.js"></script>
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

<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
      
    <div>
    <asp:TextBox ID="tbContent" runat="server" TextMode="MultiLine" ClientIDMode="Static"></asp:TextBox>
        <asp:Button ID="btnTest" runat="server" Text="提交内容" OnClick="btnTest_Click" />
    </div>
          <div><asp:Literal ID="ltContent"  runat="server"></asp:Literal> </div>
    </form>
</body>
</html>
