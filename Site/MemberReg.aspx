<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberReg.aspx.cs" Inherits="MemberReg" %>

<!DOCTYPE html>
<html>
<head>
<title>用户注册</title>

<meta charset="utf-8">
<link href="css/home.css" rel="stylesheet" type="text/css" />
</head>
<body>
<div class="wrap">
  <div class="logo"><img src="images/logo1.png" width="417" height="262" alt=""/></div>
  <div class="container">
        <form id="form1" runat="server">
    <div class="register-box1">
      <div class="reg-slogan1">用户注册 </div>
      <div class="reg-form1" > 
 
        <div class="cell">
         
          <span>用户名:</span><input id="tbUserName" runat="server" type="text" placeholder="输入用户名" value="" name="" class="text1" maxlength="11"  /> <span class="bt">*必填</span>
        </div>
        <div class="cell">
          <span>密码:</span>
          <input type="password" id="tbPwd1" runat="server"  value="" placeholder="填写密码"  class="text1" />
       <span class="bt">*必填</span>
         </div>
         <div class="cell">        
          <span>重复密码:</span><input id="tbPwd2" runat="server" type="password" value="" placeholder="重复填写密码" name="" class="text1" maxlength="11" /> <span class="bt">*必填</span>
        </div>

        <div class="cell">        
          <span>邀请码:</span><input type="text" id="tbInviteNum" runat="server" value="" name="" class="text1" maxlength="11" placeholder="填写邀请码" /> <span class="bt">*必填</span>
        </div>
          <div class="bottom"><asp:Button ID="btnReg" runat="server" Text="注册" CssClass="button btn-green" OnClick="btnReg_Click" /> </div><br />
          <div class="bottom"><asp:Button ID="btnLogin" runat="server" Text="登陆" CssClass="button btn-green" OnClick="btnLogin_Click" /></div>
         

         <div class="li">   <br>      
        <a href="Default.aspx">返回首页→</a> </div>
      </div>

    </div>
            </form>
  </div>
</div>
     <div class="copyright">
      <br />
                    <ul class="nav">
                        <li><a href="Default.aspx">首页</a></li><span>|</span>
                        <li><a href="Line.aspx">旅游线路</a></li><span>|</span>
                        <li><a href="Promotion.aspx">促销商品</a></li><span>|</span>
                        <li><a href="Mall.aspx">积分商城</a></li><span>|</span>
                        <li><a href="PicWall.aspx">图片墙</a></li>
                    </ul>
                    <br/>
        	<p>  	<%=UIHelper.ShowCopyright() %></p>
        </div>
</body>
</html>

