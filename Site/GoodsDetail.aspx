<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="GoodsDetail.aspx.cs" Inherits="GoodsDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" Runat="Server">
    <title>商品兑换</title>
    <link rel="stylesheet" type="text/css" href="css/base.css" />
<link rel="stylesheet" type="text/css" href="css/style.css" />
    <link href="css/myfix.css" rel="stylesheet" />
<script src="js/jquery.min.js" type="text/javascript" language="javascript" ></script>
<script src="js/jquery.SuperSlide.js" type="text/javascript" language="javascript" ></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
        <!--main-start-->
    <div class="main">
        <form class="all-form" id="form1" runat="server" >
    	<div class="container clearfix">
            <div class="title-bar yahei">商品兑换<span class="font-s16">EXCHANGE</span></div>
            
			<div class="abu-content">

            	<ul class="shop-title mt20 clearfix">
                    <% int cid = GetUrlInt("CID");
                        if (cid == 0)
                        { 
                         %>
                <li class="cur"><a href="Mall.aspx">全部商品</a></li>
                    <%}else{ %>
                    <li><a href="Mall.aspx">全部商品</a></li>
                    <%} %>
                <%=UIHelper.ShowGoodCats() %>
            </ul>

                <div class="gift">
                    <div class="giftimg"><%=SiteUtility.ShowImage(CurrentGoods.CoverPath,368,372,false) %></div>
                    <div class="main-infor">
                        <h3><%=CurrentGoods.Name %></h3>
                        <div class="inforlist">
                            <p>&nbsp;</p>
                            <p>礼品编号：<span><%=CurrentGoods.ID %></span></p>
                            <p>库存数量：<span><%=CurrentGoods.Num %></span></p>
                            <p>所需点数：<span class="orange price"><%=CurrentGoods.Points %></span></p>
                            <p>我的积分：<span><%=CurrentMember==null?0:MemberBLL.GetMemberPoints(CurrentMember.UserName) %></span></p>
                            <p>兑换数量：<asp:DropDownList ID="ddlNum" runat="server" CssClass="tmsselect"></asp:DropDownList></p>
                        </div>
                        <a class="user-login-pop " href="ConfirmExChange.aspx?GoodsID=<%=CurrentGoods.ID %>">立即兑换</a>
                    </div>
                </div>
                <div class="giftintro">
                    <div class="gift-head"><h3>礼品介绍</h3></div>
                    <div class="intro-cont">
                        <p style="color:#666666;font-family:Arial, Verdana, 宋体;font-size:12px;background-color:#FFFFFF;"><%=CurrentGoods.Remarks.Replace("\n","<br/>") %></p>
                    </div>
                </div>




			</div>
            
			<div class="sidebar">
				<div class="rules">
                    <div class="rules-head">积分规则</div>
                    <div>
                        <dl>
                            <dt><a href="#">如何获得积分</a></dt>
                            <dd>进入平台选择相应旅游目的地的专线，预订有显示积分信息的产品，预订成功，在出团归来后的2工作日即可获得相应的积分。</dd>
                        </dl>
                        <dl>
                            <dt><a href="#">选择兑换商品</a></dt>
                            <dd>进入平台选择"积分商城"栏目，点击进入积分礼品列表，根据可用积分，选择相应分值的积分产品进行兑换。</dd>
                        </dl>
                        <dl>
                            <dt><a href="#">填写兑换地址</a></dt>
                            <dd>点击立即兑换后、填写收货人相关信息：收货人姓名，联系电话、收获地址及邮编（话费充值需在备注里添加充值号码）。点击提交订单后，兑换成功。</dd>
                        </dl>
                        <dl>
                            <dt><a href="#">备注</a></dt>
                            <dd>话费充值会在3个工作日为您充值成功；礼品兑换会在3-5个工作日为您进行配送。</dd>
                        </dl>
                    </div>
				</div>
			</div>
		</div>
        </form>
    </div>
    <!--main-end-->
    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
    <script src="js/addons.js" type="text/javascript" language="javascript" ></script>
</asp:Content>

