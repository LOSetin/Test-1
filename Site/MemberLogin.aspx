<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberLogin.aspx.cs" Inherits="MemberLogin" %>

<!DOCTYPE html>
<html>
<head>
<title>用户登录</title>
<meta name="keywords" content="用户登陆" />
<meta name="description" content="用户登陆" />
<meta charset="utf-8">
<link href="css/home.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery.min.js"></script>
    <script src="js/messagebox.js"></script>
    <link href="css/messagebox.css" rel="stylesheet" />
</head>
<body>
<div class="wrap">
  <div class="logo"><img src="images/logo.jpg"  alt=""/></div>
  <div class="container">
      <form id="form1" runat="server">
    <div class="register-box">
      <div class="reg-slogan">用户登录 </div>
      <div class="reg-form" > <br>
        <br>
        <div class="cell">
          <label for="js-mobile_ipt"></label>
          <input id="tbUserName" runat="server" type="text" placeholder="填写账号" value="" name="mobile" class="text" maxlength="11" />
        </div>
        <div class="cell">
          <label for="js-mobile_pwd_ipt"></label>
          <input id="tbPwd" runat="server" type="password" placeholder="填写密码" value=""  class="text" />
         </div>
        <!-- 验证码 -->
        <div class="cell vcode">
          <label for="js-mail_vcode_ipt"></label>
          <input id="tbCode" runat="server" type="text" placeholder="填写验证码" value="" name="code" class="text" maxlength="4" />
          <img id="imgVerify" style="cursor:pointer; vertical-align:middle;"  src="VerifyCode.aspx?" alt="看不清？点击更换" title="看不清？点击更换" onclick="this.src=this.src+'?'" /> </div>



         <div class="bottom"><asp:Button ID="btnLogin" runat="server" Text="登陆" CssClass="button btn-green" OnClick="btnLogin_Click" /></div><br>
         <div class="bottom"><asp:Button ID="btnReg" runat="server" Text="注册" CssClass="button btn-green" OnClick="btnReg_Click" /> </div>
         
          <div class="li">   <br> 
          <p class="keeplogin">
			<input type="checkbox" name="isRemberPwdId" id="chkRember" runat="server" checked="checked" /> 
            <label for="loginkeeping">记住密码 </label>
            </p>     
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

