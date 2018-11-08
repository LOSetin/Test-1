<%@ Page Title="" Language="C#" MasterPageFile="~/MemberMaster.master" AutoEventWireup="true" CodeFile="MemberFav.aspx.cs" Inherits="MemberFav" %>
<%@ Import Namespace="Arrow.Framework.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" Runat="Server">
    <script src="js/myfix.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
     <!--main-start-->
    <div class="main1">
        <form id="form1" runat="server">
    	<div class="containerl clearfix">
             <!--main-orders-->
            <div class="containerl select-wrap">
            	<div class="ordersnav1">
                    	<ul>
                          <li class="hy">会员中心</li>

                    	 <%=ShowMenu() %>

                  	    </ul>
                </div>
                      <div class="information mt18 ml15">
                      <div class="titlebiao">
                        <ul>
                        	<li class="ti"><a href="#">会员中心</a></li>
                            <li><a href="#">我的收藏</a></li>
                            
                        </ul>
                      </div>
                     <div class="czsm"> <h3>操作说明：</h3>
                   1.此处，将显示我收藏的线路
                     </div>
                </div>
                      </div>

                 <!--biao-start-->
                            <div class="model_l mt25">
                     	<ul class="all-line-road clearfix">

                     <% 
                         int pageSize = 5;

                         string condition = "ID in (Select FavObjID From MemberFav Where UserName='" + CurrentMember.UserName + "' And FavType='" + FavType.Line + "')";
                         string orderBy = "ID desc";

                         int recordCount = TMSPager.GetRecordCount("Line", condition);
                         int pageCount = TMSPager.CaculatePageCount(pageSize, recordCount);
                         int pageIndex = GetUrlInt("p");
                         if (pageIndex <= 0) pageIndex = 1;
                         if (pageIndex > pageCount) pageIndex = pageCount;

                         var lines = LineBLL.SelectLineList(condition, orderBy, pageIndex, pageSize);
                         %>

  

                      <% foreach (var line in lines)
                          {%>
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
                           <% var group = LineBLL.GetLastGroup(line.ID); %>
                            <li> 最近发团：<%=group==null?"":group.GoDate.ToDateOnlyString() %><em class="gray date"></em> <a class="calendar" href="LineDetail.aspx?ID=<%=line.ID %>" >更多团期<i class="iconfont more-orange">▶</i></a> </li>
                        </ul>
                        <em class="details-wrap hide">线路说明：<em class="gray mr5"><%=line.LineDesc %></em></em>
                            <a href="javascript:;" class="line-show">展开详情<i class="iconfont"></i></a><a href="javascript:;" class="line-hide hide">收起详情<i class="iconfont"></i></a> 
                        </div>
                        <div class="line-infor">
                        <div class="price-wrap"> <span class="gray"><b class="orange font-s16">￥<%=line.MinPrice %>起</b></span> <span  id="a<%=line.ID %>">
                            <a  class="collect" href="javascript:DelFav('<%=CurrentMember.UserName %>','<%=line.ID %>')" ><i class="iconfont fav"></i><br/>删除</a>
                            </span>
                                </div>
                        <div class="line-btn"> <a class="button-skin-2" href="LineDetail.aspx?ID=<%=line.ID %>" target="_blank">查看/预订</a> </div>
                        </div>
                    </li>
                      <%} %>
                	
      			</ul>


             	<div class="pagination-v1 page-v1" style="display:inline-block; margin-left:293px">
                            <%=TMSPager.Show(pageIndex,pageSize, pageCount, 5, recordCount,GetUrlQuery()) %>
            </div>
            <div class="bk10"></div>
            </div>



                 <!--biao-end-->
                            <div class="bk10"></div>
      
             <!--main-orders-end-->
                                </div>
        </form>
    </div>
    <!--main-end-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
</asp:Content>

