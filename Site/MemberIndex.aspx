<%@ Page Title="" Language="C#" MasterPageFile="~/MemberMaster.master" AutoEventWireup="true" CodeFile="MemberIndex.aspx.cs" Inherits="MemberIndex" %>
<%@ Import Namespace="Arrow.Framework.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" Runat="Server">
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
                            <li><a href="#">我的信息</a></li>
                            
                        </ul>
                      </div>
                     <div class="hyzx clearfix"> 
                     	<div class="l1 ml15 fl">
                        	<div class="commentAvatarDiv m25 fl">
                            	<img  class="Image" src="images/img.jpg" width="108" height="144" alt=""/> <em class="uers">账号：<%=CurrentMember.UserName %></em>
                       	   </div> 
                          <div class="lf mt20 fl">
                          	<ul>
                           
                                   <li><em><img src="images/em2.jpg" width="26" height="26" alt="待处理订单"/></em><strong>待处理订单：(<a style="color:red;" href="MemberOrder.aspx"><%=OrderBLL.GetNotFinishOrderCount(CurrentMember.UserName) %></a>)</strong></li>
                            	<li><em><img src="images/em1.jpg" width="26" height="26" alt="可用点数"/></em><strong>可用点数：<%=MemberBLL.GetMemberPoints(CurrentMember.UserName) %></strong><a class="jf" href="Mall.aspx">兑换商品</a></li>
                                
                                <li><em><img src="images/em3.jpg" width="26" height="26" alt="消费总额"/></em><strong>消费总额：<%=MemberBLL.GetMemberTotalCost(CurrentMember.UserName) %></strong></li>
                <%--                <li><em><img src="images/em4.jpg" width="26" height="26" alt="可用积分"/></em><strong>待收兑商品：13213</strong></li>--%>
                            </ul>
                          </div>                          
                       </div>
                     </div>
                </div>

                 <!--biao-start-->
                <div class="information mt18 ml15">
               <div class="titlebiao">
                        <ul>
                        	<li ><a href="#">最新推荐</a></li>
                            <li style="float:right;"><a target="_blank" href="Line.aspx">查看更多>></a></li>
                        </ul>
                      </div>
                </div>
              <div class="model_l mt25">

              	<ul class="all-line-road clearfix">

                         <% 
                             var lines = LineBLL.SelectLineList(5, "IsDel=0", "AddTime desc");
                         %>
                       <% foreach (var line in lines) %>
                      <%{ %>
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
                        <div class="price-wrap1"> <span class="gray"><b class="orange font-s16">￥<%=line.MinPrice %>起</b></span> </div>
                        <div class="line-btn"> <a class="button-skin-2" href="LineDetail.aspx?ID=<%=line.ID %>" target="_blank">查看/预订</a> </div>
                        </div>
                    </li>
                      <%} %>
               
      			</ul>
            </div>

            <div class="bk10"></div>
                 <!--biao-end-->
            </div>
             <!--main-orders-end-->
        </div>
        </form>
    </div>
    <!--main-end-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
</asp:Content>

