<%@ Page Title="" Language="C#" MasterPageFile="~/MemberMaster.master" AutoEventWireup="true" CodeFile="MemberCost.aspx.cs" Inherits="MemberCost" %>
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
                            <li><a href="#">我的消费</a></li>
                        </ul>
                      </div>
                     <div class="czsm"> <h3>操作说明：</h3>
                   1.此处，将显示我的消费记录

                     </div>

                  <%
                      string k = GetUrlString("k");
            DateTime bt = GetUrlDateTime("bt");
            DateTime et = GetUrlDateTime("et");
            string sbt = "";
            string set = "";
            if (bt > DateTime.MinValue)
                sbt = bt.ToDateOnlyString();
            if (et > DateTime.MinValue)
                set = et.ToDateOnlyString();
            tbKeyword.Text = k;
            tbBegin.Text = sbt;
            tbEnd.Text = set;
                       %>

                 <div class="line-search bg-white" >
                     <div style="padding-left:150px;" >
                            <ul class="clearfix">
                                <li>
                                    <label>关键字：</label>
                                    <asp:TextBox ID="tbKeyword" runat="server" CssClass="import" placeholder="路线关键字" Text=''></asp:TextBox>
                                </li>
                                <li>
                                    <label>时间：</label>
                                    <div class="pos team-date">
                                        <asp:TextBox ID="tbBegin" runat="server" CssClass="sm-import date-picker"  placeholder="下单开始时间"  ClientIDMode="Static" onClick="return Calendar('tbBegin');"></asp:TextBox>
                                    </div>
                                    <em>-</em>
                                    <div class="pos team-date">
                                        <asp:TextBox ID="tbEnd" runat="server" CssClass="sm-import date-picker"  placeholder="下单结束时间" ClientIDMode="Static" onClick="return Calendar('tbEnd');"  ></asp:TextBox>
                                    </div>
                                </li>
           
                                <li>
                                <asp:Button ID="btnSearch" Text="搜索" runat="server" CssClass="btn-search btn-blue btn-font-white ml5" OnClick="btnSearch_Click" />
      
                                    <input type="reset" value="重置" class="btn-search btn-lightorange btn-search-reset ml5"  onclick="window.location.href='MemberCost.aspx'"  />      
									
                                </li>
                            </ul>
                         </div>
                    </div>

                                      <div>
                            <table width="1050" class="gridtable1" >
                            <tr>
                                <th>用户名</th><th>参与促销</th><th>路线</th><th>出发日期</th><th>购买数量</th><th>支付总额</th><th>已支付</th><th>点数增加</th><th>消费时间</th>
                            </tr>
                     <% 

                         int pageSize = 10;

                         string condition =  "CostType='" + CostType.JoinGroup + "' And UserName='"+CurrentMember.UserName+"'";
                         if (!k.IsNullOrEmpty())
                             condition = condition + "And LineName like '%" + k + "%'";
                         if (!sbt.IsNullOrEmpty())
                             condition = condition + " And AddTime>='" + bt.ToStartString() + "'";
                         if (!set.IsNullOrEmpty())
                             condition = condition + " And AddTime<='" + et.ToEndString() + "'";

                         string orderBy = "AddTime desc";

                         int recordCount = TMSPager.GetRecordCount("V_CostHistory", condition);
                         int pageCount = TMSPager.CaculatePageCount(pageSize, recordCount);
                         int pageIndex = GetUrlInt("p");
                         if (pageIndex <= 0) pageIndex = 1;
                         if (pageIndex > pageCount) pageIndex = pageCount;
                         var items = new TMS.V_CostHistory().SelectList(condition, orderBy, pageIndex, pageSize, null);
                         foreach (var history in items)
                         {
                         %>
             
                            <tr onmouseover="currentBgColor=this.style.backgroundColor; this.style.backgroundColor='#eaf1dc';" onmouseout="this.style.backgroundColor=currentBgColor">
                                <td><%=history.UserName %></td><td><%=history.PromotionName %></td>
                                <td><a target="_blank" href="LineDetail.aspx?ID=<%=history.LineID %>"><%= history.LineName %></a></td><td><%=history.GoDate.ToDateOnlyString() %></td><td><%=history.BuyNum %></td><td><%=history.TotalMoney %></td><td><%=history.MoneyPayed %></td><td><%=history.PointsCost %></td><td><%=history.AddTime %></td>
                            </tr>
                           
                  <%} %>
        </table>
                                          <br />
                                           
                 </div>

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

