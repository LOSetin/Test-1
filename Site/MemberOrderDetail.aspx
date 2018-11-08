<%@ Page Title="" Language="C#" MasterPageFile="~/MemberMaster.master" AutoEventWireup="true" CodeFile="MemberOrderDetail.aspx.cs" Inherits="MemberOrderDetail" %>
<%@ Import Namespace="TMS" %>
<%@ Import Namespace="Arrow.Framework.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" Runat="Server">
    <link href="css/myfix.css" rel="stylesheet" />

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
                            <asp:Literal ID="ltMenu" runat="server"></asp:Literal>
                    	 <%=ShowMenu() %>

                  	    </ul>
                </div>
              <div class="information mt18 ml15">
                      <div class="titlebiao">
                        <ul>
                        	<li class="ti" ><a href="MemberIndex.aspx">会员中心</a></li>
                            <li class="ti"><a href="MemberOrder.aspx">我的订单</a></li>
                            <li ><a href="#">订单详情</a></li>
                        </ul>
                      </div>
                     <div class="czsm"> <h3>操作说明：</h3>
                         在本页，您可以查看订单详细信息。
                     </div>
                  
                  <%
                      var order = OrderBLL.GetOrderView(OrderNum);
                      if (order == null) Response.Redirect("MemberOrder.aspx");
                       %>

                     <!--biao-start-->
                  
                <div class="t1">
                        	<h1>基本信息</h1>
                        </div>
                            <table width="1050" class="gridview">
                            <tr>
                                <th class="w150">参加线路：</th><td><a target="_blank" href="LineDetail.aspx?id=<%=order.LineID %>"><%=order.LineName %></a></td>
                            </tr>
                           <tr>
                                <th class="w150">旅游天数：</th><td><%=order.TravelDays %></td>
                            </tr>
                            <tr>
                                <th class="w150">出发日期：</th><td><%=order.GoDate.ToDateOnlyString() %></td>
                            </tr>
                                 <tr>
                                <th class="w150">返回日期：</th><td><%=order.BackDate.ToDateOnlyString() %></td>
                            </tr>
                                 <tr>
                                <th class="w150">集合地点：</th><td><%=order.GatheringPlace%></td>
                            </tr>
                                 <tr>
                                <th class="w150">集合时间：</th><td><%=order.GatheringTime %></td>
                            </tr>
                                 <tr>
                                <th class="w150">单&nbsp;&nbsp;&nbsp;价：</th><td>￥<%=order.InnerPrice %> </td>
                            </tr>
                                        <tr>
                                <th class="w150">团&nbsp;&nbsp;&nbsp;位：</th><td><%=order.BuyNum %> </td>
                            </tr>
                            <tr>
                                <th class="w150">总&nbsp;&nbsp;&nbsp;价：</th><td>￥<%=order.TotalMoney %> </td>
                            </tr>
                            <tr>
                                <th class="w150">参与促销：</th><td><%=order.PromotionName %></td>
                            </tr>
                           
                            <tr>
                                <th class="w150">订单状态：</th><td align="center"> 

                                <%=UIHelper.ShowOrderStatusBar(order.OrderStatus,order.OrderHistory) %>

                                </td>
                            </tr>
                            <tr>
                           
                            </tr>
                             
                            </table>
                            <div class="t1">
                        	<h1>联系人信息</h1>
                           </div>
                            <table width="1050" class="gridtable1">
                            <tr>
                                <th class="w150">姓名：</th><td><%=order.AddMemberRealName %></td>
                            </tr>
                           
                            <tr>
                                <th class="w150">电话：</th><td><%=order.AddMemberMobile %></td>
                            </tr>
                            
                            </table>
                            <br/>
                            <h1>参团者信息</h1>
                            <table width="1050" class="gridtable1">
                            <tr>
                                <th>真实姓名</th><th>性别</th><th>身份证</th><th>联系电话</th>
                            </tr>
                           <% var details = OrderBLL.GetAllDetails(OrderNum, CurrentMember.UserName);
                               foreach (var detail in details)
                               {
                                %>

                            <tr>
                                <td><%=detail.RealName %></td><td><%=detail.Sex %></td><td><%=detail.IDNum %></td><td><%=detail.MobileNum %></td>
                            </tr>
                                <%} %>
                           
                            </table>
                  <!--biao-end-->


                </div>
            <div class="bk10"></div>

            </div>
             <!--main-orders-end-->
        </div>
        </form>
    </div>
    <!--main-end-->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
</asp:Content>

