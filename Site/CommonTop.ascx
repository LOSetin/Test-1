<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CommonTop.ascx.cs" Inherits="CommonTop" %>
<%
    MemberInfo mi = MemberBLL.GetLoginInfo();
    %>
        	<div class="top">
            	<div class="hot_phone">
                	400-626-1012
                </div>
                 <div class="navBar">
			<ul class="nav1 clearfix">
                <%if (mi == null)
                    { %>
                  <li id="l1" class="mn">
				  	<h3><a target="_blank" href="MemberLogin.aspx">登陆</a></h3>
				   </li>
                   <li id="r1" class="m">
				  	<h3><a target="_blank" href="MemberReg.aspx">注册</a></h3>
				   </li>
                <%} else { %>
                <li class="mn"><h3><span style="color:#333">欢迎会员，</span><a href="MemberIndex.aspx"><%=mi.UserName %></a></h3></li>
                <li class="m"><h3><a href="MemberLogout.aspx">退出</a></h3></li>
                <%} %>

				   <li class="xia">
					<h3><a  href="MemberIndex.aspx">会员中心</a></h3>
					<ul class="sub">
						<li><a href="MemberIndex.aspx">我的信息</a></li>
						<li><a href="MemberOrder.aspx">我的订单</a></li>
						<li><a href="MemberExchange.aspx">我的兑换</a></li>
						<li><a href="MemberCost.aspx">我的消费</a></li>
						<li><a href="MemberFav.aspx">线路收藏</a></li>
                        <li><a href="MemberModifyData.aspx">修改资料</a></li>
                        <li><a href="MemberModifyPwd.aspx">修改密码</a></li>
                        <li><a href="MemberLogout.aspx">安全退出</a></li>
					</ul>
				</li>
                <li class="mn"><h3><a href="#"></a></h3></li>
            <%--    <li class="mn">
				  	<h3><a target="_blank" href="#">社区</a></h3>
				</li>
                 <li class="m">
				  	<h3><a target="_blank" href="#">关注微信</a></h3>
				</li>--%>

			</ul>
	</div>
                    <script type="text/javascript">
		jQuery(".nav1").slide({ 
				type:"menu", 
				titCell:".xia", 
				targetCell:".sub", 
				delayTime:0, 
				triggerTime:0, 
				returnDefault:true 
			});
	</script> 

            </div>
