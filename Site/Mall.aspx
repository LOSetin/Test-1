<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="Mall.aspx.cs" Inherits="Mall" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" Runat="Server">
    <title>积分商城</title>
    <link rel="stylesheet" type="text/css" href="css/base.css" />
<link rel="stylesheet" type="text/css" href="css/style.css" />
<script src="js/jquery.min.js" type="text/javascript" language="javascript" ></script>
<script src="js/jquery.SuperSlide.js" type="text/javascript" language="javascript" ></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
     <!--main-start-->
    <div class="main">
    	<div class="container clearfix">
            <div class="title-bar yahei">积分商城<span class="font-s16">SHOP</span></div>
            
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
                <%=ShowGoodCats() %>
            </ul>
             
				<div class="product clearfix">
					<ul>
                           <%
                               int pageSize = 5;
                              
                               string condition = "IsOut=0";
                               if (cid > 0)
                                   condition = condition + " And CatID=" + cid;
                               string orderBy = "ID desc";
                               int recordCount = TMSPager.GetRecordCount("Goods", condition);
                               int pageCount = TMSPager.CaculatePageCount(pageSize, recordCount);
                               int pageIndex = GetUrlInt("p");
                               if (pageIndex <= 0) pageIndex = 1;
                               if (pageIndex > pageCount) pageIndex = pageCount;
                               var goods = getGoods(condition, orderBy, pageIndex, pageSize);
                     %>
                        <% foreach (var model in goods)
                            { %>

						<li class="no-margin" >
							<a target="_blank" href="GoodsDetail.aspx?ID=<%=model.ID %>" class="product-wrap"><img title="" src="<%=getRawCover(model.CoverPath) %>"></a>
							<div class="shop-detail">
                                <a class="product-title" target="_blank" href="GoodsDetail.aspx?ID=<%=model.ID %>"><%=model.Name %></a>
								<div class="product-spend-box">
                                    <span><b class="orange font-s14 mh5"><%=model.Points %></b> 积分</span>
                                    <a class="button-lightorange line-h20 fr mh5 user-login-pop " href="GoodsDetail.aspx?ID=<%=model.ID %>">我要兑换</a>
                                </div>
							</div>
						</li>
                        <%} %>

                      
                       

					</ul>
				</div>
                <br />
                <div style="margin:0 auto; text-align:center;">
                    <div class="pagination-v1 page-v1" style="display:inline-block; text-align:center;">
                    <form action="Mall.aspx?p=<%=pageIndex %><%=GetUrlQuery() %>" method="get">
                    <%=TMSPager.Show(pageIndex,pageSize,pageCount,5,recordCount,GetUrlQuery()) %>
                    <%--    <div class="go-wrap">
                            <div class="go-label">转到</div><input type="text" name="p" class="go-input" /><div class="go-unit">页</div><input type="submit" class="button-bg-blue button-font-white go-submit" value="确定" />
                        </div>--%>
                    </form>
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
        
    </div>
    <!--main-end-->
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
    <script src="js/addons.js" type="text/javascript" language="javascript" ></script>
</asp:Content>

