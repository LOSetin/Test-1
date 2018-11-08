<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="Promotion.aspx.cs" Inherits="Promotion" %>
<%@ Import Namespace="Arrow.Framework.Extensions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" Runat="Server">
    <title>促销产品</title>
    <link rel="stylesheet" type="text/css" href="css/base.css" />
<link rel="stylesheet" type="text/css" href="css/style.css" />
<script src="js/jquery.min.js" type="text/javascript" language="javascript" ></script>
<script src="js/jquery.SuperSlide.js" type="text/javascript" language="javascript" ></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
       <!--main-start-->
    <div class="main">
    	<div class="container clearfix">
            <div class="title-bar yahei">促销商品<span class="font-s16">PROMOTION</span></div>
        
            <div class="product_cx clearfix">
                <ul>
                    
                      <%
                          int pageSize = 5;
                          string condition = "1=1";
                          string orderBy = "StartTime desc";
                          int recordCount = TMSPager.GetRecordCount("Promotion", condition);
                          int pageCount = TMSPager.CaculatePageCount(pageSize, recordCount);
                          int pageIndex = GetUrlInt("p");
                          if (pageIndex <= 0) pageIndex = 1;
                          if (pageIndex > pageCount) pageIndex = pageCount;
                          var promotions = GetPromotion(condition, orderBy, pageIndex, pageSize);
                          foreach (var model in promotions)
                          {
                              string img = "";
                              if (DateTime.Now > model.EndTime)
                                  img = "images/chapter-1.png";
                     %>

                    <li>
                        <div class="pic"><a href="PromotionDetail.aspx?PromotionID=<%=model.ID %>"><img src="<%=getRawCover(model.CoverPath) %>" alt=""></a></div>
                        <div class="txt"><i class="bj_yz"><img src="<%=img %>" alt=""></i>
                            <div class="t"><span><a href="PromotionDetail.aspx?PromotionID=<%=model.ID %>"><%=model.Name %></a></span><p><%=model.PromotionDesc %></p></div>
                            <div class="sj">
                                <p>活动开始时间：<%=model.StartTime.ToDateOnlyString() %></p>
                                <p>活动结束时间：<%=model.EndTime.ToDateOnlyString() %></p>
                                </div>
                            <div class="qrgm">
                            	<a href="PromotionDetail.aspx?PromotionID=<%=model.ID %>">立即购买</a>
                            </div>
                        </div>
                    </li>
                    <%} %>

                </ul>
            </div>
            
            <div class="pagination-v1 page-v1" style="display:inline-block; margin-left:293px">
                <form action="Promotion.aspx?p=<%=pageIndex %><%=GetUrlQuery() %>" method="get">
                     <%=TMSPager.Show(pageIndex,pageSize,pageCount,5,recordCount,GetUrlQuery()) %>
                 <%--   <div class="go-wrap">
                        <div class="go-label">转到</div><input type="text" name="p" class="go-input" /><div class="go-unit">页</div><input type="submit" class="button-bg-blue button-font-white go-submit" value="确定" />
                    </div>--%>
                </form>
            </div>
        
        
        
		</div>
        
    </div>
    <!--main-end-->
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
    <script src="js/addons.js" type="text/javascript" language="javascript" ></script>
</asp:Content>

