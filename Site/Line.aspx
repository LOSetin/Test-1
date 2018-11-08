<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="Line.aspx.cs" Inherits="UILine"  %>
<%@ Import Namespace="Arrow.Framework.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" Runat="Server">
  <title>旅游线路</title>
<link rel="stylesheet" type="text/css" href="css/base.css" />
<link rel="stylesheet" type="text/css" href="css/style.css" />
<link rel="stylesheet" type="text/css" href="css/datepicker.calendar.css" />
<script src="js/jquery.min.js" type="text/javascript" language="javascript" ></script>
<script src="js/jquery.SuperSlide.js" type="text/javascript" language="javascript" ></script>
<script type="text/javascript" src="js/showdate.js"></script>
<script src="js/jquery.common-1.0.js"></script> 
<script src="js/datepicker.js"></script> <script src="js/myfix.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
         <!--main-start-->
    <div class="main">
    	<form class="all-form" id="form1" runat="server" >
    	<div class="container clearfix">
        	<div class="model mt10">
                    
                    <div class="line-search bg-white">
                            <ul class="clearfix">
                                <li>
                                    <label>分类：</label>
                                    <asp:DropDownList ID="ddlFirstCat" runat="server" CssClass="sle" AutoPostBack="true" OnSelectedIndexChanged="ddlFirstCat_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:DropDownList ID="ddlSecondCat" runat="server" CssClass="sle" AutoPostBack="true" OnSelectedIndexChanged="ddlSecondCat_SelectedIndexChanged"></asp:DropDownList>
                                
                                </li>
                            </ul>
                    </div>
                    <div class="choose bg-white">
                        <ul>
                                     <li >
                                <span>天数：</span>
                                         <%=CreateDaysSearchLink() %>
                            </li>
                            <li>
                                <span>价格：</span>
                              <%=CreateFixPriceSearchLink("0","不限") %>
                                 <%=CreateFixPriceSearchLink("1","500以下") %>
                                 <%=CreateFixPriceSearchLink("2","500-1000") %>
                                 <%=CreateFixPriceSearchLink("3","1000-2000") %>
                                 <%=CreateFixPriceSearchLink("4","2000-4000") %>
                                 <%=CreateFixPriceSearchLink("5","4000-6000") %>
                                 <%=CreateFixPriceSearchLink("6","6000以上") %>
                            </li>
                            <li>
                                <span>接送：</span>
                                <%=CreatePickupSearchLink("0","不限") %>
                                <%=CreatePickupSearchLink("1","接送") %>
                                <%=CreatePickupSearchLink("-1","不接送") %>
                                
                            </li>
      
                        </ul>
                    </div>
                    <div class="line-search bg-white">
                            <ul class="clearfix">
                                <li>
                                    <label>关键字：</label>
                                    <input name="keyword" type="text" class="import" placeholder="线路名称" value="<%=CurrentSearchInfo.KeyWord %>" />
                                </li>
                                <li>
                                    <span>团期：</span>
                                    <div class="pos team-date">
                                        <input name="cdate1" type="text" class="sm-import date-picker" id="time" placeholder="起始日期" onclick="return Calendar('time');" value="<%=CurrentSearchInfo.MinGoDate<=GlobalSetting.MinTime?"":CurrentSearchInfo.MinGoDate.ToDateOnlyString() %>" />
                                    </div>
                                    <em>-</em>
                                    <div class="pos team-date">
                                        <input name="cdate2" type="text" class="sm-import date-picker" id="time2" placeholder="结束日期" onclick="return Calendar('time2');"  value="<%=CurrentSearchInfo.MaxGoDate<=GlobalSetting.MinTime?"":CurrentSearchInfo.MaxGoDate.ToDateOnlyString() %>" />
                                    </div>
                                </li>
                             
                                                                 <li>
                                    <span>价格：</span>
                                    <div class="pos price">
                                        <input name="cprice1" type="text" placeholder="起始价格"  class="sm-import" value="<%= CurrentSearchInfo.MinCustomPrice==0M?"":CurrentSearchInfo.MinCustomPrice.ToString() %>" />
                                        <b class="darkgray">￥</b>
                                    </div>
                                    <em>-</em>
                                    <div class="pos price">
                                        <input name="cprice2" type="text" placeholder="结束价格" class="sm-import" value="<%=CurrentSearchInfo.MaxCustomPrice==0M?"":CurrentSearchInfo.MaxCustomPrice.ToString() %>" />
                                        <b class="darkgray">￥</b>
                                    </div>
                                </li>
                                <li>
                                    <asp:Button ID="btnSearch" Text="搜索" runat="server" CssClass="btn-search btn-blue btn-font-white ml5" OnClick="btnSearch_Click" />
                                    <asp:Button ID="btnReset" Text="重置" runat="server" CssClass="btn-search btn-lightorange btn-search-reset ml5" OnClick="btnReset_Click" />
      
									
                                </li>
                            </ul>
                    </div>
                </div>
                
            <div class="model bg-white mt25">
            	<div class="all-sort clearfix">
        			<div class="fl"> 
                        <%=CreateNewSort() %>
                        <%=CreateSellSort() %>
                        <%=CreatePriceSort() %>

                        <span style="float:right"><a class="sort-item action-export-xlt" title="点击后通过EXCEL表格生成当前页面所有线路的报价列表" >生成线路报价列表</a></span> </div>
      			</div>

                     	<ul class="all-line-road clearfix">

                     <% 
                         int pageSize = 5;

                         LineSearchInfo si = LineSearch.GetSearchInfoFromUrl();
                         string condition = LineSearch.CreateFilter(si);
                         string orderBy = LineSearch.CreateSort(si);

                         int recordCount = TMSPager.GetRecordCount("Line", condition);
                         int pageCount = TMSPager.CaculatePageCount(pageSize, recordCount);
                         int pageIndex = GetUrlInt("p");
                         if (pageIndex <= 0) pageIndex = 1;
                         if (pageIndex > pageCount) pageIndex = pageCount;

                         var lines = GetLines(condition, orderBy, pageIndex, pageSize);
                         MemberInfo currentMember = MemberBLL.GetLoginInfo();
                         string memberName = currentMember == null ? "" : currentMember.UserName;
                         var favList = new List<TMS.MemberFavInfo>();
                         if (currentMember != null)
                             favList = MemberBLL.SelectLineFavs(currentMember.UserName);
                         bool isFav = false;
                         %>

  

                      <% foreach (var line in lines) %>
                      <%{
                              isFav = favList.FindIndex(s => s.FavObjID == line.ID.ToString()) > -1 ? true : false;
                              %>
                	<li class="clearfix">
                        <div class="line-box clearfix"> <a href="LineDetail.aspx?ID=<%=line.ID %>" target="_blank"><img src="<%=getRawCover(line.CoverPath) %>"></a> </div>
                        <div class="line-context">
                        <h3 class="line-free"> 
                            <a href="LineDetail.aspx?ID=<%=line.ID %>"  target="_blank"><%=line.Name %></a> 
                        </h3>
                        <ul class="line-tip">
                            <li> <span>产品编号：<em class="gray"><%=line.ProductNum %></em></span> <span>旅游天数：<em class="gray"><%=line.TravelDays %></em></span> <span>出发抵达：<em class="gray"><%=line.StartAddress %>/<%=line.TargetAddress %></em> </span> </li>
                            <li class="line-traffic-box clearfix">
                            <label class="line-traffic-label">往返交通：</label>
                            <div class="line-traffic-wrap">
                            <p class="line-traffic-go-wrap"><em class="line-traffic-go"><i class="iconfont">→</i>往</em><span class="line-traffic-content"><%=line.GoTravel %></span></p>
                            <p class="line-traffic-back-wrap"><em class="line-traffic-back"><i class="iconfont">←</i>返</em><span class="line-traffic-content"><%=line.BackTravel %></span></p>
                            </div>
                            </li>
                            <% var group = GetLastGroup(line.ID); %>
                            <li> 最近发团：<%=group==null?"":group.GoDate.ToDateOnlyString() %><em class="gray date"></em> <a class="calendar" href="LineDetail.aspx?ID=<%=line.ID %>" >更多团期<i class="iconfont more-orange">▶</i></a> </li>
                        </ul>
                        <em class="details-wrap hide">线路说明：<em class="gray mr5"><%=line.LineDesc %></em></em>
                            <a href="javascript:;" class="line-show">展开详情<i class="iconfont"></i></a><a href="javascript:;" class="line-hide hide">收起详情<i class="iconfont"></i></a> 
                        </div>
                        <div class="line-infor">
                        <div class="price-wrap"> <span class="gray"><b class="orange font-s16">￥<%=line.MinPrice %>起</b></span> <span  id="a<%=line.ID %>">
                            <%if (!isFav)
                                { %>
                            <a class="collect" href="javascript:AddFav('<%=memberName %>','<%=line.ID %>')" ><i class="iconfont"></i><br/>收藏</a> 
                            <%}
                        else { %>
                            <a  class="collect" href="javascript:RemoveFav('<%=memberName %>','<%=line.ID %>')" ><i class="iconfont fav"></i><br/>已收藏</a>
                            <%} %>
                            </span>
                                </div>
                        <div class="line-btn"> <a class="button-skin-2" href="LineDetail.aspx?ID=<%=line.ID %>" target="_blank">查看/预订</a> </div>
                        </div>
                    </li>
                      <%} %>
                	
      			</ul>


             	<div class="pagination-v1 page-v1" style="display:inline-block; margin-left:293px">
                            <%=TMSPager.Show(pageIndex,pageSize, pageCount, 5, recordCount,GetUrlQuery()) %>
<%--                    <div class="go-wrap">
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

<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
<script src="js/base.js"></script> 
<script src="js/addons.js" type="text/javascript" language="javascript" ></script>
</asp:Content>