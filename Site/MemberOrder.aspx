<%@ Page Title="" Language="C#" MasterPageFile="~/MemberMaster.master" AutoEventWireup="true" CodeFile="MemberOrder.aspx.cs" Inherits="MemberOrder" %>
<%@ Import Namespace="Arrow.Framework.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" Runat="Server">
    <link href="css/myfix.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" Runat="Server">
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
                            <li><a href="#">我的订单</a></li>
                        </ul>
                      </div>
                     <div class="czsm"> <h3>操作说明：</h3>
                   1.订单未提交时，可以修改参团人资料，也可直接提交，让商家输入参团人资料<br />
2.订单提交后，需要等待商家确认，确认后，才可以进行支付<br />
3.在支付成功之前，均可以取消订单。支付成功后，交易完成，可获得与订单价值相等的点数，1元=1点 <br />
                     </div>

                  <%
                      string k = GetUrlString("k");
            string s = GetUrlString("s");
            if (s.IsNullOrEmpty())
                s = "所有";
            DateTime bt = GetUrlDateTime("bt");
            DateTime et = GetUrlDateTime("et");
            string sbt = "";
            string set = "";
            if (bt > DateTime.MinValue)
                sbt = bt.ToDateOnlyString();
            if (et > DateTime.MinValue)
                set = et.ToDateOnlyString();
            tbKeyword.Text = k;
            ddlStatus.SelectedValue = s;
            tbBegin.Text = sbt;
            tbEnd.Text = set;
                       %>

                 <div class="line-search bg-white" >
                            <ul class="clearfix">
                                <li>
                                    <label>关键字：</label>
                                    <asp:TextBox ID="tbKeyword" runat="server" CssClass="import" placeholder="订单关键字" Text=''></asp:TextBox>
                                </li>
                                <li>
                                    <label>状态：</label>
                                       <asp:DropDownList ID="ddlStatus" runat="server" CssClass="sle" >
                                           <asp:ListItem Text="所有" Value="所有"></asp:ListItem>
                                           <asp:ListItem Text="未提交" Value="未提交"></asp:ListItem>
                                           <asp:ListItem Text="已提交" Value="已提交"></asp:ListItem>
                                           <asp:ListItem Text="已确认" Value="已确认"></asp:ListItem>
                                           <asp:ListItem Text="已完成" Value="已完成"></asp:ListItem>
                                            <asp:ListItem Text="已取消" Value="已取消"></asp:ListItem>
                                       </asp:DropDownList>
                                </li>
                                <li>
                                    <label>时间：</label>
                                    <div class="pos team-date">
                                        <asp:TextBox ID="tbBegin" runat="server" CssClass="sm-import date-picker"  placeholder="订单开始时间"  ClientIDMode="Static" onClick="return Calendar('tbBegin');"></asp:TextBox>
                                    </div>
                                    <em>-</em>
                                    <div class="pos team-date">
                                        <asp:TextBox ID="tbEnd" runat="server" CssClass="sm-import date-picker"  placeholder="订单结束时间" ClientIDMode="Static" onClick="return Calendar('tbEnd');"  ></asp:TextBox>
                                    </div>
                                </li>
           
                                <li>
                                <asp:Button ID="btnSearch" Text="搜索" runat="server" CssClass="btn-search btn-blue btn-font-white ml5" OnClick="btnSearch_Click" />
      
                                    <input type="reset" value="重置" class="btn-search btn-lightorange btn-search-reset ml5" onclick="window.location.href='MemberOrder.aspx'" />      
									
                                </li>
                            </ul>
                    </div>

                   <!--订单顶部-->

                  <div>
                      <table width="1050" class="ordertitle">
                          <tr>
                              <th width="650">订单详情</th>
                              <th width="200">参与促销</th>
                              <th width="100">状态</th>
                              <th width="100">操作</th>
                          </tr>
                      </table>
                      </div>
                  <br />
                  <!--订单顶部-->
                     <% 

                         int pageSize = 5;

                         string condition = "AddMemberName='" + CurrentMember.UserName + "'";
                         if (!k.IsNullOrEmpty())
                             condition = condition + "And LineName like '%" + k + "%'";
                         if (!s.IsNullOrEmpty() && s != "所有")
                             condition = condition + " And OrderStatus = '" + s + "'";
                         if (!sbt.IsNullOrEmpty())
                             condition = condition + " And AddTime>='" + bt.ToStartString() + "'";
                         if (!set.IsNullOrEmpty())
                             condition = condition + " And AddTime<='" + et.ToEndString() + "'";

                         string orderBy = "AddTime desc";

                         int recordCount = TMSPager.GetRecordCount("V_Order", condition);
                         int pageCount = TMSPager.CaculatePageCount(pageSize, recordCount);
                         int pageIndex = GetUrlInt("p");
                         if (pageIndex <= 0) pageIndex = 1;
                         if (pageIndex > pageCount) pageIndex = pageCount;
                         var orders = new TMS.V_Order().SelectList(condition, orderBy, pageIndex, pageSize, null);
                         foreach (var order in orders)
                         {
                         %>
                    <!--订单项-->
                  <div >
                      <table width="1050" class="ordernum">
                        <tr><th><span class="title">下单时间：</span><span><%=order.AddTime %></span>&nbsp;&nbsp;&nbsp; <span class="title">订单号：</span><span><%=order.OrderNum %></span></th></tr>
                      </table>
                  </div>
                  <!--订单项-->
                  
                      <div class="biao1">
                          <table width="1050" class="orderitem">
                              <tr>
                                  <td width="440" style="text-align:left; padding-left:10px;"><a target="_blank" href="LineDetail.aspx?ID=<%=order.LineID %>"><%=order.LineName %></a>
                                  <p class="mygodate">出发日期：<%=order.GoDate.ToDateOnlyString() %></p></td>
                                  <td width="75" >团位：<%=order.BuyNum %></td>
                                  <td width="125" class="bd">总额：￥<%=order.TotalMoney %></td>
                                  <td width="200" class="bd"><%=order.PromotionName %></td>
                                  <td width="100" class="bd"><%=order.OrderStatus %><br /> <a href="MemberOrderDetail.aspx?OrderNum=<%=order.OrderNum %>&ReturnUrl=<%=Server.UrlEncode(CurrentUrl) %>" >查看详情</a></td>
                                  <td width="100">
                                     
                                      <%=UIHelper.ShowOrderButton(order.OrderNum,order.OrderStatus,CurrentUrl) %>
                                  </td>
                              </tr>
                          </table>
                      </div>
                  <br />
                  <%} %>

                </div>
        	<div style="margin-left:150px; text-align:center;">
                              <div class="pagination-v1 page-v1" style="display:inline-block; margin:0 auto;">
                            <%=TMSPager.Show(pageIndex,pageSize, pageCount, 5, recordCount,GetUrlQuery()) %>

            </div>
        	</div>
              
               

            <div class="bk10"></div>
                 <!--biao-end-->
            </div>
             <!--main-orders-end-->
        </div>
        </form>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
</asp:Content>

