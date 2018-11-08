<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="PromotionDetail.aspx.cs" Inherits="PromotionDetail" %>
<%@ Import Namespace="Arrow.Framework.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" Runat="Server">
      <title>促销产品</title>
    <link rel="stylesheet" type="text/css" href="css/base.css" />
<link rel="stylesheet" type="text/css" href="css/style.css" />
<link rel="stylesheet" type="text/css" href="css/datepicker.calendar.css" />
<script src="js/jquery.min.js" type="text/javascript" language="javascript" ></script>
<script src="js/jquery.SuperSlide.js" type="text/javascript" language="javascript" ></script>
<script type="text/javascript" src="js/showdate.js"></script>
<script src="js/jquery.common-1.0.js"></script> 
<script src="js/datepicker.js"></script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
       <!--main-start-->
    <div class="main">
    	<form class="all-form" action="" method="post">
    	<div class="container clearfix">
            <div class="title-bar yahei">当前促销：<%=CurrentPromotion.Name %></div>
                
            <div class="model bg-white mt25">

             
                     	<ul class="all-line-road clearfix">

                     <% 
                         int pageSize = 5;
                         string orderBy = "ID desc";
                         string condition = "IsDel=0 And PromotionID="+GetUrlInt("PromotionID");

                         int recordCount = TMSPager.GetRecordCount("V_Promotion_Group", condition);
                         int pageCount = TMSPager.CaculatePageCount(pageSize, recordCount);
                         int pageIndex = GetUrlInt("p");
                         if (pageIndex <= 0) pageIndex = 1;
                         if (pageIndex > pageCount) pageIndex = pageCount;

                         var groups = GetGroups(condition, orderBy, pageIndex, pageSize);
                         %>

  

                      <% foreach (var group in groups) %>
                      <%{ %>
                	<li class="clearfix">
                        <div class="line-box clearfix"> <a href="PromotionGroupDetail.aspx?ID=<%=group.ID %>&PromotionID=<%=group.PromotionID %>&LineID=<%=group.LineID %>&GroupID=<%=group.GroupID %>"  target="_blank"><img src="<%=getRawCover(group.CoverPath) %>"></a> </div>
                        <div class="line-context">
                        <h3 class="line-free"> 
                            <a href="PromotionGroupDetail.aspx?ID=<%=group.ID %>&PromotionID=<%=group.PromotionID %>&LineID=<%=group.LineID %>&GroupID=<%=group.GroupID %>"  target="_blank"><%=group.LineName %></a> 
                        </h3>
                        <ul class="line-tip">
                            <li> <span>产品编号：<em class="gray"><%=group.ProductNum %></em></span> <span>旅游天数：<em class="gray"><%=group.TravelDays %></em></span> <span>出发抵达：<em class="gray"><%=group.StartAddress %>/<%=group.TargetAddress %></em> </span> </li>
                            <li class="line-traffic-box clearfix">
                            <label class="line-traffic-label">往返交通：</label>
                            <div class="line-traffic-wrap">
                            <p class="line-traffic-go-wrap"><em class="line-traffic-go"><i class="iconfont">→</i>往</em><span class="line-traffic-content"><%=group.GoTravel %></span></p>
                            <p class="line-traffic-back-wrap"><em class="line-traffic-back"><i class="iconfont">←</i>返</em><span class="line-traffic-content"><%=group.BackTravel %></span></p>
                            </div>
                            </li>
                            <li> 发团日期：<%=group.GoDate.ToDateOnlyString() %><em class="gray date"></em> </li>
                        </ul>
                            <p >优惠条件：<span class="red">一次性购买团位 <%=ShowBuyNum(group) %> 个</span></p>
                        <em class="details-wrap hide">线路说明：<em class="gray mr5"><%=group.LineDesc %></em></em>
                            <a href="javascript:;" class="line-show">展开详情<i class="iconfont"></i></a><a href="javascript:;" class="line-hide hide">收起详情<i class="iconfont"></i></a> 
                        </div>
                        <div class="line-infor">
                        <div class="price-wrap"> <span class="gray"><%=ShowPrice(group) %> </span><a class="collect gray user-login-dialog" href="javascript:;" data-id="10050376" data-title=""><i class="iconfont"></i><br>
                        收藏</a> </div>
                        <div class="line-btn"> <a class="button-skin-2" href="PromotionGroupDetail.aspx?ID=<%=group.ID %>&PromotionID=<%=group.PromotionID %>&LineID=<%=group.LineID %>&GroupID=<%=group.GroupID %>"  target="_blank">查看/预订</a> </div>
                        </div>
                    </li>
                      <%} %>
                	
      			</ul>


             	<div class="pagination-v1 page-v1" style="display:inline-block; margin-left:293px">
                            <%=TMSPager.Show(pageIndex,pageSize, pageCount, 5, recordCount,GetUrlQuery()) %>
 <%--                   <div class="go-wrap">
                        <div class="go-label">转到</div><input type="text" name="p" class="go-input" /><div class="go-unit">页</div><input type="submit" class="button-bg-blue button-font-white go-submit" value="确定" />
                    </div>--%>
            </div>
            <div class="bk10"></div>
            </div>
		</div>
        </form>
    </div>
    <!--main-end-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">

    <script src="js/base.js"></script> 
<script src="js/addons.js" type="text/javascript" language="javascript" ></script>
</asp:Content>

